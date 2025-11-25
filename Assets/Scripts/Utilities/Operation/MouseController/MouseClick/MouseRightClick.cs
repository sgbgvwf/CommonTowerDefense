using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class MouseRightClick : MonoBehaviour
{
    [SerializeField] private MousePointStateManager mousePoint;

    public MousePointStateManager mousePointStateManager;






    public void Build(GameObject prefab, Vector3 place)
    {

        if (Money.Instance.ChangeMoney(-1 * prefab.GetComponent<TowerMoney>().placementCost))//-1减少
        {
            Instantiate(prefab, place, quaternion.identity);
            Debug.Log("建造成功");
            mousePoint.blackboard.currentState = MousePointState.DefenseTower;//强制更新当前状态
            mousePointStateManager.TriggerReCheck();//更新检测实体
        }
        else
        {
            Debug.Log("建造不成功");
        }






    }


    public void Check()
    {






    }







}
