using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Concorde.Timer;

public class FlyerStraightController : MonoBehaviour
{

    //[HideInInspector]public Vector3 direction;
    public GameObject resource;
    public Vector3 direction;
    private TimerManager timerManager;


    [Header("飞行速度")]
    public float speed;

    //public float SlowScale;

    [Header("发射状态")]
    public bool fly;

    [Header("销毁（回收）计时")]
    private bool destroyTimer;

    private void Awake()
    {
        timerManager = new TimerManager();
        InitializeData();
        resource = this.gameObject;
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
            direction = Vector3.zero;
            ObjectPoolManager.Instance.ReturnObject(GetComponent<GeneralProperty>().prefabReference, this.gameObject);
            timerManager.Start("DestroyTimer", 1f / speed * 50f);
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void InitializeData()
    {
        if (timerManager.Exists("DestroyTimer"))
        {
            timerManager.Remove("DestroyTimer");
        }
        fly = false;
        destroyTimer = false;
    }

}
