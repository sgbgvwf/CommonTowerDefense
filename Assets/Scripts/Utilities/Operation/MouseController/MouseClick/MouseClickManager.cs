using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;

public class MouseClickManager : MonoBehaviour
{
    public InputController inputControl;

    //public MouseRelativePosition mouseRelativePosition;

    [SerializeField]private MousePointStateManager mousePoint;

    private FSM _fsm;


    //public Color originalColor;


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
        //originalColor = new Color(255/255f, 255/255f, 132/255f, 100/255f);
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
        mouseLeftClick.LeftClick();
        //mouseRelativePosition.enabled = true;
        //Debug.Log("leftClick.performed");
    }

    public void RightClick(InputAction.CallbackContext rightClick)
    {


        //Debug.Log("rightClick.performed");
        if (mousePoint.blackboard.currentState == MousePointState.Place)
        {


            mouseRightClick.Build(prefab,new Vector3 (MouseRelativePosition.GetMouseGridPosition().x, MouseRelativePosition.GetMouseGridPosition().y, 0));
        }
        else if (mousePoint.blackboard.currentState == MousePointState.DefenseTower)
        {
            Debug.Log("²é¿´");
        }
        

    }




}
