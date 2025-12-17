using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

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


    public List<Collider2D> collider2Ds;

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

        //collider2Ds = new List<Collider2D>();

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
        if (ScreenPositionAllowClick())
        {
            MouseLeftClick.Instance.LeftClick();
        }

        //mouseRelativePosition.enabled = true;
        //Debug.Log("leftClick.performed");
    }

    public void RightClick(InputAction.CallbackContext rightClick)
    {
        if (ScreenPositionAllowClick())
        {
            MouseRightClick.Instance.RightClick();

        }
        //Debug.Log("rightClick.performed");
    }


    public void ClickAirUpdate()
    {
        destroyDefenseTower.destroyOperation = false;//摧毁重置

        buildDefenseTower.buildOperation = false;//建造重置

        //检查重置
        if (checkDefenseTower.checkOperation)
        {
            checkDefenseTower.CheckReset();
        }


        MousePositionDisplay.Instance.positionStatic = false;

        mouseDisplay.color = MousePointStateManager.Instance.blackboard.originalColor;

        //Debug.Log("重置");
    }


    public bool ScreenPositionAllowClick()
    {
        bool click = true;

        if(collider2Ds.Count > 0)
        {
            foreach (var collider in collider2Ds)
            {
                //Debug.Log("1");
                if (collider.OverlapPoint(MouseRelativePosition.Instance.mouseScreenPosition))
                {
                    // 鼠标在边界内 → 执行你的点击逻辑
                    //Debug.Log("鼠标在边界内，不触发点击");
                    // 替换为你的代码：比如启用下层物体选择、执行level 1点击逻辑等
                    click = false;
                }
                else
                {
                    // 鼠标在边界外 → 不执行任何点击逻辑
                    //Debug.Log("鼠标在边界外，触发点击");
                }

            }
        }
        
        if (click)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}
