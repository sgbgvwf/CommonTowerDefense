using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Concorde.Timer;

public class FlyerStraightController : MonoBehaviour
{
    
    //[HideInInspector]public Vector3 direction;
    public Vector3 direction;

    private TimerManager timerManager;

    [Header("飞行速度")]
    public float speed;

    //public float SlowScale;

    [Header("发射状态")]
    public bool fly;

    [Header("销毁计时")]
    private bool destroyTimer;

    private void Awake()
    {
        timerManager = new TimerManager();
        fly = false;
        destroyTimer = false;
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        if(fly && !destroyTimer)
        {
            timerManager.Start("DestroyTimer", 1f / speed * 50f);
            destroyTimer = true;
        }
        if (timerManager.IsFinished("DestroyTimer"))
        {
            Destroy(gameObject);
        }
    }



}
