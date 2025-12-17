using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackLockStrategy
{
    void AttackLockStrategyEnter(AttackLockBlackboard blackboard);


    void AttackLockStrategyUpdate(AttackLockBlackboard blackboard);


    void AttackLockStrategyExit(AttackLockBlackboard blackboard);
}