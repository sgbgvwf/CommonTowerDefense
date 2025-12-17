using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyStateBlackboard : Blackboard
{
    public EnemyState currentState;

    public Vector3 firePosition;

}

public class EnemyStateManager : MonoBehaviour
{
    private FSM _fsm;

    public EnemyStateBlackboard blackboard;

    private IAttackStrategy _attackStrategy;
    private AttackLaunch _attackLaunch;
    private AttackDetection attackDetection;

    private EnemyState _currentState;

    private AttackLaunchTimer attackLaunchTimer;

    private void Start()
    {
        _fsm = new FSM(blackboard);

        EnemyIdle enemyIdle = new EnemyIdle();
        _fsm.AddState(EnemyState.Idle, enemyIdle);

        EnemyMove enemyMove = new EnemyMove();
        _fsm.AddState(EnemyState.Move, enemyMove);

        EnemyAttack enemyAttack = new EnemyAttack();
        _fsm.AddState(EnemyState.Attack, enemyAttack);
        enemyAttack.Init(blackboard, attackDetection, _attackStrategy);

        attackDetection = GetComponent<AttackDetection>();

        _attackStrategy = GetComponent<AttackLaunch>();
        _attackLaunch = GetComponent<AttackLaunch>();
        attackLaunchTimer = _attackLaunch._timer;


        //黑板数据初始化
        blackboard.currentState = EnemyState.Idle;

        blackboard.firePosition = transform.position;


        _fsm.SwitchState(EnemyState.Move);
    }


    private void Update()
    {
        _fsm.UpdateState();
        
        if(blackboard.currentState == EnemyState.Idle && _currentState != EnemyState.Idle)
        {
            _fsm.SwitchState(EnemyState.Idle);
            _currentState = EnemyState.Idle;
        }
        else if(blackboard.currentState == EnemyState.Attack && _currentState != EnemyState.Attack)
        {
            _fsm.SwitchState(EnemyState.Attack);
            _currentState = EnemyState.Attack;
        }
        else if(blackboard.currentState == EnemyState.Move && _currentState != EnemyState.Move)
        {
            _fsm.SwitchState(EnemyState.Move);
            _currentState = EnemyState.Move;
        }

        AttackDetectionTimer();

    }

    public void GetCurrentAttackState()
    {
        //Debug.Log(attackDetection);
        if (attackDetection.objectPosition.Count > 0)
        {
            blackboard.currentState = EnemyState.Attack;
        }
        else if (attackDetection.objectPosition.Count == 0)
        {
            blackboard.currentState = EnemyState.Move;
        }
    }

    /// <summary>
    /// 每隔一段时间（攻击间隔）检查一次是否进入攻击状态
    /// </summary>
    public void AttackDetectionTimer()
    {
        if (attackLaunchTimer.DetectTimer())
        {
            GetCurrentAttackState();
        }

    }



}
