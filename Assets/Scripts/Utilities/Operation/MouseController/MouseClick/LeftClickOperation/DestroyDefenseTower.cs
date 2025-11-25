using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDefenseTower : MonoBehaviour
{
    [SerializeField]public MousePointStateManager mousePoint;

    public SpriteRenderer mousePositionDisplay;


    //摧毁防御塔
    public bool destroyOperation;

    public void Destroy(GameObject destroyDefenseTower)
    {
        if (!destroyOperation)
        {
            destroyOperation = true;

            mousePositionDisplay.color = new Color(255 / 255f, 0, 0, 100 / 255f);
        }
        else
        {
            //切实执行销毁
            float moneyBack = 0.8f * destroyDefenseTower.gameObject.GetComponent<TowerMoney>().placementCost;

            Money.Instance.ChangeMoney(moneyBack);

            mousePositionDisplay.color = mousePoint.blackboard.originalColor;

            GameObject.Destroy(destroyDefenseTower);

            destroyOperation = false;

            //重新检测当前位置
            mousePoint.TriggerReCheck();

        }










    }
}
