using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackStrategy
{

    void OnAttackEnter(TowerStateBlackboard blackboard, EnemyDetection enemyDetection);


    void OnAttackUpdate(TowerStateBlackboard blackboard, EnemyDetection enemyDetection);


    void OnAttackExit(TowerStateBlackboard blackboard);

}
