using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{
    private AttackLockStrategyManager attackLockStrategyManager;
    private AttackLockType attackLockType;
    private FlyerStraightController flyerStraightController;
    //private DamageInfomation damageInfomation;


    private void Awake()
    {
        flyerStraightController = GetComponent<FlyerStraightController>();
    }

    public GeneralProperty GetDamageProperties(GameObject resource)
    {
        GeneralProperty generalProperty = resource.GetComponent<GeneralProperty>();
        return generalProperty;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GeneralProperty generalProperty = GetDamageProperties(flyerStraightController.resource);
        DamageInfomation damageInfomation =
            new DamageInfomation(
                generalProperty.damage,
                generalProperty.damageType,
                generalProperty.buffType,
                flyerStraightController.resource);

        attackLockStrategyManager = flyerStraightController.resource.GetComponent<AttackLockStrategyManager>();

        AttackObject(other.gameObject, damageInfomation);


    }

    private void AttackObject(GameObject obj, DamageInfomation damageInfomation)
    {

        IDamageable damageTarget = obj.GetComponent<IDamageable>();
        attackLockType = attackLockStrategyManager.blackboard.attackLockType;
        //Debug.Log(attackLockType.targetType);
        //Debug.Log(attackLockType.itselfType);
        if (obj.tag == "Enemy" && attackLockType.targetType == EntityType.Enemy)
        {
            damageTarget.TakeDamage(damageInfomation);
            finalDeal();
        }
        else if (obj.tag == "DefenseTower" && attackLockType.targetType == EntityType.DefenseTower)
        {
            damageTarget.TakeDamage(damageInfomation);
            finalDeal();
        }
    }




    private void finalDeal()
    {
        FlyerStraightController flyerStraightController = gameObject.GetComponent<FlyerStraightController>();
        if (flyerStraightController != null)
        {
            flyerStraightController.direction = Vector3.zero;
            flyerStraightController.InitializeData();
        }
        ObjectPoolManager.Instance.ReturnObject(GetComponent<GeneralProperty>().prefabReference, gameObject);
        //Debug.Log("return");
    }




}
