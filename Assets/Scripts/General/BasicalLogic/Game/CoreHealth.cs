using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoreHealth : MonoBehaviour
{
    public int coreHealth;



    public UnityEvent<GameObject> HealthReduction;

    public UnityEvent<GameObject> Death;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        CoreHealthReduce(collision.tag);

    }


    public void CoreHealthReduce(string name)
    {
        if(coreHealth > 1 && name == "Enemy")
        {
            coreHealth--;
            HealthReduction?.Invoke(this.gameObject);
        }
        else
        {
            coreHealth = 0;
            Death?.Invoke(this.gameObject);
        }
    }








}
