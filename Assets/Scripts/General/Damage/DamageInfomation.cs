using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//伤害类型
public enum DamageType
{
    Physical,//物理

    Magical//法术

}

//效果类型
public enum EffectType
{
    Instant,//单次

    Continuous//持续
}


public class DamageInfomation
{
    public float damageValue;//伤害值

    public DamageType damageType;//伤害类型

    public EffectType effectType;//效果类型

    public GameObject source;//伤害来源

    public float duration;//持续时间


    public DamageInfomation(float value, DamageType type, GameObject src)
    {
        damageValue = value;
        damageType = type;
        effectType = EffectType.Instant;
        source = src;
        duration = 0;
    }


    public DamageInfomation(float valuePerSecond, DamageType type, float time, GameObject src)
    {
        damageValue = valuePerSecond;
        damageType = type;
        effectType = EffectType.Continuous;
        source = src;
        duration = time;
    }


}

