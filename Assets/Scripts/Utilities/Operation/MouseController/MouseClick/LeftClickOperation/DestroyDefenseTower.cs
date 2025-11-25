using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDefenseTower : MonoBehaviour
{
    [SerializeField]public MousePointStateManager mousePoint;

    public MousePositionDisplay positionDisplay;


    public SpriteRenderer mouseDisplay;


    //摧毁防御塔
    public bool destroyOperation;

    public void Destroy(GameObject destroyDefenseTower)
    {
        if (!destroyOperation)
        {
            destroyOperation = true;

            mouseDisplay.color = new Color(255 / 255f, 0, 0, 100 / 255f);

            positionDisplay.positionStatic = true;
        }
        else if(destroyOperation && positionDisplay.SamePosition())
        {
            //切实执行销毁
            float moneyBack = 0.8f * destroyDefenseTower.gameObject.GetComponent<TowerMoney>().placementCost;

            Money.Instance.ChangeMoney(moneyBack);//加钱必定成功，所以直接使用

            mouseDisplay.color = mousePoint.blackboard.originalColor;

            positionDisplay.positionStatic = false;

            GameObject.Destroy(destroyDefenseTower);

            destroyOperation = false;

            //重新检测当前位置
            mousePoint.TriggerReCheck();

        }
        else
        {
            Debug.Log("销毁取消");
        }









    }
}
