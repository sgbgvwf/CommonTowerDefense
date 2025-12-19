using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager instance;
    public static ObjectPoolManager Instance;

    //预制体以及对应的对象池
    private Dictionary<GameObject, Queue<GameObject>> _poolDict = new Dictionary<GameObject, Queue<GameObject>>();

    private Dictionary<GameObject, int> _queueValue = new Dictionary<GameObject, int>();

    //为每个预制体创建锁对象（通常用object）
    //用字典映射就不会影响不同对象的分别获取
    private Dictionary<GameObject, object> _lockObjects = new Dictionary<GameObject, object>();

    //添加一个全局锁来保护字典操作，防止同时访问get方法来操作
    private readonly object _dictionaryLock = new object();

    private void Awake()
    {
        if (instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // 获取锁对象，如果没有则创建
    private object GetLockObject(GameObject prefab)
    {
        lock (_dictionaryLock)
        {
            if (!_lockObjects.ContainsKey(prefab))
            {
                _lockObjects[prefab] = new object();//直接“[]”包含了创建键
            }
            return _lockObjects[prefab];
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
        if (initialCount > maxCount)
        {
            initialCount = maxCount;
        }

        //避免重复初始化
        if (_poolDict.ContainsKey(prefab))
        {
            return;
        }

        //上锁，即这里的代码同一时间只能有一个线程执行，其他线程会等待，直到这个锁被释放
        lock (GetLockObject(prefab))
        {
            _poolDict[prefab] = new Queue<GameObject>();
            _queueValue[prefab] = maxCount;

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

        lock (GetLockObject(prefab))
        {
            // 检查队列是否包含该预制体并且队列不为空
            if (_poolDict.TryGetValue(prefab, out var queue) && queue.Count > 0)
            {
                // 检查队列中的对象是否有效
                while (queue.Count > 0)
                {
                    targetObj = queue.Dequeue();
                    if (targetObj != null)
                    {
                        targetObj.transform.position = position;
                        targetObj.transform.rotation = rotation;
                        targetObj.SetActive(true);
                        return targetObj;
                    }
                    //如果对象为null，继续从队列中取出下一个
                }

                // 如果队列中所有对象都为null，创建一个新的
                targetObj = Instantiate(prefab, position, rotation);
                targetObj.transform.SetParent(transform);
                targetObj.GetComponent<GeneralProperty>().prefabReference = prefab;
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

        lock (GetLockObject(prefab))
        {
            //检查队列是否包含该预制体并且队列不为空
            if (_poolDict.TryGetValue(prefab, out var queue) && queue.Count > 0)
            {
                //Debug.Log("1");
                //检查队列中的对象是否有效
                while (queue.Count > 0)
                {
                    targetObj = queue.Dequeue();
                    if (targetObj != null)
                    {
                        targetObj.transform.position = position;
                        targetObj.transform.rotation = rotation;
                        targetObj.transform.SetParent(parent);
                        targetObj.SetActive(true);
                        return targetObj;
                    }
                    //如果对象为null，继续从队列中取出下一个
                }

                // 如果队列中所有对象都为null，创建一个新的
                targetObj = Instantiate(prefab, position, rotation, parent);
                targetObj.GetComponent<GeneralProperty>().prefabReference = prefab;
            }
            else
            {
                //Debug.Log("2");
                //Debug.Log(prefab);
                targetObj = Instantiate(prefab, position, rotation, parent);

                //targetObj.transform.SetParent(parent);
                //Debug.Log(targetObj.transform.parent);
                //没有对象池就创建一个
                if (!_poolDict.ContainsKey(prefab))
                {
                    _poolDict[prefab] = new Queue<GameObject>();
                    //Debug.Log("4");
                }
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
        if (obj == null) return;

        lock (GetLockObject(prefab))
        {
            // 检查对象是否已经被销毁
            if (obj == null)
            {
                return;
            }

            if (!_queueValue.ContainsKey(prefab))
            {
                Destroy(obj);
                //Debug.Log("No:" + prefab);
                //Debug.Log(obj.GetComponent<GeneralProperty>().prefabReference);
                return;
            }

            obj.SetActive(false);//禁用

            if (_poolDict[prefab].Count < _queueValue[prefab])
            {
                _poolDict[prefab].Enqueue(obj);
                obj.transform.SetParent(transform);//把对象回收回对象池
                //Debug.Log("recycle");
            }
            else
            {
                Destroy(obj);//溢出则销毁
                //Debug.Log("max");
            }
        }
    }
}