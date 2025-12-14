using System.Collections.Generic;
using UnityEngine;

// 可选：添加这个属性，防止在同一个 GameObject 上挂载多个该组件
[DisallowMultipleComponent]
public class BuildManager : MonoBehaviour
{
    private static BuildManager instance;
    public static BuildManager Instance;

    private DefenseTowerType selectedTowerType;

    [System.Serializable]
    public struct TowerPrefabEntry
    {
        public DefenseTowerType towerType;

        public GameObject towerPrefab;
    }

    [Header("塔预制体映射")]
    public List<TowerPrefabEntry> towerPrefabEntries;

    public Dictionary<DefenseTowerType, GameObject> towerPrefabDictionary;//用字典映射

    public bool HasTowerSelected;

    private DefenseTowerChoose_UI lastChoose;



    private void Awake()
    {
        if(instance == null)
        {
            Instance = this;
        }

        //建立映射
        towerPrefabDictionary = new Dictionary<DefenseTowerType, GameObject>();

        foreach (var entry in towerPrefabEntries)
        {
            if (!towerPrefabDictionary.ContainsKey(entry.towerType))
            {
                towerPrefabDictionary.Add(entry.towerType, entry.towerPrefab);
            }
        }
    }

    private void Start()
    {
        HasTowerSelected = false;

        selectedTowerType = DefenseTowerType.None;

        
    }


    //这个方法将被UI按钮调用，用于设置选中的塔类型
    public void SelectTowerToBuild(DefenseTowerType type, DefenseTowerChoose_UI defenseTowerChoose_UI)
    {

        LastFrameworkFade(defenseTowerChoose_UI);


        //Debug.Log("选中了塔类型: " + type);

        selectedTowerType = type;

        if (selectedTowerType != DefenseTowerType.None)
        {
            HasTowerSelected = true;
        }
        else
        {
            HasTowerSelected = false;
        }
    }

    //用于建造时获取当前选中的塔类型
    public DefenseTowerType GetSelectedTowerType()
    {
        return selectedTowerType;
    }


    public void LastFrameworkFade(DefenseTowerChoose_UI defenseTowerChoose_UI)
    {
        if (lastChoose != null)
        {
            lastChoose.ExitChoose();
        }

        lastChoose = defenseTowerChoose_UI;
    }


}