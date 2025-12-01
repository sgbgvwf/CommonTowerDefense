using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Concorde.Timer;

public class MagicalTurretAttackController : MonoBehaviour
{
    [Header("魔法弹预制体")]
    public GameObject magicBullet;

    [Header("检测与索敌")]
    public EnemyDetection enemyDetection;

    [Header("蓄力时间（攻击间隔）")]
    public float fireFrequence;


    private TimerManager timerManager;



    private void Awake()
    {
        timerManager = new TimerManager();
    }


    private void Update()
    {
        


    }






    public void Fire()
    {

    }


    //private IEnumerator 


}
