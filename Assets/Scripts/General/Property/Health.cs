using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("血量属性")]
    public float initialHealth;

    public float maxHealth;

    [Header("血量")]
    public float health;

    public float deltaHealth;

    //声明事件
    public UnityEvent<GameObject> OnTakeDamage;

    public UnityEvent<GameObject> IsDead;




    private void Start()
    {
        health = initialHealth;//初始化血量
    }


    //生命值减少
    public void HealthDecrease(DamageInfomation damage)
    {
        if (health + deltaHealth > 0)
        {
            health += deltaHealth;
            OnTakeDamage?.Invoke(this.gameObject);

        }
        else if(health + deltaHealth < 0)
        {
            health = 0;
            IsDead?.Invoke(this.gameObject);

        }

    }



    public void HealthIncrease(GameObject Restorer)
    {
        if(health + deltaHealth >= maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += deltaHealth;
        }


    }

}
