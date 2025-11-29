using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDefenseTower : MonoBehaviour
{

    public SpriteRenderer mouseDisplay;


    //摧毁防御塔
    public bool destroyOperation;

    public void Destroy(GameObject destroyDefenseTower)
    {
        if (!destroyOperation)
        {
            destroyOperation = true;

            mouseDisplay.color = new Color(255 / 255f, 0, 0, 100 / 255f);

            MousePositionDisplay.Instance.positionStatic = true;
        }
        else if(destroyOperation && MousePositionDisplay.Instance.SamePosition())
        {
            //切实执行销毁
            float moneyBack = 0.8f * destroyDefenseTower.gameObject.GetComponent<TowerPlaceMoney>().placementCost;

            Money.Instance.ChangeMoney(moneyBack);//加钱必定成功，所以直接使用

            mouseDisplay.color = MousePointStateManager.Instance.blackboard.originalColor;

            MousePositionDisplay.Instance.positionStatic = false;

            GameObject.Destroy(destroyDefenseTower);

            destroyOperation = false;

            //重新检测当前位置
            MousePointStateManager.Instance.TriggerReCheck();

        }
        else
        {
            Debug.Log("销毁取消");
        }









    }
}
