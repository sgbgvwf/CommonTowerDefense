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

    public float healthChange;

    //声明事件
    public UnityEvent<GameObject> OnTakeDamage;

    public UnityEvent<GameObject> IsDead;




    private void Start()
    {
        health = initialHealth;//初始化血量
    }


    //生命值减少
    public void ChangeHealth(float changeValue)
    {
        healthChange = changeValue;
        if(changeValue < 0)
        {
            if (health + healthChange > 0)
            {
                health += healthChange;
                OnTakeDamage?.Invoke(this.gameObject);

            }
            else if (health + healthChange < 0)
            {
                health = 0;
                IsDead?.Invoke(this.gameObject);

            }
        }
        else
        {
            if (health + healthChange >= maxHealth)
            {
                health = maxHealth;
            }
            else
            {
                health += healthChange;
            }
        }


    }



    public void HealthIncrease(GameObject Restorer)
    {



    }

}
