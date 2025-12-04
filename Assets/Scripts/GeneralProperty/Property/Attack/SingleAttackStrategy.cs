using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Concorde.Timer;

public class SingleAttackStrategy : MonoBehaviour, ITowerAttackStrategy
{
    private AttackLockBlackboard _blackboard;


    [Header("预制体与其父物体")]
    public GameObject prefab;

    public Transform parentObject;
    /*
    [Header("检测与索敌")]
    public EnemyDetection enemyDetection;
    */
    [Header("攻击间隔")]
    public float fireFrequency;


    private TimerManager timerManager;



    //public AttackTimeType attackTimeType;

    public float delayTime;


    public void OnAttackEnter(TowerStateBlackboard blackboard, AttackDetection attackDetection)
    {
        _blackboard = GetComponent<AttackLockStrategyManager>().blackboard;

        timerManager = new TimerManager();
        timerManager.Start("AttackFrequency", 0f);
        //Debug.Log("at");
    }

    public void OnAttackUpdate(TowerStateBlackboard blackboard, AttackDetection attackDetection)
    {
        if (timerManager.IsFinished("AttackFrequency"))
        {
            GameObject entity = Instantiate(prefab, _blackboard.attackDetection.detectionPosition, Quaternion.identity, parentObject);


        }




        /*
        if (timerManager.IsFinished("FireFrequency"))
        {
            if(blackboard.attackTimeType == AttackTimeType.Immediately)
            {
                attackTimeTypes.ImmediatelyAttack(bullet, blackboard.firePosition, attackDetection, bulletEntity);

                timerManager.Remove("FireFrequency");
                timerManager.Start("FireFrequency", fireFrequency);
            }
            else if(blackboard.attackTimeType == AttackTimeType.Delay)
            {
                attackTimeTypes.DelayAttack(bullet, blackboard.firePosition, attackDetection, bulletEntity, delayTime);

                timerManager.Remove("FireFrequency");
                timerManager.Start("FireFrequency", fireFrequency + delayTime);
            }

            
        }
        */
    }


    public void OnAttackExit(TowerStateBlackboard blackboard)
    {
        /*
        GameObject bullet = bulletEntity.transform?.Find("Bullet").gameObject;
        Destroy(bullet);
        */
    }









}
