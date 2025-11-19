using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : IState
{

    private BuffStateManager _buffStateManager;

    private BuffFSM _fsm;

    public void OnEnter()
    {
        _buffStateManager.blackboard.Slow = _fsm.NowState[BuffState.Slow];
    }

    public void OnExit()
    {
     
    }

    public void OnUpdate()
    {
       
    }
}
