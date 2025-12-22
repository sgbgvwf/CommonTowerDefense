using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(DamageInfomation damage);
}

public class Hurt : MonoBehaviour, IDamageable
{
    private HealthOperation health;
    private GeneralProperty generalProperty;
    private BuffStateManager BuffStateManager;


    private void Awake()
    {
        health = GetComponent<HealthOperation>();
        generalProperty = GetComponent<GeneralProperty>();
        BuffStateManager = GetComponent<BuffStateManager>();
    }

    public void TakeDamage(DamageInfomation damage)
    {
        Debug.Log("hurt");
        //Debug.Log($"[帧{Time.frameCount}] 收到伤害，攻击者：{damage.source.name}");
        float finalDamage = damage.damageValue;

        GameObject attacker = damage.source;

        //伤害类型
        if(damage.damageType == DamageType.Physical)//物理伤害
        {
            if (finalDamage - generalProperty.physicalDefense < finalDamage * 0.05f)
            {
                finalDamage = finalDamage * 0.05f;
            }
            else
            {
                finalDamage -= generalProperty.physicalDefense;
            }

        }
        else if(damage.damageType == DamageType.Magical)//法术伤害
        {
            if (1 - generalProperty.magicalDefense/100 <0.05f)
            {
                finalDamage = finalDamage * 0.05f;
            }
            else
            {
                finalDamage *= (1 - generalProperty.magicalDefense / 100);
            }
        }

        //持续类型
        if(damage.effectType == EffectType.Instant)//单次伤害
        {

            health.ChangeHealth(-finalDamage);
            //Debug.Log(health.deltaHealth);

        }
        else if(damage.effectType == EffectType.Continuous)
        {




        }

        //Buff效果
        if(damage.buffType != BuffState.None)
        {
            if (damage.buffType == BuffState.Burn)
            {
                BuffStateManager.blackboard.buffFSM.EnterState(BuffState.Burn);

            }
            else if (damage.buffType == BuffState.Cold)
            {
                BuffStateManager.blackboard.buffFSM.EnterState(BuffState.Cold);
            }













        }
        



    }






}
