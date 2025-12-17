using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyAttack : IState
{
    private EnemyStateBlackboard _blackboard;

    private AttackDetection _attackDetection;

    private IAttackStrategy _attackStrategy;

    public void Init(EnemyStateBlackboard blackboard, AttackDetection attackDetection, IAttackStrategy attackStrategy)
    {
        _blackboard = blackboard;
        _attackDetection = attackDetection;
        _attackStrategy = attackStrategy;
        //Debug.Log(_attackStrategy);

    }

    public void OnEnter()
    {
        _attackStrategy?.OnAttackEnter(ref _blackboard, _attackDetection);

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
