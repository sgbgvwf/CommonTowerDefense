using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Concorde.Timer;

public class Buff_Cold : IState
{
    private GameObject gameObject;

    private BuffBlackboard _blackboard;

    private BuffFSM _fsm;

    private TimerManager timerManager;

    public void Init(GameObject itself, BuffBlackboard buffBlackboard, BuffFSM buffFSM)
    {
        gameObject = itself;
        _blackboard = buffBlackboard;
        _fsm = buffFSM;
        timerManager = new TimerManager();
    }

    public void OnEnter()
    {
        if (_blackboard.Burn == true)
        {
            _fsm.ExitState(BuffState.Burn);
        }
        _blackboard.Cold = true;
        
    }

    public void OnExit()
    {
       _blackboard.Cold = false;
    }

    public void OnUpdate()
    {
        

    }
}
