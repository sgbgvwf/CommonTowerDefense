using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    /*
    //[Header("攻击者")]
    private GameObject attacker;

    [Header("伤害")]
    public float attack;

    [Header("攻击类型")]
    public DamageType damageType;

    [Header("buff效果")]
    public BuffState buffType;

    [Header("持续类型")]
    public EffectType effectType;
    */
    private FlyerStraightController flyerStraightController;
    private DamageInfomation damageInfomation;
    /*
    private void Awake()
    {
        flyerStraightController = GetComponent<FlyerStraightController>();
    }

    public void GetDamageProperties(GameObject resource)
    {
        GeneralProperty generalProperty = resource.GetComponent<GeneralProperty>();
        damageInfomation = new DamageInfomation(generalProperty.damage, generalProperty.damageType, generalProperty.buffType, resource);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GetDamageProperties(flyerStraightController.resource);
        //Debug.Log(other.name);
        //Hurt enemy = other.GetComponent<Hurt>();
        IDamageable damageTarget = other.GetComponent<IDamageable>();

        if (other != null && other.tag == "Enemy")
        {
            
            damageTarget.TakeDamage(damageInfomation);

            //Debug.Log(damageInfomation.buffType);

            finalDeal();
            

        }

    }

    private void finalDeal()
    {
        if (gameObject.GetComponent<FlyerStraightController>())
        {
            gameObject.GetComponent<FlyerStraightController>().direction = Vector3.zero;
            gameObject.GetComponent<FlyerStraightController>().InitializeData();
        }
        ObjectPoolManager.Instance.ReturnObject(GetComponent<GeneralProperty>().prefabReference, gameObject);
    }
    */




}
