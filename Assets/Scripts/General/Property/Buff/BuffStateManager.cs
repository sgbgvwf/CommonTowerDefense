using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuffBlackboard : Blackboard
{
    //public BuffState currentState;

    public bool Burn;

    public bool Cold;

    public bool InWater;

    public bool Slow;

}

public class BuffStateManager : MonoBehaviour
{
    private BuffFSM _fsm;

    public BuffBlackboard blackboard;



    private void Start()
    {
        _fsm = new BuffFSM(blackboard);

        _fsm.AddState(BuffState.Burn, new Burn());

        _fsm.AddState(BuffState.Cold, new Cold());

        _fsm.AddState(BuffState.InWater, new InWater());

        _fsm.AddState(BuffState.Slow, new Slow());

    }


    private void Update()
    {

        _fsm.UpdateState();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag =="BurnDamage" && blackboard.InWater != true)
        {

            _fsm.EnterState(BuffState.Burn);

            Debug.Log("»º…’…À∫¶");
        }

        if (collision.tag == "ColdDamage")
        {

            _fsm.EnterState(BuffState.Cold);

            Debug.Log("∫Æ¿‰…À∫¶");
        }

        if (collision.tag == "Water")
        {

            _fsm.EnterState(BuffState.InWater);

            Debug.Log("‘⁄ÀÆ÷–");
        }







    }


    public void UpDateBlackboard()
    {
 

    }



}
