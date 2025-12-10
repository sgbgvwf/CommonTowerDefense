using Concorde.Timer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackLaunch : MonoBehaviour, ITowerAttackStrategy
{
    public AttackLockStrategyManager strategyManager;

    private TimerManager timerManager;

    private AttackLockBlackboard _blackboard;

    [Header("预制体与其父物体")]
    public GameObject prefab;

    public Transform parent;

    [Header("攻击间隔")]
    public float attackFrequency;

    public float attackSpeedScale;

    [Header("延迟时间")]
    public float delayTime;



    public void OnAttackEnter(TowerStateBlackboard blackboard, AttackDetection attackDetection)
    {
        _blackboard = strategyManager.blackboard;
        timerManager = new TimerManager();
        timerManager.Start("AttackFrequency", 0f);
    }

    public void OnAttackUpdate(TowerStateBlackboard blackboard, AttackDetection attackDetection)
    {
        //Debug.Log(blackboard.currentState);
        switch (_blackboard.delayAttack)
        {
            case true:
                if (timerManager.IsFinished("AttackFrequency"))
                {
                    GameObject entity = ObjectPoolManager.Instance.GetObject(prefab, _blackboard.SpawnPosition, Quaternion.identity, parent);
                    //entity.name = "entity";
                    //entity.GetComponent<FlyerStraightController>().direction = Vector3.zero;

                    StartCoroutine(DelayTime(entity));

                    timerManager.Remove("AttackFrequency");
                    timerManager.Start("AttackFrequency", attackFrequency * attackSpeedScale + delayTime);
                }
                break;

            case false:
                if (timerManager.IsFinished("AttackFrequency"))
                {
                    GameObject entity = ObjectPoolManager.Instance.GetObject(prefab, _blackboard.SpawnPosition, Quaternion.identity, parent);
                    entity.name = "Bullet(Clone)";
                    LaunchObject(entity);

                    timerManager.Remove("AttackFrequency");
                    timerManager.Start("AttackFrequency", attackFrequency * attackSpeedScale);
                }
                break;
        }

    }
    
    public void OnAttackExit(TowerStateBlackboard blackboard)
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

    private void Update()
    {

    }

    

    /*
    /// <summary>
    /// 生成攻击物
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="spawnPosition"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject InstantiateObject(GameObject prefab, Vector3 spawnPosition, Transform parent)
    {
        GameObject entity = Instantiate(prefab, spawnPosition, Quaternion.identity, parent);
        
        return entity;
    }
    */

    /// <summary>
    /// 发射攻击
    /// </summary>
    /// <param name="gameObject"></param>
    public void LaunchObject(GameObject gameObject)
    {
        gameObject.GetComponent<FlyerStraightController>().direction = _blackboard.attackDirection;
        gameObject.GetComponent<FlyerStraightController>().fly = true;
        //Debug.Log(_blackboard.attackDirection);

    }


    private IEnumerator DelayTime(GameObject entity)
    {
        yield return new WaitForSeconds(delayTime);
        LaunchObject(entity);
    }


}
