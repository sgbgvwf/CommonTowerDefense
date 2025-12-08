using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildDefenseTower : MonoBehaviour
{
    public SpriteRenderer mouseDisplay;

    [Header("塔预制体映射")]
    [SerializeField] private List<TowerPrefabEntry> towerPrefabEntries;

    [Header("防御塔管理器")]
    [SerializeField] private BuildManager buildManager;

    private Dictionary<DefenseTowerType, GameObject> towerPrefabDictionary;//用字典映射

    [System.Serializable]
    public struct TowerPrefabEntry
    {
        public DefenseTowerType towerType;

        public GameObject towerPrefab;
    }


    public bool buildOperation;



    private void Start()
    {
        towerPrefabDictionary = new Dictionary<DefenseTowerType, GameObject>();

        foreach (var entry in towerPrefabEntries)
        {
            if (!towerPrefabDictionary.ContainsKey(entry.towerType))
            {
                towerPrefabDictionary.Add(entry.towerType, entry.towerPrefab);
            }
            else
            {
                //Debug.Log("重复的塔类型: " + entry.towerType);
            }
        }
    }


    private GameObject GetTowerPrefab(DefenseTowerType towerType)
    {
        if (towerPrefabDictionary.TryGetValue(towerType, out GameObject prefab))
        {
            return prefab;
        }
        Debug.Log("找不到塔预制体: " + towerType);
        return null;
    }




    public void Build()
    {
        if (!buildOperation)
        {
            buildOperation = true;

            mouseDisplay.color = new Color(0, 255/255f, 0, 100 / 255f);

            MousePositionDisplay.Instance.positionStatic = true;
        }
        else
        {
            DefenseTowerType towerSelectedType = buildManager.GetSelectedTowerType();

            if (towerSelectedType == DefenseTowerType.None)
            {
                Debug.Log("请先选择一个防御塔再建造！");
                return;
            }

            //获取预制体
            GameObject towerBuildPrefab = GetTowerPrefab(towerSelectedType);

            if (towerBuildPrefab != null)
            {
                //建造位置
                Vector3 buildPosition = new Vector3(
                    MouseRelativePosition.GetMouseGridPosition().x,
                    MouseRelativePosition.GetMouseGridPosition().y,
                    0
                );
                if (Money.Instance.ChangeMoney(-1 * GetTowerPrefab(towerSelectedType).GetComponent<TowerPlaceMoney>().placementCost) && MousePositionDisplay.Instance.SamePosition())//-1减少
                {
                    //建造防御塔
                    Instantiate(GetTowerPrefab(towerSelectedType), buildPosition, quaternion.identity);
                    //Debug.Log("建造成功");
                    //mousePoint.blackboard.currentState = MousePointState.DefenseTower;//强制更新当前状态
                    MousePointStateManager.Instance.TriggerReCheck();//更新检测实体
                }
                else
                {
                    Debug.Log("金钱不足");
                }
            }
            
            
            
            mouseDisplay.color = MousePointStateManager.Instance.blackboard.originalColor;

            buildOperation = false;

            MousePositionDisplay.Instance.positionStatic = false;
        }







    }




}
