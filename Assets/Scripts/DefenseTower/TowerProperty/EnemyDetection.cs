using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField]private TowerStateManager _stateManager;

    private Vector3 detectionPosition;

    [Header("检测半径")]
    public float detectionRadius;

    private Dictionary<GameObject, Vector3> enemyPosition = new Dictionary<GameObject, Vector3>();

    private Dictionary<GameObject, float> enemyDistance = new Dictionary<GameObject, float>();

    [Header("当前检测敌人")]
    [HideInInspector]public Vector3 direction;

    private void Start()
    {
        detectionPosition = transform.position + new Vector3(0.5f, 0.5f, 0);
    }

    private void Update()
    {
        EnterScope();
        ExitScope();

        LockEnemy();
    }

    private void EnterScope()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(detectionPosition, detectionRadius);

        if(hitColliders.Length > 0)
        {
            _stateManager.blackboard.currentState = TowerState.Attack;
        }

        //遍历检测到的所有碰撞体
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag != "Enemy")
            {
                continue;
            }

            if (!enemyPosition.ContainsKey(hitCollider.gameObject))
            {
                enemyPosition.Add(hitCollider.gameObject, hitCollider.transform.position);
            }
        }
    }

    private void ExitScope()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(detectionPosition, detectionRadius);

        //遍历所有曾在范围内的碰撞体
        foreach (var enemy in enemyPosition.Keys)
        {
            bool exist = false;

            foreach(var hitCollider in hitColliders)
            {
                if(hitCollider == enemy)
                {
                    exist = true;
                }
            }

            if (!exist)
            {
                enemyPosition.Remove(enemy);
                enemyDistance.Remove(enemy);
            }
        }
    }

    public void LockEnemy()
    {
        if(enemyPosition.Count == 0)
        {
            return;
        }

        foreach(var enemy in enemyPosition.Keys)
        {
            if (!enemyDistance.ContainsKey(enemy))
            {
                enemyDistance.Add(enemy, DistanceCalculation(enemy));
            }
            else
            {
                enemyDistance[enemy] = DistanceCalculation(enemy);
            }
        }

        float minDistance = enemyDistance.Values.Min();

        GameObject nearestEnemy = enemyDistance
            .Where(pair => pair.Value == minDistance)
            .Select(pair => pair.Key)
            .FirstOrDefault();

        direction = enemyPosition[nearestEnemy] - detectionPosition;

    }

    private float DistanceCalculation(GameObject gameObject)
    {
        if (!gameObject.GetComponent<EnemyPath>())
        {
            return 0; 
        }

        float distance = 
            Mathf.Abs(gameObject.transform.position.x - 
            gameObject.GetComponent<EnemyPath>().planPathPointsList
            [gameObject.GetComponent<EnemyPath>().planPathPointsList.Count]
            .transform.position.x)+
            Mathf.Abs(gameObject.transform.position.y -
            gameObject.GetComponent<EnemyPath>().planPathPointsList
            [gameObject.GetComponent<EnemyPath>().planPathPointsList.Count]
            .transform.position.y);

        return distance;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + new Vector3(0.5f, 0.5f, 0), detectionRadius);
    }

}
