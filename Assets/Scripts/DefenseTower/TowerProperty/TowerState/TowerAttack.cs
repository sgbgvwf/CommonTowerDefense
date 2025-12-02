using Concorde.Timer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : IState
{
    private TowerStateBlackboard _blackboard;

    private EnemyDetection _enemyDetection;

    private IAttackStrategy _attackStrategy;

    public void Init(TowerStateBlackboard blackboard, EnemyDetection enemyDetection, IAttackStrategy attackStrategy)
    {
        _blackboard = blackboard;
        _enemyDetection = enemyDetection;
        _attackStrategy = attackStrategy;
        //Debug.Log(_attackStrategy);

    }

    public void OnEnter()
    {
        _attackStrategy?.OnAttackEnter(_blackboard, _enemyDetection);
        //Debug.Log(_attackStrategy);
    }

    public void OnExit()
    {
        _attackStrategy?.OnAttackExit(_blackboard);
    }

    public void OnUpdate()
    {
        _attackStrategy?.OnAttackUpdate(_blackboard, _enemyDetection);
    }


}
