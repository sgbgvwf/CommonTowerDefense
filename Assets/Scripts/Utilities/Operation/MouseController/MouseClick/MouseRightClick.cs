using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRightClick : MonoBehaviour
{
    private static MouseRightClick instance;
    public static MouseRightClick Instance;

    [Header("可视化处理")]
    public SpriteRenderer mouseDisplay;

    [Header("防御塔管理器")]
    [SerializeField] private BuildManager buildManager;

    [Header("塔预制体映射")]
    [SerializeField] private List<TowerPrefabEntry> towerPrefabEntries;

    [Header("关联脚本")]
    public BuildDefenseTower buildDefenseTower;

    public CheckDefenseTower checkDefenseTower;

    public DestroyDefenseTower destroyDefenseTower;



    private Dictionary<DefenseTowerType, GameObject> towerPrefabDictionary;//用字典映射

    [System.Serializable]
    public struct TowerPrefabEntry
    {
        public DefenseTowerType towerType;

        public GameObject towerPrefab;
    }


    private void Awake()
    {
        if (instance == null)
        {
            Instance = this;
        }

    }

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
                Debug.Log("重复的塔类型: " + entry.towerType);
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


    //点击右键
    public void RightClick()
    {

        if (MousePointStateManager.Instance.blackboard.currentState == MousePointState.Place && MousePositionDisplay.Instance.SamePosition())//检测的是空地
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

                //建造
                buildDefenseTower.Build(towerBuildPrefab, buildPosition);


            }
        }



        else if (MousePointStateManager.Instance.blackboard.currentState == MousePointState.DefenseTower && MousePositionDisplay.Instance.SamePosition())//检测的是防御塔
        {
            checkDefenseTower.Check(MousePointStateManager.Instance.blackboard.currentTower);
        }

        //点空气
        else//检测位置与鼠标位置不符
        {
            MouseClickManager.Instance.ClickAirUpdate();

        }
    }
}