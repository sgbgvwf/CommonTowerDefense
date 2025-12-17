using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Property/LevelDataSO")]

public class LevelDataSO : ScriptableObject
{
    public GameObject prefab;

    public Level Level = new Level();
    [SerializeField] public List<LevelProperties> levelPropertiesList = new List<LevelProperties>();
}

[System.Serializable]
public class Level
{
    public int initLevel;
    public int maxLevel;
}

[System.Serializable]
public class LevelProperties
{
    [Header("等级状态")]
    public int level;
    public float levelUpCost;

    [Header("生命值")]
    public float health; 
    public float maxHealth;

    [Header("攻击加成/属性")]
    public float damage;
    public float attackFrequency;
    public float attackSpeedScale;
    public DamageType damageType;
    public BuffState buffType;
    public EffectType effectType;

    [Header("防御加成")]
    public float physicalDefense;
    public float magicalDefense;
}
