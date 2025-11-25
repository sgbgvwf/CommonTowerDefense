using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLeftClick : MonoBehaviour
{
    [SerializeField]private MousePointStateManager mousePoint;

    public MouseClickManager mouseClick;

    public MousePositionDisplay positionDisplay;

    public SpriteRenderer mouseDisplay;

    [Header("关联脚本")]
    public DestroyDefenseTower destroyDefenseTower;

    public BuildDefenseTower buildDefenseTower;






    //点击左键
    public void LeftClick()
    {
        if (mousePoint.blackboard.currentState == MousePointState.DefenseTower && positionDisplay.SamePosition())//检测物体是防御塔
        {

            destroyDefenseTower.Destroy(mousePoint.blackboard.currentTower);

        }

        //点空气
        else//检测位置与鼠标位置不符
        {
            mouseClick.ClickAirUpdate();

        }








    }
}
