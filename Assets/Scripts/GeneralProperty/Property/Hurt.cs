using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(DamageInfomation damage);
}

public class Hurt : MonoBehaviour, IDamageable
{
    private Health health;

    private DefenseProperty defense;

    private BuffStateManager BuffStateManager;


    private void Awake()
    {
        health = GetComponent<Health>();

        defense = GetComponent<DefenseProperty>();

        BuffStateManager = GetComponent<BuffStateManager>();
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

            }













        }
        



    }






}
