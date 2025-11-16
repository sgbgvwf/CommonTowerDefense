using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("ÉËº¦")]
    public float attack;

    [Header("¹¥»÷ÀàÐÍ")]
    public DamageType damageType;





    [Header("×Óµ¯´©Í¸")]
    public bool bulletCross;

    public float maxCrossTimes;

    public float currentCrossTimes;


    private void OnTriggerEnter2D(Collider2D other)
    {
        //Hurt enemy = other.GetComponent<Hurt>();

        if(other != null && other.tag == "Enemy")
        {

            DamageInfomation damageInfomation = new DamageInfomation(attack, damageType, gameObject);
            other.gameObject.GetComponent<Hurt>()?.TakeDamage(damageInfomation);

            currentCrossTimes++;
        }

        if(currentCrossTimes == maxCrossTimes && bulletCross)
        {
            Destroy(gameObject);
        }


    }

















}
