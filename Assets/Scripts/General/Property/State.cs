using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class State : MonoBehaviour
{
    [Header("血量属性")]
    public float initialHealth;

    public float maxHealth;

    [Header("血量")]
    public float health;



    public float energy;


    public UnityEvent<Transform> OnTakeDamage;

    public UnityEvent<Transform> IsDead;




    private void Start()
    {
        health = initialHealth;
    }



    /*
    public void TakeDamege(Attack attacker)
    {

        if (health - attacker.damage > 0)//防止血量溢出为负数
        {
            health -= attacker.damage;//当前血量减去受到伤害

            //通知所有订阅了"OnTakeDamege"这个事件的监听者：受伤了
            OnTakeDamage?.Invoke(attacker.gameObject.transform);
        }
        else
        {
            health = 0;//死亡
            IsDead?.Invoke(attacker.gameObject.transform);


            Destroy(gameObject);
        }
    
    }
    */





}
