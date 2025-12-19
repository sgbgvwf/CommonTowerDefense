using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelDataSO levelDataSO;

    private GeneralProperty generalProperty;

    private DataOperation dataOperation;

    private Level _level;
    private List<LevelProperties> _levelPropertiesList;
    public LevelProperties _levelProperties;

    private int _currentLevel;

    private void Awake()
    {
        dataOperation = DataOperation.Instance;
        generalProperty = GetComponent<GeneralProperty>();
        _level = levelDataSO.Level;
        _levelPropertiesList = levelDataSO.levelPropertiesList;
        _levelProperties = _levelPropertiesList.Find(p => p.level == _currentLevel);
    }

    private void Start()
    {
        _currentLevel = _level.initLevel;
    }

    public bool LevelUp()
    {
        if(_currentLevel == _level.maxLevel)
        {
            //已满级
            return false;
        }

        var nextlevelProperties = _levelPropertiesList.Find(p => p.level == _currentLevel + 1);
        if (Money.Instance.ChangeMoney(-nextlevelProperties.levelUpCost))
        {
            //Debug.Log(_currentLevel);
            _currentLevel++;
            dataOperation.UpdateSingleData(ref generalProperty.level, nextlevelProperties.level);

            dataOperation.UpdateSingleData(ref generalProperty.health, nextlevelProperties.health);
            dataOperation.UpdateSingleData(ref generalProperty.maxHealth, nextlevelProperties.maxHealth);

            dataOperation.UpdateSingleData(ref generalProperty.damage, nextlevelProperties.damage);
            dataOperation.UpdateSingleData(ref generalProperty.attackFrequency, nextlevelProperties.attackFrequency);
            dataOperation.UpdateSingleData(ref generalProperty.attackSpeedScale, nextlevelProperties.attackSpeedScale);
            dataOperation.UpdateSingleData(ref generalProperty.damageType, nextlevelProperties.damageType);
            dataOperation.UpdateSingleData(ref generalProperty.buffType, nextlevelProperties.buffType);
            dataOperation.UpdateSingleData(ref generalProperty.effectType, nextlevelProperties.effectType);

            dataOperation.UpdateSingleData(ref generalProperty.physicalDefense, nextlevelProperties.physicalDefense);
            dataOperation.UpdateSingleData(ref generalProperty.magicalDefense, nextlevelProperties.magicalDefense);

            return true;
        }
        else
        {
            //金钱不足
            return false;
        }

    }



}
