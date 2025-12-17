using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMakerTowerProcessDisplay : MonoBehaviour
{

    public Transform towerTransform;
    //public Transform displayTransform;

    //public GroundSearch groundSearch;

    //public PlaceMakerTowerController controller;

    public void ArrowToward(float count, Vector3 way)
    {



        Vector2 towerWay =way - towerTransform.position;
        towerWay = towerWay.normalized;
        //Debug.Log(way);
        //Debug.Log(gameObject.transform.position);

        //Debug.Log(towerWay);
        float angleRadians = -MathF.Atan2(towerWay.x, towerWay.y);

        float angleDegrees = angleRadians * Mathf.Rad2Deg;
        //Debug.Log(angleDegrees);
        transform.rotation = Quaternion.Euler(0, 0, angleDegrees);
        




    }



}
