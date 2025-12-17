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
    public BuffState buffType;//元素类型
    public EffectType effectType;//效果类型
    public GameObject source;//伤害来源
    public float duration;//持续时间

    /// <summary>
    /// 瞬时伤害
    /// </summary>
    /// <param name="value">伤害值</param>
    /// <param name="type">伤害类型</param>
    /// <param name="src">伤害来源</param>
    public DamageInfomation(float value, DamageType type, BuffState buff, GameObject src)
    {
        damageValue = value;
        damageType = type;
        buffType = buff;
        effectType = EffectType.Instant;
        source = src;
        duration = 0;
    }

    /// <summary>
    /// 持续伤害
    /// </summary>
    /// <param name="valuePerSecond">每秒伤害</param>
    /// <param name="type">伤害类型</param>
    /// <param name="time">持续时间</param>
    /// <param name="src">伤害来源</param>
    public DamageInfomation(float valuePerSecond, DamageType type, BuffState buff, float time, GameObject src)
    {
        damageValue = valuePerSecond;
        damageType = type;
        buffType = buff;
        effectType = EffectType.Continuous;
        source = src;
        duration = time;
    }


}

