using Concorde.Timer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackLaunch : MonoBehaviour, IAttackStrategy
{
    public AttackLockStrategyManager strategyManager;

    private TimerManager timerManager;

    private AttackLockBlackboard _blackboard;

    [HideInInspector]public AttackLaunchTimer _timer = new AttackLaunchTimer();

    [Header("预制体与其父物体")]
    public GameObject prefab;

    public Transform parent;

    [Header("攻击间隔")]
    public float attackFrequency;
    public float attackSpeedScale;

    [Header("延迟时间")]
    public float delayTime;



    public void OnAttackEnter<T1, T2>(ref T1 blackboard, T2 attackDetection)
    {

        _blackboard = strategyManager.blackboard;
        timerManager = new TimerManager();

        _timer.BeginTimer();
    }

    public void OnAttackUpdate<T1, T2>(ref T1 blackboard, T2 attackDetection)
    {
        //Debug.Log("111"+ _blackboard.delayAttack);
        switch (_blackboard.delayAttack)
        {
            case true:
                if (_timer.DetectTimer())
                {
                    GameObject entity = GetEntity();
                    //entity.name = "entity";
                    //entity.GetComponent<FlyerStraightController>().direction = Vector3.zero;

                    StartCoroutine(DelayTime(entity));

                    _timer.EndTimer(attackFrequency, delayTime, attackSpeedScale, _blackboard.delayAttack);
                }
                break;

            case false:
                if (_timer.DetectTimer())
                {
                    GameObject entity = GetEntity();
                    //entity.name = "Bullet(Clone)";
                    LaunchObject(entity);

                    _timer.EndTimer(attackFrequency, delayTime, attackSpeedScale, _blackboard.delayAttack);
                }
                break;
        }

    }
    
    public void OnAttackExit<T>(ref T blackboard)
    {
        if (_blackboard.delayAttack)
        {
            if (parent.transform?.Find("entity"))
            {
                GameObject entity = parent.transform.Find("entity").gameObject;
                ObjectPoolManager.Instance.ReturnObject(prefab, entity);
            }
            
        }

    }

    private GameObject GetEntity()
    {

        GameObject entity = ObjectPoolManager.Instance.GetObject(prefab, _blackboard.SpawnPosition, Quaternion.identity, parent);

        entity.GetComponent<FlyerStraightController>().resource = gameObject;
        return entity;
        
    }

    /// <summary>
    /// 发射攻击
    /// </summary>
    /// <param name="gameObject"></param>
    public void LaunchObject(GameObject gameObject)
    {
        gameObject.GetComponent<FlyerStraightController>().direction = _blackboard.attackDirection;
        gameObject.GetComponent<FlyerStraightController>().fly = true;
        gameObject.GetComponent<FlyerStraightController>().resource = this.gameObject;
        //Debug.Log(_blackboard.attackDirection);

    }


    private IEnumerator DelayTime(GameObject entity)
    {
        yield return new WaitForSeconds(delayTime);
        if(entity != null)
        {
            LaunchObject(entity);

        }
    }





}
