using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{


    private Vector2 detectionPosition;

    [Header("检测半径")]
    public float detectionRadius;




    public void FindEnemy()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(detectionPosition, detectionRadius);

        //遍历检测到的所有碰撞体
        foreach (var hitCollider in hitColliders)
        {









        }






    }



    private float DistanceCalculation(GameObject gameObject)
    {
        if (!gameObject.GetComponent<EnemyPath>())
        {
            return 0; 
        }

        float distance = Mathf.Abs((gameObject.transform.position.x - 
            gameObject.GetComponent<EnemyPath>().planPathPointsList
            [gameObject.GetComponent<EnemyPath>().planPathPointsList.Count]
            .transform.position.x)+
            (gameObject.transform.position.y -
            gameObject.GetComponent<EnemyPath>().planPathPointsList
            [gameObject.GetComponent<EnemyPath>().planPathPointsList.Count]
            .transform.position.y)
            );

        return distance;

    }







}
