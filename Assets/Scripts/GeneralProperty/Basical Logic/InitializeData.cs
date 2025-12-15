using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeData : MonoBehaviour
{
    public Levels thisLevel;

    public float initialMoney;

    public int initialHealth;

    public GameDataSO gameDataSO;

    public LevelAccomplishDataSO LevelAccomplishDataSO;

    public bool maxMoney;

    public float maxMoneyCount;

    private void Start()
    {
        DataInitialization();
    }

    public void DataInitialization()
    {
        Money.Instance.InitializeMoneyData(initialMoney, maxMoney, maxMoneyCount);

        CoreHealth.Instance.InitializeHealthData(initialHealth);

        gameDataSO.thisLevel = thisLevel;

        LevelAccomplishDataSO.BeginLevel(thisLevel);

        //gameDataSO.accomplish = false;
    }


}
