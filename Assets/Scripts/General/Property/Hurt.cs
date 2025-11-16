using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour
{
    private Health health;

    private DefenseProperty defense;




    private void Awake()
    {
        health = GetComponent<Health>();

        defense = GetComponent<DefenseProperty>();
    }



    public void TakeDamage(DamageInfomation damage)
    {
        float finalDamage = damage.damageValue;

        GameObject attacker = damage.source;

        //伤害类型
        if(damage.damageType == DamageType.Physical)//物理伤害
        {
            if (finalDamage - defense.physicalDefense < finalDamage * 0.05f)
            {
                finalDamage = finalDamage * 0.05f;
            }
            else
            {
                finalDamage -= defense.physicalDefense;
            }

        }
        else if(damage.damageType == DamageType.Magical)//法术伤害
        {
            if (1 - defense.magicalDefense/100 <0.05f)
            {
                finalDamage = finalDamage * 0.05f;
            }
            else
            {
                finalDamage *= (1 - defense.magicalDefense / 100);
            }
        }


        //持续类型
        if(damage.effectType == EffectType.Instant)//单次伤害
        {
            health.deltaHealth = -finalDamage;//负号最后处理
            health.HealthDecrease(attacker);
            health.deltaHealth = 0;//确保伤害只有一次
        }






    }






}
