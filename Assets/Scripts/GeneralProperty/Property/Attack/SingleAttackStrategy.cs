using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Concorde.Timer;

public class SingleAttackStrategy : MonoBehaviour, IAttackStrategy
{
    public void OnAttackEnter(TowerStateBlackboard blackboard, EnemyDetection enemyDetection)
    {
        timerManager.Start("FireFrequency", 0f);
        //Debug.Log("at");
    }

    public void OnAttackUpdate(TowerStateBlackboard blackboard, EnemyDetection enemyDetection)
    {
        if (timerManager.IsFinished("FireFrequency"))
        {
            if(blackboard.attackTimeType == AttackTimeType.Immediately)
            {
                attackTimeTypes.ImmediatelyAttack(bullet, blackboard.firePosition, enemyDetection, bulletEntity);

                timerManager.Remove("FireFrequency");
                timerManager.Start("FireFrequency", fireFrequency);
            }
            else if(blackboard.attackTimeType == AttackTimeType.Delay)
            {
                attackTimeTypes.DelayAttack(bullet, blackboard.firePosition, enemyDetection, bulletEntity, delayTime);

                timerManager.Remove("FireFrequency");
                timerManager.Start("FireFrequency", fireFrequency + delayTime);
            }

            
        }

    }

    public void OnAttackExit(TowerStateBlackboard blackboard)
    {
        Destroy(bulletEntity.transform?.Find("Bullet"));
    }



    [Header("预制体与其父物体")]
    public GameObject bullet;

    public Transform bulletEntity;
    /*
    [Header("检测与索敌")]
    public EnemyDetection enemyDetection;
    */
    [Header("攻击间隔")]
    public float fireFrequency;

    private TimerManager timerManager;

    [Header("攻击时序")]
    public AttackTimeTypes attackTimeTypes;

    //public AttackTimeType attackTimeType;

    public float delayTime;


    private void Awake()
    {
        timerManager = new TimerManager();
    }





}
