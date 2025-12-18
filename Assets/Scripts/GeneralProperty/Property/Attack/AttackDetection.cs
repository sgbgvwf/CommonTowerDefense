using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackDetection : MonoBehaviour
{
    [HideInInspector]public Vector3 detectionPosition;

    public AttackLockStrategyManager _StrategyManager;

    private EntityType _targetType;

    [Header("检测半径")]
    public float detectionRadius;

    public Dictionary<GameObject, Vector3> objectPosition = new Dictionary<GameObject, Vector3>();


    

    private void Awake()
    {
        _targetType = _StrategyManager.blackboard.attackLockType.targetType;
    }

    private void Start()
    {

    }

    private void Update()
    {
        PositionUpdate();
        EnterScope(_targetType.ToString());
        ExitScope();

        if(objectPosition.Count > 0)
        {
            _StrategyManager.blackboard.lockEnemy = true;
        }
        else
        {
            _StrategyManager.blackboard.lockEnemy = false;
        }
    }

    private void PositionUpdate()
    {
        if (_StrategyManager.blackboard.attackLockType.itselfType == EntityType.DefenseTower)
        {
            detectionPosition = transform.position + new Vector3(0.5f, 0.5f, 0);
        }
        else if (_StrategyManager.blackboard.attackLockType.itselfType == EntityType.Enemy)
        {
            detectionPosition = transform.position;
        }
    }

    private void EnterScope(string detectionTarget)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(detectionPosition, detectionRadius);

        //遍历检测到的所有碰撞体
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag != detectionTarget)
            {
                continue;
            }

            if (!objectPosition.ContainsKey(hitCollider.gameObject))
            {
                objectPosition.Add(hitCollider.gameObject, hitCollider.transform.position);
            }
            else
            {
                objectPosition[hitCollider.gameObject] = hitCollider.transform.position;
            }
        }

    }

    private void ExitScope()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(detectionPosition, detectionRadius);

        List<GameObject> momentPosition = new List<GameObject>();

        //遍历所有曾在范围内的碰撞体
        foreach  (var _object in objectPosition.Keys)
        {
            bool exist = false;

            foreach (var hitCollider in hitColliders)
            {
                if(hitCollider.gameObject == _object)
                {
                    exist = true;
                }
            }

            if (!exist)
            {
                momentPosition.Add(_object);
            }
        }

        foreach(var _object in momentPosition)
        {
            objectPosition.Remove(_object);
        }

        momentPosition.Clear();

    }





    private void OnDrawGizmosSelected()
    {
        if (_StrategyManager.blackboard.attackLockType.itselfType == EntityType.DefenseTower)
        {
            Gizmos.DrawWireSphere(transform.position + new Vector3(0.5f, 0.5f, 0), detectionRadius);
        }
        else if (_StrategyManager.blackboard.attackLockType.itselfType == EntityType.Enemy)
        {
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }



}
