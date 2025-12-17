using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoreHealth : MonoBehaviour
{
    private static CoreHealth instance;
    public static CoreHealth Instance;

    public GameDataSO gameDataSO;

    //当前生命
    public int coreHealth;

    //初始生命
    public int initialHealth;
   

    

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

    public void InitializeHealthData(int _initialHealth)
    {
        coreHealth = _initialHealth;
        gameDataSO.coreHealth = coreHealth;
        initialHealth = _initialHealth;
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
                gameDataSO.CoreHealthReduceEvent();
            }

            else
            {
                coreHealth = 0;
                gameDataSO.GameOverEvent();
            }
        }

        gameDataSO.coreHealth = coreHealth;
    }


}
