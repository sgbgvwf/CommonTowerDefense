using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAttackStrategy
{
    void OnAttackEnter(EnemyStateBlackboard blackboard, AttackDetection attackDetection);

    void OnAttackUpdate(EnemyStateBlackboard blackboard, AttackDetection attackDetection);

    void OnAttackExit(TowerStateBlackboard blackboard);

}