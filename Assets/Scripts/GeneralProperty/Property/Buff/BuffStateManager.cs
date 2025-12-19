using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuffBlackboard : Blackboard
{
    //public BuffState currentState;
    public BuffFSM buffFSM;

    public bool Burn;

    public bool Cold;

    public bool InWater;

    public bool Slow;

}

public class BuffStateManager : MonoBehaviour
{
    private BuffFSM _fsm;

    public BuffBlackboard blackboard;

    private void Awake()
    {
        _fsm = new BuffFSM(blackboard);

        Buff_Burn buff_Burn = new Buff_Burn();
        _fsm.AddState(BuffState.Burn, buff_Burn);
        buff_Burn.Init(gameObject, blackboard, _fsm);

        Buff_Cold buff_Cold = new Buff_Cold();
        _fsm.AddState(BuffState.Cold, buff_Cold);
        buff_Cold.Init(gameObject, blackboard, _fsm);

        Buff_InWater buff_InWater = new Buff_InWater();
        _fsm.AddState(BuffState.InWater, buff_InWater);
        buff_InWater.Init(gameObject, blackboard, _fsm);

        Buff_Slow buff_Slow = new Buff_Slow();
        _fsm.AddState(BuffState.Slow, buff_Slow);
        buff_Slow.Init(gameObject, blackboard, _fsm);

        blackboard.buffFSM = _fsm;
    }

    private void Start()
    {


    }


    private void Update()
    {

        _fsm.UpdateStates();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.tag =="BurnDamage" && blackboard.InWater != true)
        {

            _fsm.EnterState(BuffState.Burn);

            Debug.Log("燃烧伤害");
        }

        if (collision.tag == "ColdDamage")
        {

            _fsm.EnterState(BuffState.Cold);

            Debug.Log("寒冷伤害");
        }
        */
        if (collision.tag == "Water")
        {

            _fsm.EnterState(BuffState.InWater);

            Debug.Log("在水中");
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {

            _fsm.ExitState(BuffState.InWater);

            Debug.Log("出水");
        }
    }





    public void UpDateBlackboard()
    {
 

    }



}
