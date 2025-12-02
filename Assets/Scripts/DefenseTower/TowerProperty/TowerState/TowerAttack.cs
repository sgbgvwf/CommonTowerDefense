using Concorde.Timer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : IState
{
    private TowerStateBlackboard _blackboard;

    private EnemyDetection _enemyDetection;

    private ITowerAttackStrategy _attackStrategy;

    public void Init(TowerStateBlackboard blackboard, EnemyDetection enemyDetection, ITowerAttackStrategy attackStrategy)
    {
        _blackboard = blackboard;
        _enemyDetection = enemyDetection;
        _attackStrategy = attackStrategy;
    }

    public void OnEnter()
    {
        _attackStrategy?.OnAttackEnter(_blackboard, _enemyDetection);
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
