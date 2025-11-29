using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildDefenseTower : MonoBehaviour
{
    public SpriteRenderer mouseDisplay;


    public bool buildOperation;

    public void Build(GameObject prefab, Vector3 place)
    {
        if (!buildOperation)
        {
            buildOperation = true;

            mouseDisplay.color = new Color(0, 255/255f, 0, 100 / 255f);

            MousePositionDisplay.Instance.positionStatic = true;
        }
        else
        {
            if (Money.Instance.ChangeMoney(-1 * prefab.GetComponent<TowerPlaceMoney>().placementCost) && MousePositionDisplay.Instance.SamePosition())//-1减少
            {
                //建造防御塔
                Instantiate(prefab, place, quaternion.identity);
                //Debug.Log("建造成功");
                //mousePoint.blackboard.currentState = MousePointState.DefenseTower;//强制更新当前状态
                MousePointStateManager.Instance.TriggerReCheck();//更新检测实体
            }
            else
            {
                Debug.Log("金钱不足");
            }

            mouseDisplay.color = MousePointStateManager.Instance.blackboard.originalColor;

            buildOperation = false;

            MousePositionDisplay.Instance.positionStatic = false;
        }







    }




}
