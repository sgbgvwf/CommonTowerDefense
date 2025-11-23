using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class MouseRightClick : MonoBehaviour
{









    public void Build(GameObject prefab, Vector3 place)
    {

        if (Money.Instance.ChangeMoney(-1 * prefab.GetComponent<TowerMoney>().placementCost))//-1减少
        {
            Instantiate(prefab, place, quaternion.identity);
            Debug.Log("建造成功");

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
