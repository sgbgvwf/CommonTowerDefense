using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Concorde.Timer;

public class Buff_Slow : IState
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
        _blackboard.Slow = true;
    }

    public void OnExit()
    {
        _blackboard.Slow = false;
    }

    public void OnUpdate()
    {
       
    }
}
