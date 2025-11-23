using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class MouseClickManager : MonoBehaviour
{
    public InputController inputControl;

    //public MouseRelativePosition mouseRelativePosition;

    [SerializeField]private MousePointStateManager mousePoint;

    private FSM _fsm;


    public MouseLeftClick mouseLeftClick;

    public MouseRightClick mouseRightClick;


    public GameObject prefab;







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
    //½ûÓÃ
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
            Debug.Log("²ð³ý");
        }



    }

    public void RightClick(InputAction.CallbackContext rightClick)
    {
        //Debug.Log("rightClick.performed");
        if(mousePoint.blackboard.currentState == MousePointState.Place)
        {


            mouseRightClick.Build(prefab,new Vector3 (MouseRelativePosition.GetMouseGridPosition().x, MouseRelativePosition.GetMouseGridPosition().y, 0));
        }

        if (mousePoint.blackboard.currentState == MousePointState.DefenseTower)
        {
            Debug.Log("²é¿´");
        }




    }


}
