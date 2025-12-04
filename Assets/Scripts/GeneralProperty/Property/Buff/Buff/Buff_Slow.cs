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

    private float originalSpeed;

    public float SlowScale;

    public void Init(GameObject itself, BuffBlackboard buffBlackboard, BuffFSM buffFSM)
    {
        gameObject = itself;
        _blackboard = buffBlackboard;
        _fsm = buffFSM;
        timerManager = new TimerManager();
        originalSpeed = gameObject.GetComponent<EnemyMoveController>().moveSpeed;
    }
    public void OnEnter()
    {
        _blackboard.Slow = true;
        if(gameObject.tag == "Enemy")
        {
            gameObject.GetComponent<EnemyMoveController>().moveSpeed = SlowScale * gameObject.GetComponent<EnemyMoveController>().moveSpeed;
        }
    }

    public void OnExit()
    {
        _blackboard.Slow = false;
        if (gameObject.tag == "Enemy")
        {
            gameObject.GetComponent<EnemyMoveController>().moveSpeed = 1f / SlowScale * gameObject.GetComponent<EnemyMoveController>().moveSpeed;
        }
    }

    public void OnUpdate()
    {
       
    }
}
