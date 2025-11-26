using UnityEngine;

// 可选：添加这个属性，防止在同一个 GameObject 上挂载多个该组件
[DisallowMultipleComponent]
public class BuildManager : MonoBehaviour
{
    private DefenseTowerType selectedTowerType;

    public bool HasTowerSelected;

    private void Start()
    {
        HasTowerSelected = false;

        selectedTowerType = DefenseTowerType.None;
    }


    // 这个方法将被 UI 按钮调用，用于设置选中的塔类型
    public void SelectTowerToBuild(DefenseTowerType type)
    {
        Debug.Log("选中了塔类型: " + type);

        // 1. 更新当前选中的塔类型
        selectedTowerType = type;

        // 2. 手动更新 HasTowerSelected 的值
        // 如果选中的类型不是 "None"，则表示有塔被选中
        if (selectedTowerType != DefenseTowerType.None)
        {
            HasTowerSelected = true;
        }
        else
        {
            HasTowerSelected = false;
        }
    }

    //用于获取当前选中的塔类型
    public DefenseTowerType GetSelectedTowerType()
    {
        return selectedTowerType;
    }


}