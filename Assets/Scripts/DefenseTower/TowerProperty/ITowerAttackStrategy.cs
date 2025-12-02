using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITowerAttackStrategy
{
    // 进入攻击状态时调用（初始化冷却、特效等）
    void OnAttackEnter(TowerStateBlackboard blackboard, EnemyDetection enemyDetection);

    // 攻击状态每帧更新（发射子弹、检测敌人等）
    void OnAttackUpdate(TowerStateBlackboard blackboard, EnemyDetection enemyDetection);

    // 退出攻击状态时调用（关闭特效、重置参数等）
    void OnAttackExit(TowerStateBlackboard blackboard);

}
