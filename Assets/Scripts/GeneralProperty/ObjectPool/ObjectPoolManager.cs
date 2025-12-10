using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager instance;
    public static ObjectPoolManager Instance;

    //预制体以及对应的对象池
    private Dictionary<GameObject, Queue<GameObject>> _poolDict = new Dictionary<GameObject, Queue<GameObject>>();

    private Dictionary<Queue<GameObject>, int> _queueValue = new Dictionary<Queue<GameObject>, int>();

    private void Awake()
    {
        if (instance == null)
        {
            Instance = this;
        }
    }

    /// <summary>
    /// 初始化对象池（先提前创建好一些对象）
    /// </summary>
    /// <param name="prefab">预制体</param>
    /// <param name="initialCount">初始数量</param>
    /// <param name="maxCount">最大数量</param>
    public void InitPool(GameObject prefab, int initialCount, int maxCount)
    {
        if(initialCount > maxCount)
        {
            initialCount = maxCount;
        }

        //避免重复初始化
        if (_poolDict.ContainsKey(prefab))
        {
            return;
        }

        _poolDict[prefab] = new Queue<GameObject>();
        _queueValue[_poolDict[prefab]] = maxCount;

        for (int i = 0; i < initialCount; i++)
        {
            GameObject newObj = Instantiate(prefab);
            newObj.SetActive(false);//禁用
            newObj.transform.SetParent(transform);
            newObj.GetComponent<GeneralProperty>().prefabReference = prefab;
            _poolDict[prefab].Enqueue(newObj);
        }

        //Debug.Log( _poolDict.ContainsKey(prefab));
        //Debug.Log(prefab);
    }

    /// <summary>
    /// 从对象池获取对象（默认父对象）
    /// </summary>
    /// <param name="prefab">预制体</param>
    /// <param name="pos">位置</param>
    /// <param name="rot">旋转</param>
    /// <returns>激活后的游戏对象</returns>
    public GameObject GetObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject targetObj;

        if (_poolDict.ContainsKey(prefab) && _poolDict[prefab].Count > 0)
        {
            targetObj = _poolDict[prefab].Dequeue();
            targetObj.transform.position = position;
            targetObj.transform.rotation = rotation;
            targetObj.SetActive(true);
        }
        else
        {
            targetObj = Instantiate(prefab, position, rotation);
            targetObj.transform.SetParent(transform);

            //没有对象池就创建一个
            if (!_poolDict.ContainsKey(prefab))
            {
                _poolDict[prefab] = new Queue<GameObject>();
            }
        }

        return targetObj;
    }

    /// <summary>
    /// 从对象池获取对象（设置父物体）
    /// </summary>
    /// <param name="prefab">预制体</param>
    /// <param name="position">位置</param>
    /// <param name="rotation">旋转</param>
    /// <param name="parent">父物体</param>
    /// <returns></returns>
    public GameObject GetObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        GameObject targetObj;

        if (_poolDict.ContainsKey(prefab) && _poolDict[prefab].Count > 0)
        {
            Debug.Log("1");
            targetObj = _poolDict[prefab].Dequeue();
            targetObj.transform.position = position;
            targetObj.transform.rotation = rotation;
            targetObj.transform.SetParent(parent);
            targetObj.SetActive(true);
        }
        else
        {
            Debug.Log("2");
            targetObj = Instantiate(prefab, position, rotation);
            targetObj.transform.SetParent(parent);

            //没有对象池就创建一个
            if (!_poolDict.ContainsKey(prefab))
            {
                _poolDict[prefab] = new Queue<GameObject>();
            }
        }

        return targetObj;
    }


    /// <summary>
    /// 回收对象至对象池
    /// </summary>
    /// <param name="prefab">对应预制体</param>
    /// <param name="obj">要回收的对象</param>
    public void ReturnObject(GameObject prefab, GameObject obj)
    {
        if (!_poolDict.ContainsKey(prefab))
        {
            Destroy(obj);
            Debug.Log("No:" + prefab);
            Debug.Log(obj.GetComponent<GeneralProperty>().prefabReference);
            return;
        }

        obj.SetActive(false);//禁用

        if(_poolDict[prefab].Count < _queueValue[_poolDict[prefab]])
        {
            _poolDict[prefab].Enqueue(obj);
            obj.transform.SetParent(transform);//把对象回收回对象池
            Debug.Log("recycle");
        }
        else
        {
            Destroy(obj);//溢出则销毁
            Debug.Log("max");
        }

    }





}
