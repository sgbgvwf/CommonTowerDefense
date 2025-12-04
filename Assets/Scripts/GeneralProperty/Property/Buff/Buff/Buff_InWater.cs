using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Concorde.Timer;


public class Buff_InWater : IState
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
        _blackboard.InWater = true;
        if (_blackboard.Burn == true)
        {
            _fsm.ExitState(BuffState.Burn);
        }
    }

    public void OnExit()
    {
        _blackboard.InWater = false;
    }

    public void OnUpdate()
    {
        if(_blackboard.Burn == true)
        {
            _fsm.ExitState(BuffState.Burn);
        }
    }
}
