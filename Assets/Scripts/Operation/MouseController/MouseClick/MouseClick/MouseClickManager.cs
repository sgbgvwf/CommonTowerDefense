using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class MouseClickManager : MonoBehaviour
{
    public InputController inputControl;

    public MouseRelativePosition mouseRelativePosition;

    [SerializeField]private MousePoint mousePoint;

    private FSM _fsm;



    private void Awake()
    {
        //blackboard = GetComponent<MouseBlackboard>();
        inputControl = new InputController();

        inputControl.ClickOperation.LeftClick.performed += LeftClick;
        inputControl.ClickOperation.RightClick.performed += RightClick;



    }

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        inputControl.Enable();
    }
    //禁用
    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {

    }

    public void LeftClick(InputAction.CallbackContext leftClick)
    {
        //mouseRelativePosition.enabled = true;
        //Debug.Log("leftClick.performed");
        if(mousePoint.blackboard.currentState == MousePointState.DefenseTower)
        {
            Debug.Log("拆除");
        }



    }

    public void RightClick(InputAction.CallbackContext rightClick)
    {
        //Debug.Log("rightClick.performed");
        if(mousePoint.blackboard.currentState == MousePointState.Place)
        {
            Debug.Log("建造");
        }

        if (mousePoint.blackboard.currentState == MousePointState.DefenseTower)
        {
            Debug.Log("查看");
        }




    }


}
