using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        CoreHealth.Instance.CoreHealthReduce(collision.gameObject);

        if (collision.GetComponent<EnemyPath>())
        {
            collision.GetComponent<EnemyPath>().planPathPointsList.Clear();
            collision.GetComponent<EnemyPath>().currentTargetPathPoint = null;
            ObjectPoolManager.Instance.ReturnObject(collision.GetComponent<GeneralProperty>().prefabReference, collision.gameObject);

        }
    }
}
