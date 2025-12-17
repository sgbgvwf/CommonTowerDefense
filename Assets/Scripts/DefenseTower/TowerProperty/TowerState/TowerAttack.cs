using Concorde.Timer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : IState
{
    private TowerStateBlackboard _blackboard;

    private AttackDetection _attackDetection;

    private IAttackStrategy _attackStrategy;

    public void Init(TowerStateBlackboard blackboard, AttackDetection attackDetection, IAttackStrategy attackStrategy)
    {
        _blackboard = blackboard;
        _attackDetection = attackDetection;
        _attackStrategy = attackStrategy;
        //Debug.Log(_attackStrategy);

    }

    public void OnEnter()
    {
        _attackStrategy?.OnAttackEnter(ref _blackboard, _attackDetection);
        //Debug.Log("222");
    }

    public void OnExit()
    {
        _attackStrategy?.OnAttackExit(ref _blackboard);
    }

    public void OnUpdate()
    {
        _attackStrategy?.OnAttackUpdate(ref _blackboard, _attackDetection);
        
    }

}
