using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildDefenseTower : MonoBehaviour
{
    [SerializeField] public MousePointStateManager mousePoint;

    public MousePositionDisplay positionDisplay;

    public MouseClickManager MouseClickManager;

    public SpriteRenderer mouseDisplay;


    public bool buildOperation;

    public void Build(GameObject prefab, Vector3 place)
    {
        if (!buildOperation)
        {
            buildOperation = true;

            mouseDisplay.color = new Color(0, 255/255f, 0, 100 / 255f);

            positionDisplay.positionStatic = true;
        }
        else
        {
            if (Money.Instance.ChangeMoney(-1 * prefab.GetComponent<TowerMoney>().placementCost) && positionDisplay.SamePosition())//-1减少
            {
                //建造防御塔
                Instantiate(prefab, place, quaternion.identity);
                //Debug.Log("建造成功");
                //mousePoint.blackboard.currentState = MousePointState.DefenseTower;//强制更新当前状态
                mousePoint.TriggerReCheck();//更新检测实体
            }
            else
            {
                Debug.Log("金钱不足");
            }

            mouseDisplay.color = mousePoint.blackboard.originalColor;

            buildOperation = false;

            positionDisplay.positionStatic = false;
        }







    }




}
