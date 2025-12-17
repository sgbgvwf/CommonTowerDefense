using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackLockStrategy_HealthMinimum : IState
{
    //private AttackLockStrategyManager _manager;

    private AttackDetection _attackDetection;

    private AttackLockBlackboard _blackboard;

    private Dictionary<GameObject, float> objectHealthDict = new Dictionary<GameObject, float>();

    public void Init(AttackLockBlackboard blackboard)
    {
        _blackboard = blackboard;
    }

    public void OnEnter()
    {
        //Debug.Log("?");
        _attackDetection = _blackboard.attackDetection;
        //_blackboard = _manager.blackboard;
    }

    public void OnExit()
    {
        objectHealthDict.Clear();
        objectDistanceDict.Clear();
    }

    public void OnUpdate()
    {
        RemoveOverScope();
        RemoveZeroHealth();
        HealthMinLock();
    }

    public void HealthMinLock()
    {
        if (_attackDetection.objectPosition.Count == 0)
        {
            return;
        }

        foreach(var _object in _attackDetection.objectPosition.Keys)
        {
            if (!objectHealthDict.ContainsKey(_object))
            {
                objectHealthDict.Add(_object, HealthMinimumCalculation(_object));
            }
            else
            {
                objectHealthDict[_object] = HealthMinimumCalculation(_object);
            }
        }

        float minHealth = objectHealthDict.Values.Min();

        List<GameObject> minHealthObjectList = objectHealthDict
            .Where(pair => pair.Value == minHealth)
            .Select(pair => pair.Key)
            .ToList();

        if(minHealthObjectList.Count == 1)
        {
            _blackboard.attackDirection = (_attackDetection.objectPosition[minHealthObjectList[0]] - _attackDetection.detectionPosition).normalized;

        }
        else if(minHealthObjectList.Count > 1) 
        {
            PathNearestLock();
        }

    }



    private float HealthMinimumCalculation(GameObject gameObject)
    {
        if (!gameObject.GetComponent<GeneralProperty>())
        {
            return float.MaxValue;
        }

        float health = gameObject.GetComponent<GeneralProperty>().health;

        return health;
    }

    private void RemoveOverScope()
    {
        List<GameObject> momentPosition = new List<GameObject>();

        foreach (var _object in objectHealthDict.Keys)
        {
            bool exist = false;

            foreach (var obj in _attackDetection.objectPosition.Keys)
            {
                if (obj == _object)
                {
                    exist = true;
                }
            }

            if (!exist)
            {
                momentPosition.Add(_object);
            }
        }

        foreach (var obj in momentPosition)
        {
            objectHealthDict.Remove(obj);
            objectDistanceDict.Remove(obj);
        }

        momentPosition.Clear();

    }
    
    private void RemoveZeroHealth()
    {
        List<GameObject> momentObject = new List<GameObject>();

        foreach (var _object in objectHealthDict.Keys)
        {
            if(_object == null)
            {
                continue;
            }

            if (_object.GetComponent<GeneralProperty>().health <= 0)
            {
                momentObject.Add(_object);
            }
        }

        foreach (var obj in momentObject)
        {
            objectHealthDict.Remove(obj);
            objectDistanceDict.Remove(obj);
        }

        momentObject.Clear();
    }
    


    private Dictionary<GameObject, float> objectDistanceDict = new Dictionary<GameObject, float>();


    public void PathNearestLock()
    {
        if (objectHealthDict.Count == 0)
        {
            return;
        }
        //Debug.Log(_attackDetection.objectPosition.Count);
        foreach (var _object in objectHealthDict.Keys)
        {
            if (!objectDistanceDict.ContainsKey(_object) && _object != null)
            {
                objectDistanceDict.Add(_object, PathNearestDistanceCalculation(_object));
            }
            else
            {
                objectDistanceDict[_object] = PathNearestDistanceCalculation(_object);
            }
        }

        float minDistance = objectDistanceDict.Values.Min();

        GameObject nearestObject = objectDistanceDict
            .Where(pair => pair.Value == minDistance)
            .Select(pair => pair.Key)
            .FirstOrDefault();

        _blackboard.attackDirection = (_attackDetection.objectPosition[nearestObject] - _attackDetection.detectionPosition).normalized;

    }

    /// <summary>
    /// 计算对象与其最终路径点的曼哈顿距离
    /// </summary>
    /// <param name="gameObject">要计算的对象</param>
    /// <returns></returns>
    private float PathNearestDistanceCalculation(GameObject gameObject)
    {
        if (gameObject == null)
        {
            return float.MaxValue;
        }
        if (!gameObject.GetComponent<EnemyPath>())
        {
            return float.MaxValue;
        }

        EnemyPath enemyPath = gameObject.GetComponent<EnemyPath>();

        float distance =
            Mathf.Abs(gameObject.transform.position.x -
            enemyPath.planPathPointsList[enemyPath.planPathPointsList.Count - 1].transform.position.x) +
            Mathf.Abs(gameObject.transform.position.y -
            enemyPath.planPathPointsList[enemyPath.planPathPointsList.Count - 1].transform.position.y);
        //Debug.Log(enemyPath.planPathPointsList[enemyPath.planPathPointsList.Count - 1].transform.position);
        return distance;

    }
}
