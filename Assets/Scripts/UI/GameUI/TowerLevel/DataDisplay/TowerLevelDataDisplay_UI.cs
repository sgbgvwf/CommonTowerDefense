using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class TowerLevelDataDisplay_UI : MonoBehaviour
{
    private GameObject _currentTower;
    private GeneralProperty _generalProperty;
    private LevelDataSO _levelData;

    private DataOperation dataOperation;

    private int _currentLevel;
    private LevelProperties _levelProperties_1;
    private int _nextLevel;
    private LevelProperties _levelProperties_2;

    [Header("属性展示组件")]
    public TextMeshProUGUI level;
    public TextMeshProUGUI levelUpCost;
    public GameObject levelUpButton;

    public TextMeshProUGUI maxHealth_1;

    public TextMeshProUGUI damage_1;
    public TextMeshProUGUI damageType_1;
    public TextMeshProUGUI buffType_1;
    public TextMeshProUGUI effectType_1;

    public TextMeshProUGUI physicalDefense_1;
    public TextMeshProUGUI magicalDefense_1;


    public TextMeshProUGUI maxHealth_2;

    public TextMeshProUGUI damage_2;
    public TextMeshProUGUI damageType_2;
    public TextMeshProUGUI buffType_2;
    public TextMeshProUGUI effectType_2;

    public TextMeshProUGUI physicalDefense_2;
    public TextMeshProUGUI magicalDefense_2;


    private void Awake()
    {
        dataOperation = DataOperation.Instance;
        CloseAllDisplay();
    }

    /// <summary>
    /// 更新当前的塔的状态
    /// </summary>
    public void CurrentTowerUpdate()
    {
        _currentTower = MousePointStateManager.Instance.blackboard.currentTower;//塔
        _generalProperty = _currentTower.GetComponent<GeneralProperty>();//属性

        _levelData = _generalProperty.levelData;//等级数据
        _currentLevel = _generalProperty.level;//等级
        _nextLevel = _currentLevel + 1;

        _levelProperties_1 = _levelData.levelPropertiesList.Find(p => p.level == _currentLevel);//等级对应的数据
        _levelProperties_2 = _levelData.levelPropertiesList.Find(p => p.level == _nextLevel);//等级对应的数据

        MaxLevelDetection();
    }

    /// <summary>
    /// 检测等级是否达到上限，是则关闭（下一级）,否则更新状态（两级）并开启（两级（下一级））
    /// </summary>
    private bool MaxLevelDetection()
    {
        if(_currentLevel == _generalProperty.MaxLevel)
        {
            CloseAllNextDisplay();
            CurrentLevelDataUpdate();

            return true;
        }

        OpenAllNextDisplay();
        CurrentLevelDataUpdate();
        NextLevelDataUpdate();

        return false;
    }

    //==========数据更新==========

    /// <summary>
    /// 数据更新：当前一级
    /// </summary>
    private void CurrentLevelDataUpdate()
    {
        level.text = _levelProperties_1.level.ToString();

        maxHealth_1.text = _levelProperties_1.maxHealth.ToString();

        damage_1.text = _levelProperties_1.damage.ToString();
        damageType_1.text = DamageTypeToString(_levelProperties_1.damageType);
        buffType_1.text = BuffTypeToString(_levelProperties_1.buffType);
        effectType_1.text = effectTypeToString(_levelProperties_1.effectType);

        physicalDefense_1.text = _levelProperties_1.physicalDefense.ToString();
        magicalDefense_1.text = _levelProperties_1.magicalDefense.ToString();

    }

    /// <summary>
    /// 数据更新：下一级
    /// </summary>
    private void NextLevelDataUpdate()
    {
        levelUpCost.text = _levelProperties_2.levelUpCost.ToString();

        maxHealth_2.text = _levelProperties_2.maxHealth.ToString();

        damage_2.text = _levelProperties_2.damage.ToString();
        damageType_2.text = DamageTypeToString(_levelProperties_2.damageType);
        buffType_2.text = BuffTypeToString(_levelProperties_2.buffType);
        effectType_2.text = effectTypeToString(_levelProperties_2.effectType);

        physicalDefense_2.text = _levelProperties_2.physicalDefense.ToString();
        magicalDefense_2.text = _levelProperties_2.magicalDefense.ToString();

    }

    //==========展示==========

    /// <summary>
    /// 禁用所有展示
    /// </summary>
    public void CloseAllDisplay()
    {
        CloseAllCurrentDisplay();
        CloseAllNextDisplay();
    }

    /// <summary>
    /// 启用所有展示
    /// </summary>
    public void OpenAllDisplay()
    {
        OpenAllCurrentDisplay();
        OpenAllNextDisplay();
    }

    /// <summary>
    /// 禁用下一级展示
    /// </summary>
    private void CloseAllNextDisplay()
    {
        levelUpButton.SetActive(false);//禁用升级按钮

        maxHealth_2.gameObject.SetActive(false);

        damage_2.gameObject.SetActive(false);
        damageType_2.gameObject.SetActive(false);
        buffType_2.gameObject.SetActive(false);
        effectType_2.gameObject.SetActive(false);

        physicalDefense_2.gameObject.SetActive(false);
        magicalDefense_2.gameObject.SetActive(false);
    }

    /// <summary>
    /// 禁用下一级展示
    /// </summary>
    private void OpenAllNextDisplay()
    {
        levelUpButton.SetActive(true);//启用升级按钮

        maxHealth_2.gameObject.SetActive(true);

        damage_2.gameObject.SetActive(true);
        damageType_2.gameObject.SetActive(true);
        buffType_2.gameObject.SetActive(true);
        effectType_2.gameObject.SetActive(true);

        physicalDefense_2.gameObject.SetActive(true);
        magicalDefense_2.gameObject.SetActive(true);
    }

    /// <summary>
    /// 禁用当前一级展示
    /// </summary>
    private void CloseAllCurrentDisplay()
    {
        maxHealth_1.gameObject.SetActive(false);

        damage_1.gameObject.SetActive(false);
        damageType_1.gameObject.SetActive(false);
        buffType_1.gameObject.SetActive(false);
        effectType_1.gameObject.SetActive(false);

        physicalDefense_1.gameObject.SetActive(false);
        magicalDefense_1.gameObject.SetActive(false);
    }

    /// <summary>
    /// 启用当前一级展示
    /// </summary>
    private void OpenAllCurrentDisplay()
    {
        maxHealth_1.gameObject.SetActive(true);

        damage_1.gameObject.SetActive(true);
        damageType_1.gameObject.SetActive(true);
        buffType_1.gameObject.SetActive(true);
        effectType_1.gameObject.SetActive(true);

        physicalDefense_1.gameObject.SetActive(true);
        magicalDefense_1.gameObject.SetActive(true);
    }

    //==========数据处理==========

    private string DamageTypeToString(DamageType damageType)
    {
        string name;
        switch (damageType)
        {
            case DamageType.Physical:
                name = "物理";
                break;
            default:
                name = "法术";
                break;
        }
        return name;
    }

    private string BuffTypeToString(BuffState buff)
    {
        string name;
        switch (buff)
        {
            case BuffState.None:
                name = "无";
                break;
            case BuffState.Burn:
                name = "灼";
                break;
            case BuffState.Cold:
                name = "寒";
                break;
            case BuffState.Slow:
                name = "缓";
                break;
            default:
                name = "无";
                break;
        }
        return name;
    }

    private string effectTypeToString(EffectType effectType)
    {
        string name;
        switch (effectType)
        {
            case EffectType.Instant:
                name = "否";
                break;
            default:
                name = "是";
                break;
        }
        return name;
    }






}
