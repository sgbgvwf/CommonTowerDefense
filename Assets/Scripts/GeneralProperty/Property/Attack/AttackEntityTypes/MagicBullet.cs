using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    [Header("伤害")]
    public float attack;

    [Header("攻击类型")]
    public DamageType damageType;

    [Header("buff效果")]
    public BuffState buffType;

    [Header("持续类型")]
    public EffectType effectType;



    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.name);
        //Hurt enemy = other.GetComponent<Hurt>();
        IDamageable damageTarget = other.GetComponent<IDamageable>();

        if (other != null && other.tag == "Enemy")
        {
            DamageInfomation damageInfomation = new DamageInfomation(attack, damageType, buffType, gameObject);
            
            damageTarget.TakeDamage(damageInfomation);

            

             Destroy(gameObject);
            

        }

       

    }

















}
