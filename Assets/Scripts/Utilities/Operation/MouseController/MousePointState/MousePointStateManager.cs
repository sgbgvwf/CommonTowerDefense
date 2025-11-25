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

    public Color originalColor;



}

public class MousePointStateManager : MonoBehaviour
{
    private FSM _fsm;
    
    public MouseBlackboard blackboard;

    private GameObject _currentTower;//µ±Ç°·ÀÓùËþ

    private SpriteRenderer _mousePositionDisplay;

    private Collider2D _collider;



    //public MousePointState currentState;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();

        _mousePositionDisplay = GetComponent<SpriteRenderer>();
    }


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

    public void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("DefenseTower"))
        {
            //ÇÐ»»µ½·ÀÓùËþ²Ù×÷×´Ì¬
            _fsm.SwitchState(MousePointState.DefenseTower);
            blackboard.currentState = MousePointState.DefenseTower;
            blackboard.currentTower = collision.gameObject;

            //_mousePositionDisplay.color = blackboard.originalColor;

            Debug.Log(blackboard.currentState);

        }

        else if (collision.CompareTag("Ground") && blackboard.currentTower == null)
        {

            //ÇÐ»»µ½¿É·ÅÖÃ×´Ì¬
            _fsm.SwitchState(MousePointState.Place);
            blackboard.currentState = MousePointState.Place;

            //_mousePositionDisplay.color = blackboard.originalColor;

            Debug.Log(blackboard.currentState);

        }

    }



    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DefenseTower") || collision.CompareTag("Ground"))
        {
            //ÇÐ»»µ½¿ÕÆø£¨²»¿É²Ù×÷£©×´Ì¬

            _fsm.SwitchState(MousePointState.Air);
            blackboard.currentState = MousePointState.Air;
            blackboard.currentTower = null;

            Debug.Log(blackboard.currentState);
        }


    }

    public void TriggerReCheck()
    {
        _collider.enabled = false;
        _collider.enabled = true;
    }

    public void ColorReSet()
    {
        _mousePositionDisplay.color = blackboard.originalColor;

    }


}
