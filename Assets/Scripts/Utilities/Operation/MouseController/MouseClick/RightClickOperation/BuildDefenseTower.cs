using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BuildDefenseTower : MonoBehaviour
{
    /*
    private static BuildDefenseTower instance;
    public static BuildDefenseTower Instance;
    */
    public SpriteRenderer mouseDisplay;


    /*
    [Header("防御塔管理器")]
    [SerializeField] private BuildManager buildManager;
    */


    public bool buildOperation;

    public Option_UI noMoney;

    public Option_UI noTower;
    /*
    private void Awake()
    {
        if(instance == null)
        {
            Instance = this;
        }
    }
    */
    private void Start()
    {

    }

    private GameObject GetTowerPrefab(DefenseTowerType towerType)
    {
        if (BuildManager.Instance.towerPrefabDictionary.TryGetValue(towerType, out GameObject prefab))
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
            DefenseTowerType towerSelectedType = BuildManager.Instance.GetSelectedTowerType();

            if (towerSelectedType == DefenseTowerType.None)
            {
                noTower.Display();
                //Debug.Log("请先选择一个防御塔再建造！");
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
                    GameObject newTower = Instantiate(GetTowerPrefab(towerSelectedType), buildPosition, quaternion.identity, transform);
                    newTower.name = GetTowerPrefab(towerSelectedType).name;
                    //Debug.Log("建造成功");
                    //mousePoint.blackboard.currentState = MousePointState.DefenseTower;//强制更新当前状态
                    MousePointStateManager.Instance.TriggerReCheck();//更新检测实体
                }
                else
                {
                    noMoney.Display();
                    //Debug.Log("金钱不足");
                }
            }

            mouseDisplay.color = MousePointStateManager.Instance.blackboard.originalColor;

            buildOperation = false;

            MousePositionDisplay.Instance.positionStatic = false;
        }

    }




}
