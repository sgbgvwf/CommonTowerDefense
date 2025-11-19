using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Burn : IState
{
    private BuffStateManager _buffStateManager;

    private BuffFSM _fsm;

    public void OnEnter()
    {
        _buffStateManager.blackboard.Burn = _fsm.NowState[BuffState.Burn];

    }

    public void OnExit()
    {
        
    }

    public void OnUpdate()
    {
        if (_buffStateManager.blackboard.Cold == true)
        {
            _fsm.ExitState(BuffState.Cold);
            _buffStateManager.blackboard.Cold = _fsm.NowState[BuffState.Cold];
        }

    }
}
