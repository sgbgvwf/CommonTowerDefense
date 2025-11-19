using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using System;

[Serializable]
public class MouseBlackboard : Blackboard
{

    public MousePointState currentState;

    public GameObject currentTower;

    
}

public class MousePoint : MonoBehaviour
{
    private FSM _fsm;
    
    public MouseBlackboard blackboard;

    private GameObject _currentTower;//µ±Ç°·ÀÓùËþ

    


    //public MousePointState currentState;

    void Start()
    {
        _fsm = new FSM(blackboard);

        _fsm.AddState(MousePointState.Air, new PointAirState());

        _fsm.AddState(MousePointState.Place, new PointPlaceState());

        _fsm.AddState(MousePointState.DefenseTower, new PointDefenseTowerState());


        _fsm.SwitchState(MousePointState.Place);
    }



    private void Update()
    {

        _fsm.UpdateState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("DefenseTower"))
        {
            //ÇÐ»»µ½·ÀÓùËþ²Ù×÷×´Ì¬
            blackboard.currentTower = collision.gameObject;
            _fsm.SwitchState(MousePointState.DefenseTower);
            blackboard.currentState = MousePointState.DefenseTower;

            Debug.Log(blackboard.currentState);

        }

        else if (collision.CompareTag("Ground") && _currentTower == null)
        {
            //ÇÐ»»µ½¿É·ÅÖÃ×´Ì¬
            _fsm.SwitchState(MousePointState.Place);
            blackboard.currentState = MousePointState.Place;

            Debug.Log(blackboard.currentState);

        }

    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DefenseTower") || collision.CompareTag("Ground"))
        {
            //ÇÐ»»µ½¿ÕÆø£¨²»¿É²Ù×÷£©×´Ì¬
            _fsm.SwitchState(MousePointState.Air);
            blackboard.currentState = MousePointState.Air;

            Debug.Log(blackboard.currentState);

        }
    }



}
