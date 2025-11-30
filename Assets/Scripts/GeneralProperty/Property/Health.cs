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


    public SpriteRenderer WhiteImage;

    public SpriteRenderer RedImage;



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
        if(changeValue < 0)
        {
            if (health + changeValue > 0)
            {
                health += changeValue;
                OnTakeDamage?.Invoke(this.gameObject);
                HurtDisplay();

            }
            else if (health + changeValue < 0)
            {
                health = 0;
                IsDead?.Invoke(this.gameObject);

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



    public void HealthIncrease(GameObject Restorer)
    {



    }


    public void HurtDisplay()
    {


        StartCoroutine(HurtDisplayTime());

    }


    private IEnumerator HurtDisplayTime()
    {
        RedImage.enabled = true;

        yield return new WaitForSeconds(0.1f);

        RedImage.enabled = false;
    }
}
