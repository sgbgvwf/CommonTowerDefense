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

    public EnemyDetection enemyDetection;
    
    private ITowerAttackStrategy _attackStrategy;



    void Start()
    {
        _fsm = new FSM(blackboard);

        TowerAttack attackState = new TowerAttack();

        attackState.Init(blackboard, enemyDetection, _attackStrategy);

        _attackStrategy = GetComponent<MagicalTurretAttackStrategy>();

        blackboard.currentState = TowerState.Idle;

        blackboard.firePosition = transform.position + new Vector3(0.5f, 0.5f, 0);


        _fsm.AddState(TowerState.Idle, new TowerIdle());

        _fsm.AddState(TowerState.Attack, new TowerAttack());


        _fsm.SwitchState(TowerState.Idle);
    }


    private void Update()
    {
        _fsm.UpdateState();

        if(blackboard.currentState == TowerState.Attack)
        {
            _fsm.SwitchState(TowerState.Attack);
        }
        else if(blackboard.currentState == TowerState.Idle)
        {
            _fsm.SwitchState(TowerState.Idle);
        }
    }












}