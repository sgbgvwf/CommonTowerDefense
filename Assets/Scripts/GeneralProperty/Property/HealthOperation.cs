using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class HealthOperation : MonoBehaviour
{
    /*
    [Header("血量")]
    public float health;
    
    [Header("血量属性")]
    public float maxHealth;
    */
    //public SpriteRenderer WhiteImage;

    //public SpriteRenderer RedImage;

    private GeneralProperty GeneralProperty;

    //声明事件
    public UnityEvent OnTakeDamage;

    public UnityEvent IsDead;


    private void Awake()
    {

        GeneralProperty = GetComponent<GeneralProperty>();
        //maxHealth = GeneralProperty.maxHealth;

        //health = GeneralProperty.initialHealth;//初始化血量
    }


    //生命值减少
    public void ChangeHealth(float changeValue)
    {
        float health = GeneralProperty.health;
        float maxHealth = GeneralProperty.maxHealth;

        if(changeValue < 0)
        {
            if (health + changeValue > 0)
            {
                health += changeValue;
                GeneralProperty.health = health;
                OnTakeDamage?.Invoke();
                //HurtDisplay();

            }
            else if (health + changeValue < 0)
            {
                health = 0;
                GeneralProperty.health = health;
                IsDead?.Invoke();
                Destroy(gameObject);
            }
        }
        else
        {
            if (health + changeValue >= maxHealth)
            {
                health = maxHealth;
            }
            else
            {
                health += changeValue;
            }
        }


    }
    


}
