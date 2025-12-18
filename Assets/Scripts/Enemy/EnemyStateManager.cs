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


    private void Awake()
    {
        _fsm = new FSM(blackboard);

        _attackStrategy = GetComponent<AttackLaunch>();
        _attackLaunch = GetComponent<AttackLaunch>();
        attackDetection = GetComponent<AttackDetection>();
        attackLaunchTimer = _attackLaunch._timer;

        EnemyIdle enemyIdle = new EnemyIdle();
        _fsm.AddState(EnemyState.Idle, enemyIdle);

        EnemyMove enemyMove = new EnemyMove();
        _fsm.AddState(EnemyState.Move, enemyMove);

        EnemyAttack enemyAttack = new EnemyAttack();
        _fsm.AddState(EnemyState.Attack, enemyAttack);
        enemyAttack.Init(blackboard, attackDetection, _attackStrategy);


    }

    private void Start()
    {



        //黑板数据初始化
        blackboard.currentState = EnemyState.Move;

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

    private void OnEnable()
    {
        attackLaunchTimer.attackCircle += AttackDetectionTimer;
    }

    private void OnDisable()
    {
        attackLaunchTimer.attackCircle -= AttackDetectionTimer;
    }

    /// <summary>
    /// 每隔一段时间（攻击间隔）检查一次是否进入攻击状态
    /// </summary>
    public void AttackDetectionTimer()
    {

        
        GetCurrentAttackState();
        

    }

    public void GetCurrentAttackState()
    {
        //Debug.Log(attackDetection.objectPosition.Count);
        if (attackDetection.objectPosition.Count > 0)
        {
            blackboard.currentState = EnemyState.Attack;
        }
        else if (attackDetection.objectPosition.Count == 0)
        {
            blackboard.currentState = EnemyState.Move;
        }
    }





}
