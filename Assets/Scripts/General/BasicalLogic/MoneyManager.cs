using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [Header("初始金钱")]
    public float initialMoney;

    [Header("当前金钱")]
    public float money;



    public float deltaMoney;

    

    private void Start()
    {
        money = initialMoney;
    }


    public void GetMoney()
    {
        //数据需保证已验证过正负值
        money += deltaMoney;
        deltaMoney = 0;//防止多次添加
    }



}
