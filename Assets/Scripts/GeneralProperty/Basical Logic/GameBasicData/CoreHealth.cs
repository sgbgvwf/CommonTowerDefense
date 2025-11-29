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
        if (instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("单例不单一！");
        }
    }

    public void InitializeHealthData()
    {
        coreHealth = initialHealth;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }


    public void CoreHealthReduce(GameObject DetectedGameObject)
    {
        if(DetectedGameObject.tag == "Enemy")
        {
            if(DetectedGameObject.GetComponent<EnemyProperty>() == null)
            {
                return;
            }

            if (coreHealth - DetectedGameObject.GetComponent<EnemyProperty>().coreDamage > 0)
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


}
