using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum AttackLockStrategy
{
    PathNearest,
    DistanceNearest,
    HealthMinimum
}

[Serializable]
public class AttackLockBlackboard : Blackboard
{
    public EntityType itselfType;//自身类型

    public EntityType targetType;//目标类型

    public AttackLockStrategy strategy;//索敌策略

    public bool delayAttack;//延迟攻击

    [HideInInspector]public bool lockEnemy;

    [HideInInspector]public Vector3 SpawnPosition;//攻击生成位置

    [HideInInspector]public Vector3 attackDirection;//攻击方向

    [HideInInspector]public AttackDetection attackDetection;
}

public class AttackLockStrategyManager : MonoBehaviour
{
    private FSM _fsm;

    public AttackLockBlackboard blackboard;

    private AttackDetection _attackDetection;

    //当前锁定方式
    private AttackLockStrategy _currentStrategy;

    private void Awake()
    {
        _fsm = new FSM(blackboard);

        AttackLockStrategy_PathNearest attackLockStrategy_PathNearest = new AttackLockStrategy_PathNearest();
        _fsm.AddState(AttackLockStrategy.PathNearest, attackLockStrategy_PathNearest);
        attackLockStrategy_PathNearest.Init(blackboard);

        AttackLockStrategy_DistanceNearest attackLockStrategy_DistanceNearest = new AttackLockStrategy_DistanceNearest();
        _fsm.AddState(AttackLockStrategy.DistanceNearest, attackLockStrategy_DistanceNearest);
        attackLockStrategy_DistanceNearest.Init(blackboard);

        AttackLockStrategy_HealthMinimum attackLockStrategy_HealthMinimum = new AttackLockStrategy_HealthMinimum();
        _fsm.AddState(AttackLockStrategy.HealthMinimum, attackLockStrategy_HealthMinimum);
        attackLockStrategy_HealthMinimum.Init(blackboard);

        _attackDetection = GetComponent<AttackDetection>();


        if(blackboard.targetType == EntityType.DefenseTower && blackboard.strategy == AttackLockStrategy.PathNearest)
        {
            blackboard.strategy = AttackLockStrategy.DistanceNearest;
        }

        if(blackboard.itselfType == EntityType.DefenseTower)
        {
            blackboard.SpawnPosition = transform.position + new Vector3(0.5f, 0.5f, 0);
        }
        else if(blackboard.itselfType == EntityType.Enemy)
        {
            blackboard.SpawnPosition = transform.position;

        }

        blackboard.attackDetection = _attackDetection;
        //Debug.Log(_currentStrategy);
        _currentStrategy = blackboard.strategy;

        _fsm.SwitchState(_currentStrategy);
    }

    //不应该用update转换，应该是接收事件转换
    //暂时先用着吧
    //黑板数值决定目前状态
    private void Update()
    {
        //Debug.Log(blackboard.strategy);
        _fsm.UpdateState();

        if(blackboard.strategy == AttackLockStrategy.PathNearest && _currentStrategy != AttackLockStrategy.PathNearest)
        {
            _fsm.SwitchState(AttackLockStrategy.PathNearest);
            _currentStrategy = AttackLockStrategy.PathNearest;
        }
        else if(blackboard.strategy == AttackLockStrategy.DistanceNearest && _currentStrategy != AttackLockStrategy.DistanceNearest)
        {
            _fsm.SwitchState(AttackLockStrategy.DistanceNearest);
            _currentStrategy = AttackLockStrategy.DistanceNearest;
        }
        else if(blackboard.strategy == AttackLockStrategy.HealthMinimum && _currentStrategy != AttackLockStrategy.HealthMinimum)
        {
            _fsm.SwitchState(AttackLockStrategy.HealthMinimum);
            _currentStrategy = AttackLockStrategy.HealthMinimum;
        }

        

    }
    

}
