using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackLockStrategy_DistanceNearest : IState
{
    //private AttackLockStrategyManager _manager;

    private AttackDetection _attackDetection;

    private AttackLockBlackboard _blackboard;

    private Dictionary<GameObject, float> distanceDict = new Dictionary<GameObject, float>();

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
        distanceDict.Clear();
    }

    public void OnUpdate()
    {
        RemoveOverScope();
        DistanceLock();
        //Debug.Log("_attackDetection");
    }


    public void DistanceLock()
    {
        if (_attackDetection.objectPosition.Count == 0)
        {
            return;
        }
        
        foreach(var _object in _attackDetection.objectPosition.Keys)
        {
            if (!distanceDict.ContainsKey(_object))
            {
                distanceDict.Add(_object, NearestDistanceCalculation(_object, _attackDetection.detectionPosition));
            }
            else
            {
                distanceDict[_object] = NearestDistanceCalculation(_object, _attackDetection.detectionPosition);
            }
        }

        float minDistance = distanceDict.Values.Min();

        GameObject nearestObject = distanceDict
            .Where(pair => pair.Value == minDistance)
            .Select(pair => pair.Key)
            .FirstOrDefault();

        _blackboard.attackDirection = (_attackDetection.objectPosition[nearestObject] - _attackDetection.detectionPosition).normalized;
    }


    /// <summary>
    /// 计算对象与检测点之间的距离
    /// </summary>
    /// <param name="gameObject">被检测对象</param>
    /// <param name="detector">检测对象的位置</param>
    /// <returns></returns>
    private float NearestDistanceCalculation(GameObject gameObject, Vector3 detector)
    {
        if (!gameObject.GetComponent<EnemyPath>())
        {
            return 0;
        }

        EnemyPath enemyPath = gameObject.GetComponent<EnemyPath>();

        float distance = Mathf.Abs((gameObject.transform.position - detector).magnitude);

        return distance;
    }

    private void RemoveOverScope()
    {
        List<GameObject> momentPosition = new List<GameObject>();

        foreach (var _object in distanceDict.Keys)
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

        foreach (var enemy in momentPosition)
        {
            distanceDict.Remove(enemy);
        }

        momentPosition.Clear();

    }




}
