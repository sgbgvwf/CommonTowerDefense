using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;



public class MouseClickManager : MonoBehaviour
{
    private FSM _fsm;

    public InputController inputControl;

    //public MouseRelativePosition mouseRelativePosition;

    [SerializeField]private MousePointStateManager mousePoint;


    //public Color originalColor;
    [Header("鼠标位置与显示")]
    public MouseRelativePosition mouseRelativePosition;

    public MousePositionDisplay mousePositionDisplay;

    public SpriteRenderer mouseDisplay;

    [Header("左右键脚本")]
    public MouseLeftClick mouseLeftClick;

    public MouseRightClick mouseRightClick;

    [Header("左键关联脚本")]
    public DestroyDefenseTower destroyDefenseTower;

    [Header("右键关联脚本")]
    public BuildDefenseTower buildDefenseTower;

    public CheckDefenseTower checkDefenseTower;




    //public GameObject prefab;







    private void Awake()
    {
        //blackboard = GetComponent<MouseBlackboard>();
        inputControl = new InputController();

        inputControl.ClickOperation.LeftClick.performed += LeftClick;
        inputControl.ClickOperation.RightClick.performed += RightClick;



    }

    void Start()
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


    public void LeftClick(InputAction.CallbackContext leftClick)
    {
        mouseLeftClick.LeftClick();
        //mouseRelativePosition.enabled = true;
        //Debug.Log("leftClick.performed");
    }

    public void RightClick(InputAction.CallbackContext rightClick)
    {
        mouseRightClick.RightClick();
        //Debug.Log("rightClick.performed");
    }


    public void ClickAirUpdate()
    {
        destroyDefenseTower.destroyOperation = false;//摧毁重置

        buildDefenseTower.buildOperation = false;//建造重置

        mousePositionDisplay.positionStatic = false;

        mouseDisplay.color = mousePoint.blackboard.originalColor;

        Debug.Log("重置");
    }

}
