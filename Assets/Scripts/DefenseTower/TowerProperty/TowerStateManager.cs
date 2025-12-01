using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TowerStateBlackboard : Blackboard
{
    public TowerState currentState;

    public Vector3 currentAttackDirection;
}

public class TowerStateManager : MonoBehaviour
{

    private FSM _fsm;

    public TowerStateBlackboard blackboard;

    public EnemyDetection enemyDetection;
    




    void Start()
    {
        _fsm = new FSM(blackboard);

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