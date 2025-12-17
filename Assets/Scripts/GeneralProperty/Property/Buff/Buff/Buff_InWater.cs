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

        if (_blackboard.Burn == true)
        {
            _fsm.ExitState(BuffState.Burn);
        }

        if (gameObject.tag == "Enemy")
        {
            gameObject.GetComponent<EnemyProperty>().moveSpeed = 0.9f * gameObject.GetComponent<EnemyProperty>().moveSpeed;
        }
        _blackboard.InWater = true;
    }

    public void OnExit()
    {


        if (gameObject.tag == "Enemy")
        {
            gameObject.GetComponent<EnemyProperty>().moveSpeed = 1f / 0.9f * gameObject.GetComponent<EnemyProperty>().moveSpeed;
        }
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
