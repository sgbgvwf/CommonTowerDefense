using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITowerAttackStrategy
{

    void OnAttackEnter(TowerStateBlackboard blackboard, AttackDetection attackDetection);


    void OnAttackUpdate(TowerStateBlackboard blackboard, AttackDetection attackDetection);


    void OnAttackExit(TowerStateBlackboard blackboard);

}
