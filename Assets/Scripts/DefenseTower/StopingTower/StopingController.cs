using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopingController : MonoBehaviour
{

    [Header("采矿状态")]
    public bool stoping;

    public float stopingSpeed;

    public float stopingDuration;

    public float stopingCounter;



    [Header("放置花费")]
    public float placeMoney;



    [Header("金钱获取")]
    public MoneyManager moneyManager;

    public float moneyCount;





    private void Update()
    {

        StopingTimeCounter();


    }



    public void StopingTimeCounter()
    {
        if (!stoping)
        {
            //采矿开始
            stoping = true;
            stopingCounter = stopingDuration;
        }
        if (stoping)
        {
            stopingCounter -= stopingSpeed * Time.deltaTime;

            if(stopingCounter <= 0)
            {
                //采矿结束
                stoping = false;
                GetMoney();
            }
        }
    }


    public void GetMoney()
    {
        moneyManager.deltaMoney = moneyCount;
        moneyManager.GetMoney();
    }



}
