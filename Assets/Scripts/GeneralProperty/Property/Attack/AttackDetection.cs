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
        _targetType = _StrategyManager.blackboard.targetType;
    }

    private void Start()
    {
        detectionPosition = transform.position + new Vector3(0.5f, 0.5f, 0);
    }

    private void Update()
    {
        
        EnterScope(_targetType.ToString());
        ExitScope();
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
        Gizmos.DrawWireSphere(transform.position + new Vector3(0.5f, 0.5f, 0), detectionRadius);
    }



}
