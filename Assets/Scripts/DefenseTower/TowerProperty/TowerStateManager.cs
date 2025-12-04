using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TowerStateBlackboard : Blackboard
{
    public TowerState currentState;

    public Vector3 firePosition;

}

public class TowerStateManager : MonoBehaviour
{

    private FSM _fsm;

    public TowerStateBlackboard blackboard;

    //public EnemyDetection enemyDetection;
    
    private ITowerAttackStrategy _attackStrategy;

    private AttackDetection attackDetection;

    private TowerState _currentState;


    void Start()
    {
        _fsm = new FSM(blackboard);

        TowerAttack attackState = new TowerAttack();

        _fsm.AddState(TowerState.Idle, new TowerIdle());

        _fsm.AddState(TowerState.Attack, attackState);

        //Debug.Log(_attackStrategy);

        //黑板数据初始化
        blackboard.currentState = TowerState.Idle;

        blackboard.firePosition = transform.position + new Vector3(0.5f, 0.5f, 0);

        attackDetection = GetComponent<AttackDetection>();

        _attackStrategy = GetComponent<AttackLaunch>();

        attackState.Init(blackboard, attackDetection, _attackStrategy);


        _fsm.SwitchState(TowerState.Idle);
    }


    private void Update()
    {
        //Debug.Log(blackboard.currentState);
        _fsm.UpdateState();

        if(blackboard.currentState == TowerState.Attack && _currentState != TowerState.Attack)
        {
            _fsm.SwitchState(TowerState.Attack);
            _currentState = TowerState.Attack;
        }
        else if(blackboard.currentState == TowerState.Idle && _currentState != TowerState.Idle)
        {
            _fsm.SwitchState(TowerState.Idle);
            _currentState = TowerState.Idle;
        }

        GetCurrentState();
    }


    public void GetCurrentState()
    {
        //Debug.Log(attackDetection);
        if (attackDetection.objectPosition.Count > 0)
        {
            blackboard.currentState = TowerState.Attack;
        }
        else if (attackDetection.objectPosition.Count == 0)
        {
            blackboard.currentState = TowerState.Idle;
        }
    }









}