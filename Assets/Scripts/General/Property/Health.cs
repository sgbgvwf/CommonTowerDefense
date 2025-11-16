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
    public UnityEvent<Transform> OnTakeDamage;

    public UnityEvent<Transform> IsDead;




    private void Start()
    {
        health = initialHealth;//初始化血量
    }


    //生命值减少
    public void HealthDecrease(GameObject attacker)
    {
        if (health + deltaHealth > 0)
        {
            OnTakeDamage?.Invoke(attacker.gameObject.transform);

        }
        else if(health + deltaHealth < 0)
        {
            IsDead?.Invoke(attacker.gameObject.transform);

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
