using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttack : MonoBehaviour
{
    private AttackLockStrategyManager attackLockStrategyManager;
    private AttackLockType attackLockType;
    private FlyerStraightController flyerStraightController;
    //private DamageInfomation damageInfomation;

    private bool isAttack = false;

    private void Awake()
    {
        flyerStraightController = GetComponent<FlyerStraightController>();
    }

    /// <summary>
    /// 获取伤害属性
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    public GeneralProperty GetDamageProperties(GameObject resource)
    {
        GeneralProperty generalProperty = resource.GetComponent<GeneralProperty>();
        return generalProperty;
    }

    /// <summary>
    /// 碰撞时从源物体获取属性
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("1");
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

    /// <summary>
    /// 攻击进行
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="damageInfomation"></param>
    private void AttackObject(GameObject obj, DamageInfomation damageInfomation)
    {
        //Debug.Log("2");
        IDamageable damageTarget = obj.GetComponent<IDamageable>();
        attackLockType = attackLockStrategyManager.blackboard.attackLockType;
        //Debug.Log(attackLockType.targetType);
        //Debug.Log(attackLockType.itselfType);
        //Debug.Log($"[帧{Time.frameCount}] 进行伤害，攻击情况：{isAttack}");


        if (obj.tag == "Enemy" && attackLockType.targetType == EntityType.Enemy)
        {
            if (isAttack)
            {
                //Debug.Log("3");
                return;
            }
            else
            {
                isAttack = true;
                //Debug.Log("4");
            }
            Debug.Log($"[帧{Time.frameCount}] 进行伤害，攻击情况：{isAttack}");

            damageTarget.TakeDamage(damageInfomation);
            finalDeal();
        }
        else if (obj.tag == "DefenseTower" && attackLockType.targetType == EntityType.DefenseTower)
        {
            if (isAttack)
            {
                //Debug.Log("3");
                return;
            }
            else
            {
                isAttack = true;
                //Debug.Log("4");
            }
            damageTarget.TakeDamage(damageInfomation);
            finalDeal();
        }
        else
        {
            //Debug.Log(obj.tag);
            //Debug.Log(attackLockType.targetType);
            //Debug.Log("0");
        }
        //Debug.Log("5");
    }



    /// <summary>
    /// 飞行物的最终处理
    /// </summary>
    private void finalDeal()
    {
        //Debug.Log("6");
        FlyerStraightController flyerStraightController = gameObject.GetComponent<FlyerStraightController>();
        if (flyerStraightController != null)
        {
            flyerStraightController.direction = Vector3.zero;
            flyerStraightController.InitializeData();
        }

        ObjectPoolManager.Instance.ReturnObject(GetComponent<GeneralProperty>().prefabReference, gameObject);
        isAttack = false;
        //Debug.Log("return");
    }




}
