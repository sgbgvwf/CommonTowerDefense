using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Cold : IState
{

    private BuffStateManager _buffStateManager;

    private BuffFSM _fsm;

    public void OnEnter()
    {
        _buffStateManager.blackboard.Cold = _fsm.NowState[BuffState.Cold];
        if (_buffStateManager.blackboard.Burn == true)
        {
            _fsm.ExitState(BuffState.Burn);
            _buffStateManager.blackboard.Burn = _fsm.NowState[BuffState.Burn];
        }
    }

    public void OnExit()
    {
       
    }

    public void OnUpdate()
    {
        if (_buffStateManager.blackboard.Burn == true)
        {
            _fsm.ExitState(BuffState.Burn);
            _buffStateManager.blackboard.Burn = _fsm.NowState[BuffState.Burn];
        }

    }
}
