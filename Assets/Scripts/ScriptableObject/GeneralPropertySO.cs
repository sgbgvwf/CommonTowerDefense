using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Property/GeneralPropertySO")]

public class GeneralPropertySO : ScriptableObject
{
    [Header("预制体")]
    public GameObject prefabReference;
    public LevelDataSO levelData;

    [Header("生命值")]
    public float health;
    public float initialHealth;
    public float maxHealth;

    [Header("基础攻击")]
    public float damage;
    public float attackFrequency;
    public float attackSpeedScale;
    public DamageType damageType;
    public BuffState buffType;
    public EffectType effectType;

    [Header("基础防御")]
    public float physicalDefense;
    public float magicalDefense;


    private void OnEnable()
    {
        LevelProperties level0 = levelData.levelPropertiesList.Find(p => p.level == 0);
        if (level0 != null)
        {
            levelData.levelPropertiesList.Remove(level0);
            AddLevel_0();
        }
        else
        {
            AddLevel_0();
        }
    }

    private void AddLevel_0()
    {
        LevelProperties levelProperties = new LevelProperties();

        levelProperties.level = 0;
        levelProperties.levelUpCost = 0;

        levelProperties.health = health;
        levelProperties.maxHealth = maxHealth;

        levelProperties.damage = damage;
        levelProperties.attackFrequency = attackFrequency;
        levelProperties.attackSpeedScale = attackSpeedScale;
        levelProperties.damageType = damageType;
        levelProperties.buffType = buffType;
        levelProperties.effectType = effectType;

        levelProperties.physicalDefense = physicalDefense;
        levelProperties.magicalDefense = magicalDefense;

        levelData.levelPropertiesList.Add(levelProperties);
    }

}
