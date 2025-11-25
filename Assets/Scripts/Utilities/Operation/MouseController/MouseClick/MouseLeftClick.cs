using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLeftClick : MonoBehaviour
{
    [SerializeField]private MousePointStateManager mousePoint;

    public MouseClickManager mouseClickManager;

    public SpriteRenderer mousePositionDisplay;

    [Header("关联脚本")]
    public DestroyDefenseTower destroyDefenseTower;








    //点击左键
    public void LeftClick()
    {
        if (mousePoint.blackboard.currentState == MousePointState.DefenseTower)//点击物体是防御塔
        {

            destroyDefenseTower.Destroy(mousePoint.blackboard.currentTower);

        }
        else//点击物体不是防御塔
        {
            destroyDefenseTower.destroyOperation = false;

            mousePositionDisplay.color = mousePoint.blackboard.originalColor;
        }








    }
}
