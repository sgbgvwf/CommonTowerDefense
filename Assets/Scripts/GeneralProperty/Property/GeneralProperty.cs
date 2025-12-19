using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralProperty : MonoBehaviour
{
    public GeneralPropertySO generalPropertyData;
    public LevelDataSO levelData;
    private DataOperation dataOperation;

    //[Header("预制体")]
    [HideInInspector]public GameObject prefabReference;

    [Header("生命值")]
    public float health;
    [HideInInspector] public float initialHealth;
    [HideInInspector] public float maxHealth;

    [Header("攻击")]
    public float damage;
    public float attackFrequency;
    public float attackSpeedScale;
    public DamageType damageType;
    public BuffState buffType;
    public EffectType effectType;

    [Header("防御")]
    public float physicalDefense;
    public float magicalDefense;

    [Header("等级")]
    public int level;
    [HideInInspector] public int initLevel;
    [HideInInspector] public int MaxLevel;
    //[HideInInspector]public List<Level> levelList;
    //[HideInInspector]public List<LevelProperties> levelPropertiesList;

    private void Awake()
    {
        dataOperation = DataOperation.Instance;
        DataInitialization();

    }

    public void DataInitialization()
    {
        dataOperation.UpdateSingleData(ref prefabReference, generalPropertyData.prefabReference);

        dataOperation.UpdateSingleData(ref initialHealth, generalPropertyData.initialHealth);
        health = initialHealth;
        dataOperation.UpdateSingleData(ref maxHealth, generalPropertyData.maxHealth);

        dataOperation.UpdateSingleData(ref damage, generalPropertyData.damage);
        dataOperation.UpdateSingleData(ref attackFrequency, generalPropertyData.attackFrequency);
        dataOperation.UpdateSingleData(ref attackSpeedScale, generalPropertyData.attackSpeedScale);
        dataOperation.UpdateSingleData(ref damageType, generalPropertyData.damageType);
        dataOperation.UpdateSingleData(ref buffType, generalPropertyData.buffType);
        dataOperation.UpdateSingleData(ref effectType, generalPropertyData.effectType);

        dataOperation.UpdateSingleData(ref physicalDefense, generalPropertyData.physicalDefense);
        dataOperation.UpdateSingleData(ref magicalDefense, generalPropertyData.magicalDefense);

        dataOperation.UpdateSingleData(ref initLevel, levelData.Level.initLevel);
        level = initLevel;
        dataOperation.UpdateSingleData(ref MaxLevel, levelData.Level.maxLevel);

    }



}
