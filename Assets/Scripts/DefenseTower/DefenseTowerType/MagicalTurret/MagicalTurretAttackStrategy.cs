using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Concorde.Timer;

public class MagicalTurretAttackStrategy : MonoBehaviour, ITowerAttackStrategy
{
    public void OnAttackEnter(TowerStateBlackboard blackboard, EnemyDetection enemyDetection)
    {
        timerManager.Start("FireFrequency", 0f);
    }

    public void OnAttackUpdate(TowerStateBlackboard blackboard, EnemyDetection enemyDetection)
    {
        if (timerManager.IsFinished("FireFrequency"))
        {
            GameObject _magicBullet = Instantiate(magicBullet, blackboard.firePosition, Quaternion.identity, magicBulletEntity);
            timerManager.Start("Wait", fireFrequency / 5);

            


            timerManager.Remove("FireFrequency");
            timerManager.Start("FireFrequency", fireFrequency);
        }


    }

    public void OnAttackExit(TowerStateBlackboard blackboard)
    {
        throw new System.NotImplementedException();
    }



    [Header("魔法弹预制体")]
    public GameObject magicBullet;
    public Transform magicBulletEntity;
    /*
    [Header("检测与索敌")]
    public EnemyDetection enemyDetection;
    */
    [Header("蓄力时间（攻击间隔）")]
    public float fireFrequency;



    private TimerManager timerManager;



    private void Awake()
    {
        timerManager = new TimerManager();
    }






    public void Fire(GameObject magicBullet, Vector3 direction)
    {

    }



}
