using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRightClick : MonoBehaviour
{
    private static MouseRightClick instance;
    public static MouseRightClick Instance;

    [Header("可视化处理")]
    public SpriteRenderer mouseDisplay;




    [Header("关联脚本")]
    public BuildDefenseTower buildDefenseTower;

    public CheckDefenseTower checkDefenseTower;

    public DestroyDefenseTower destroyDefenseTower;



    


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

    }

    private void Start()
    {

    }


    //点击右键
    public void RightClick()
    {

        if (MousePointStateManager.Instance.blackboard.currentState == MousePointState.Place && MousePositionDisplay.Instance.SamePosition())//检测的是空地
        {
            //建造
            buildDefenseTower.Build();
            
        }



        else if (MousePointStateManager.Instance.blackboard.currentState == MousePointState.DefenseTower && MousePositionDisplay.Instance.SamePosition())//检测的是防御塔
        {
            checkDefenseTower.Check(MousePointStateManager.Instance.blackboard.currentTower);
        }

        //点空气
        else//检测位置与鼠标位置不符
        {
            MouseClickManager.Instance.ClickAirUpdate();

        }
    }
}