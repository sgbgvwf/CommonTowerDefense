using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Concorde.Timer;

public class Buff_Burn : IState
{
    private GameObject gameObject;

    private BuffBlackboard _blackboard;

    private BuffFSM _fsm;

    private TimerManager timerManager;

    private DamageInfomation buffDamageInfomation;

    public void Init(GameObject itself, BuffBlackboard buffBlackboard, BuffFSM buffFSM)
    {
        gameObject = itself;
        _blackboard = buffBlackboard;
        _fsm = buffFSM;
        timerManager = new TimerManager();
        buffDamageInfomation = new DamageInfomation(5f, DamageType.Magical, BuffState.None, gameObject);
    }

    public void OnEnter()
    {

        if (_blackboard.Cold == true)
        {
            _fsm.ExitState(BuffState.Cold);
        }
        _blackboard.Burn = true;

        timerManager.Start("BurnTimer", 5f);
        timerManager.Start("BurnDamage", 0f);
    }

    public void OnExit()
    {
        _blackboard.Burn = false;
    }

    public void OnUpdate()
    {
        if (timerManager.IsFinished("BurnTimer"))
        {
            _fsm.ExitState(BuffState.Burn);
        }

        if(_blackboard.Burn == true)
        {
            if (timerManager.IsFinished("BurnDamage"))
            {
                gameObject.GetComponent<Hurt>().TakeDamage(buffDamageInfomation);

                timerManager.Start("BurnDamage", 1f);
            }
        }
        

    }





}
