using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour 
{
    private static Money instance;
    public static Money Instance;



    //当前金钱
    public float money;


    //初始金钱
    public float initialMoney;

    private float moneyChange;

    private void Awake()
    {
        if (instance == null)
        {
            Instance = this;
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
