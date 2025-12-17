using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Money : MonoBehaviour 
{
    //单例化
    private static Money instance;
    public static Money Instance;

    public GameDataSO gameDataSO;

    [Header("当前金钱")]
    public float money;

    //[Header("初始金钱")]
    //public float initialMoney;

    [Header("金钱上限")]
    public bool usingMaxMoney;

    public float maxMoney;



    private float moneyChange;

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

    public void InitializeMoneyData(float initialMoney, bool _maxMoney, float _maxMoneyCount)
    {
        money = initialMoney;
        usingMaxMoney = _maxMoney;
        maxMoney = _maxMoneyCount;

        gameDataSO.money = money;
        gameDataSO.maxMoney = usingMaxMoney;
        gameDataSO.maxMoneyCount = maxMoney;
    }


    public bool ChangeMoney(float changeValue)
    {
        bool success;

        if (changeValue < 0)
        {
            if (money + changeValue >= 0)
            {
                money += changeValue;
                success = true;

            }
            else
            {
                success = false;
            }
        }
        else if (usingMaxMoney)//启用金钱最大值限制
        {
            if(money + changeValue < maxMoney)
            {
                money += changeValue;
                success = true;
            }
            else
            {
                money = maxMoney;
                success = true;
            }
        }
        else
        {
            money += changeValue;
            success = true;
        }

        MoneyUpdate();

        return success;
    }

    public void MoneyUpdate()
    {
        gameDataSO.money = money;
    }


}
