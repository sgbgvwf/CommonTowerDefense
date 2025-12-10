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
    private static MousePointStateManager instance;
    public static MousePointStateManager Instance;

    private FSM _fsm;
    
    public MouseBlackboard blackboard;

    private GameObject _currentTower;//当前防御塔

    private SpriteRenderer _mousePositionDisplay;

    private Collider2D _collider;

    private Vector2Int _mouseGridPosition;


    private void Awake()
    {
        if (instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("单例不单一！");
        }

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
        //Debug.Log(MousePointStateManager.Instance.blackboard.currentState);

        if (MouseChangeGrid())
        {
            TriggerReCheck();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DefenseTower"))
        {
            //切换到防御塔操作状态
            _fsm.SwitchState(MousePointState.DefenseTower);
            blackboard.currentState = MousePointState.DefenseTower;
            blackboard.currentTower = collision.gameObject;

            //_mousePositionDisplay.color = blackboard.originalColor;

            //Debug.Log(blackboard.currentState);
        }

        else if (collision.CompareTag("Place") && blackboard.currentTower == null)
        {
            //切换到可放置状态
            _fsm.SwitchState(MousePointState.Place);
            blackboard.currentState = MousePointState.Place;

            //_mousePositionDisplay.color = blackboard.originalColor;

            //Debug.Log(blackboard.currentState);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("DefenseTower") || collision.CompareTag("Place"))
        {
            //切换到空气（不可操作）状态

            _fsm.SwitchState(MousePointState.Air);
            blackboard.currentState = MousePointState.Air;
            blackboard.currentTower = null;

            //Debug.Log(blackboard.currentState);
        }
    }

    public void TriggerReCheck()
    {
        _collider.enabled = false;
        _collider.enabled = true;
        //Debug.Log(_collider.enabled);
    }

    public void ColorReSet()
    {
        _mousePositionDisplay.color = blackboard.originalColor;
    }

    private bool MouseChangeGrid()
    {
        if(_mouseGridPosition == MouseRelativePosition.Instance.mouseGridPosition)
        {
            _mouseGridPosition = MouseRelativePosition.Instance.mouseGridPosition;
            return false;
        }
        else
        {
            _mouseGridPosition = MouseRelativePosition.Instance.mouseGridPosition;
            return true;
        }
   
    }
}
