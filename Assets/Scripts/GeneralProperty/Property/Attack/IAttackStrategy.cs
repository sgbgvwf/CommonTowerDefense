using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackStrategy
{

    void OnAttackEnter<T1, T2>(ref T1 blackboard, T2 attackDetection);


    void OnAttackUpdate<T1, T2>(ref T1 blackboard, T2 attackDetection);


    void OnAttackExit<T>(ref T blackboard);

}
