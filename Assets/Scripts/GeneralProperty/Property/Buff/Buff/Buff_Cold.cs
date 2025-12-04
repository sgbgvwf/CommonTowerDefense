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

    private DamageInfomation buffDamageInfomation;

    public float coldDamage;

    public void Init(GameObject itself, BuffBlackboard buffBlackboard, BuffFSM buffFSM)
    {
        gameObject = itself;
        _blackboard = buffBlackboard;
        _fsm = buffFSM;
        timerManager = new TimerManager();
        coldDamage = 2f;
        buffDamageInfomation = new DamageInfomation(coldDamage, DamageType.Magical, BuffState.None, gameObject);
    }

    public void OnEnter()
    {
        if (_blackboard.Burn == true)
        {
            _fsm.ExitState(BuffState.Burn);
        }

        if (gameObject.tag == "Enemy" && !_blackboard.Cold)
        {
            gameObject.GetComponent<EnemyMoveController>().moveSpeed = 0.9f * gameObject.GetComponent<EnemyMoveController>().moveSpeed;
        }
        else if (gameObject.tag == "DefenseTower" && _blackboard.Cold)
        {
            //gameObject.GetComponent<AttackLaunch>().attackSpeedScale = 0.9f * gameObject.GetComponent<AttackLaunch>().attackSpeedScale;
        }

        timerManager.Start("ColdTimer", 5f);
        timerManager.Start("ColdDamage", 0f);

        _blackboard.Cold = true;
    }

    public void OnExit()
    {

        if (gameObject.tag == "Enemy" && _blackboard.Cold)
        {
            gameObject.GetComponent<EnemyMoveController>().moveSpeed = 1f / 0.9f * gameObject.GetComponent<EnemyMoveController>().moveSpeed;
        }
        else if(gameObject.tag == "DefenseTower" && _blackboard.Cold)
        {
            //gameObject.GetComponent<AttackLaunch>().attackSpeedScale = 0.9f * gameObject.GetComponent<AttackLaunch>().attackSpeedScale;
        }

        timerManager.Remove("ColdTimer");
        timerManager.Remove("ColdDamage");

        _blackboard.Cold = false;
    }

    public void OnUpdate()
    {
        if (_blackboard.InWater)
        {
            buffDamageInfomation.damageValue = 4f;
        }
        else
        {
            buffDamageInfomation.damageValue = 2f;
        }

        if (timerManager.IsFinished("ColdTimer"))
        {
            _fsm.ExitState(BuffState.Cold);
        }

        if (_blackboard.Burn == true)
        {
            if (timerManager.IsFinished("ColdDamage"))
            {
                gameObject.GetComponent<Hurt>().TakeDamage(buffDamageInfomation);

                timerManager.Start("ColdDamage", 1f);
            }
        }
    }
}
