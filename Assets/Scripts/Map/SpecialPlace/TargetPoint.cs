using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        CoreHealth.Instance.CoreHealthReduce(collision.gameObject);
        Destroy(collision.gameObject);
    }
}
