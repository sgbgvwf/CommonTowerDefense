using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerStraightController : MonoBehaviour
{
    
    [HideInInspector]public Vector3 direction;

    [Header("·ÉÐÐËÙ¶È")]
    public float speed;

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }


}
