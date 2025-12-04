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
    }

    public void OnUpdate()
    {
        RemoveOverScope();
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

        GameObject minHealthObject = objectHealthDict
            .Where(pair => pair.Value == minHealth)
            .Select(pair => pair.Key)
            .First();

        _blackboard.attackDirection = (_attackDetection.objectPosition[minHealthObject] - _attackDetection.detectionPosition).normalized;
    }



    private float HealthMinimumCalculation(GameObject gameObject)
    {
        if (!gameObject.GetComponent<Health>())
        {
            return 0f;
        }

        float health = gameObject.GetComponent<Health>().health;

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

        foreach (var enemy in momentPosition)
        {
            objectHealthDict.Remove(enemy);
        }

        momentPosition.Clear();

    }
}
