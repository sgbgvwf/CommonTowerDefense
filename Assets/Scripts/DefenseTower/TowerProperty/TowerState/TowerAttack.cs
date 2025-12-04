using Concorde.Timer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : IState
{
    private TowerStateBlackboard _blackboard;

    private AttackDetection _attackDetection;

    private ITowerAttackStrategy _attackStrategy;

    public void Init(TowerStateBlackboard blackboard, AttackDetection attackDetection, ITowerAttackStrategy attackStrategy)
    {
        _blackboard = blackboard;
        _attackDetection = attackDetection;
        _attackStrategy = attackStrategy;
        //Debug.Log(_attackStrategy);

    }

    public void OnEnter()
    {
        _attackStrategy?.OnAttackEnter(_blackboard, _attackDetection);
       
    }

    public void OnExit()
    {
        _attackStrategy?.OnAttackExit(_blackboard);
    }

    public void OnUpdate()
    {
        _attackStrategy?.OnAttackUpdate(_blackboard, _attackDetection);
        
    }

}
