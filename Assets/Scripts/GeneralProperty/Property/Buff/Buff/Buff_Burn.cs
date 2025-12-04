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

    public float burnDamage;

    public void Init(GameObject itself, BuffBlackboard buffBlackboard, BuffFSM buffFSM)
    {
        gameObject = itself;
        _blackboard = buffBlackboard;
        _fsm = buffFSM;
        timerManager = new TimerManager();
        burnDamage = 5f;
        buffDamageInfomation = new DamageInfomation(burnDamage, DamageType.Magical, BuffState.None, gameObject);
    }

    public void OnEnter()
    {

        if (_blackboard.Cold == true)
        {
            _fsm.ExitState(BuffState.Cold);
        }


        timerManager.Start("BurnTimer", 5f);
        timerManager.Start("BurnDamage", 0f);
        _blackboard.Burn = true;
    }

    public void OnExit()
    {
        timerManager.Remove("BurnTimer");
        timerManager.Remove("BurnDamage");
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
