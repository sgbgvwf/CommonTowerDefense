using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;

public class MouseClickManager : MonoBehaviour
{
    private static MouseClickManager instance;
    public static MouseClickManager Instance;


    private FSM _fsm;

    public InputController inputControl;

    //public MouseRelativePosition mouseRelativePosition;




    //public Color originalColor;
    [Header("鼠标位置显示")]
    public SpriteRenderer mouseDisplay;

    [Header("左键关联脚本")]
    public DestroyDefenseTower destroyDefenseTower;

    [Header("右键关联脚本")]
    public BuildDefenseTower buildDefenseTower;

    public CheckDefenseTower checkDefenseTower;




    //public GameObject prefab;







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
        MouseLeftClick.Instance.LeftClick();
        //mouseRelativePosition.enabled = true;
        //Debug.Log("leftClick.performed");
    }

    public void RightClick(InputAction.CallbackContext rightClick)
    {
        MouseRightClick.Instance.RightClick();
        //Debug.Log("rightClick.performed");
    }


    public void ClickAirUpdate()
    {
        destroyDefenseTower.destroyOperation = false;//摧毁重置

        buildDefenseTower.buildOperation = false;//建造重置

        MousePositionDisplay.Instance.positionStatic = false;

        mouseDisplay.color = MousePointStateManager.Instance.blackboard.originalColor;

        //Debug.Log("重置");
    }

}
