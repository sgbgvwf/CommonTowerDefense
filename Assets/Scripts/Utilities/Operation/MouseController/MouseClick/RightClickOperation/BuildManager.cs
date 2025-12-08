using UnityEngine;

// 可选：添加这个属性，防止在同一个 GameObject 上挂载多个该组件
[DisallowMultipleComponent]
public class BuildManager : MonoBehaviour
{
    private static BuildManager instance;
    public static BuildManager Instance;

    private DefenseTowerType selectedTowerType;

    public bool HasTowerSelected;

    private DefenseTowerChoose_UI lastChoose;

    private void Awake()
    {
        if(instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        HasTowerSelected = false;

        selectedTowerType = DefenseTowerType.None;
    }


    // 这个方法将被 UI 按钮调用，用于设置选中的塔类型
    public void SelectTowerToBuild(DefenseTowerType type, DefenseTowerChoose_UI defenseTowerChoose_UI)
    {

        LastFrameworkFade(defenseTowerChoose_UI);


        Debug.Log("选中了塔类型: " + type);

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