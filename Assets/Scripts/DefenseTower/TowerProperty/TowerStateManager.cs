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
    
    private IAttackStrategy _attackStrategy;

    private AttackDetection attackDetection;

    private TowerState _currentState;


    void Start()
    {
        _fsm = new FSM(blackboard);

        attackDetection = GetComponent<AttackDetection>();
        _attackStrategy = GetComponent<AttackLaunch>();

        TowerAttack towerAttack = new TowerAttack();
        _fsm.AddState(TowerState.Attack, towerAttack);
        towerAttack.Init(blackboard, attackDetection, _attackStrategy);

        _fsm.AddState(TowerState.Idle, new TowerIdle());

        //黑板数据初始化
        blackboard.currentState = TowerState.Idle;

        blackboard.firePosition = transform.position + new Vector3(0.5f, 0.5f, 0);

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

        GetCurrentAttackState();
        //Debug.Log(blackboard.currentState);
    }


    public void GetCurrentAttackState()
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