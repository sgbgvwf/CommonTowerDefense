using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour 
{
    //单例化
    private static Money instance;
    public static Money Instance;

    [Header("当前金钱")]
    public float money;

    [Header("初始金钱")]
    public float initialMoney;

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

    public void InitializeMoneyData()
    {
        money = initialMoney;
    }


    public bool ChangeMoney(float changeValue)
    {

        if (changeValue < 0)
        {
            if (money + changeValue > 0)
            {
                money += changeValue;
                return true;

            }
            else
            {

                return false;
            }
        }
        else if (usingMaxMoney)//启用金钱最大值限制
        {
            if(money + changeValue < maxMoney)
            {
                money += changeValue;
                return true;
            }
            else
            {
                money = maxMoney;
                return true;
            }
        }
        else
        {
            money += changeValue;
            return true;
        }

    }

    public void MoneyUpdate()
    {

    }





}
