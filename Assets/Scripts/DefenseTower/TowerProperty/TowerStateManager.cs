using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TowerStateBlackboard : Blackboard
{
    public TowerState currentState;

    public Vector3 firePosition;

    public AttackTimeType attackTimeType;

}

public class TowerStateManager : MonoBehaviour
{

    private FSM _fsm;

    public TowerStateBlackboard blackboard;

    public EnemyDetection enemyDetection;
    
    private IAttackStrategy _attackStrategy;

    private TowerState _currentState;


    void Start()
    {
        _fsm = new FSM(blackboard);

        TowerAttack attackState = new TowerAttack();

        _fsm.AddState(TowerState.Idle, new TowerIdle());

        _fsm.AddState(TowerState.Attack, attackState);

        _attackStrategy = GetComponent<SingleAttackStrategy>();

        //Debug.Log(_attackStrategy);

        blackboard.currentState = TowerState.Idle;

        blackboard.firePosition = transform.position + new Vector3(0.5f, 0.5f, 0);

        attackState.Init(blackboard, enemyDetection, _attackStrategy);





        _fsm.SwitchState(TowerState.Idle);
    }


    private void Update()
    {
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
    }












}