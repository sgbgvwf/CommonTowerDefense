using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoreHealth : MonoBehaviour
{
    private static CoreHealth instance;
    public static CoreHealth Instance;


    //当前生命
    public int coreHealth;

    //初始生命
    public int initialHealth;
   

    public UnityEvent<GameObject> HealthReduction;

    public UnityEvent<GameObject> Death;

    private void Awake()
    {
        if(instance == null)
        {
            Instance = this;

        }
    }

    public void InitializeHealthData()
    {
        coreHealth = initialHealth;
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
