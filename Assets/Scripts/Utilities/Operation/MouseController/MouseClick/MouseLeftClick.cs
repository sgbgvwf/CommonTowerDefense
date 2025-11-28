using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLeftClick : MonoBehaviour
{
    private static MouseLeftClick instance;
    public static MouseLeftClick Instance;

    //[SerializeField]private MousePointStateManager mousePoint;

    [Header("鼠标位置显示")]
    public SpriteRenderer mouseDisplay;

    [Header("关联脚本")]
    public DestroyDefenseTower destroyDefenseTower;

    public BuildDefenseTower buildDefenseTower;

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




    //点击左键
    public void LeftClick()
    {
        if (MousePointStateManager.Instance.blackboard.currentState == MousePointState.DefenseTower && MousePositionDisplay.Instance.SamePosition())//检测物体是防御塔
        {

            destroyDefenseTower.Destroy(MousePointStateManager.Instance.blackboard.currentTower);

        }

        //点空气
        else//检测位置与鼠标位置不符
        {
            MouseClickManager.Instance.ClickAirUpdate();

        }








    }
}
