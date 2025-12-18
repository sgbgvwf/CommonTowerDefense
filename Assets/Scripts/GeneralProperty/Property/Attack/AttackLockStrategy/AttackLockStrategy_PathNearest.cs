using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AttackLockStrategy_PathNearest : IState
{
    //private AttackLockStrategyManager _manager;

    private AttackDetection _attackDetection;

    private AttackLockBlackboard _blackboard;

    private Dictionary<GameObject, float> objectDistanceDict = new Dictionary<GameObject, float>();

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
        objectDistanceDict.Clear();
    }

    public void OnUpdate()
    {
        RemoveOverScope();
        RemoveZeroHealth();
        PathNearestLock();
        //Debug.Log("_attackDetection");

    }

    public void PathNearestLock()
    {
        if (_attackDetection.objectPosition.Count == 0)
        {
            return;
        }
        //Debug.Log(_attackDetection.objectPosition.Count);
        foreach (var _object in _attackDetection.objectPosition.Keys)
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

        SetAttackDetection(nearestObject);
    }

    private void SetAttackDetection(GameObject nearestObject)
    {
        if(_blackboard.attackLockType.targetType == EntityType.Enemy)
        {
            _blackboard.attackDirection = (_attackDetection.objectPosition[nearestObject] - _attackDetection.detectionPosition).normalized;
        }
        else if (_blackboard.attackLockType.targetType == EntityType.DefenseTower)
        {
            _blackboard.attackDirection = (_attackDetection.objectPosition[nearestObject] + new Vector3(0.5f, 0.5f, 0) - _attackDetection.detectionPosition).normalized;
        }

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

    private void RemoveOverScope()
    {
        List<GameObject> momentPosition = new List<GameObject>();

        foreach (var _object in objectDistanceDict.Keys)
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
            objectDistanceDict.Remove(enemy);
        }

        momentPosition.Clear();

    }
    private void RemoveZeroHealth()
    {
        List<GameObject> momentObject = new List<GameObject>();

        foreach (var _object in objectDistanceDict.Keys)
        {
            if (_object == null)
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
            objectDistanceDict.Remove(obj);
        }

        momentObject.Clear();
    }
}
