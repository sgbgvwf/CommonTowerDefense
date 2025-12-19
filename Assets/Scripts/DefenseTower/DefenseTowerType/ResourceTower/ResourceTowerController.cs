using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceTowerController : MonoBehaviour
{

    private GeneralProperty generalProperty;

    [Header("采矿状态")]
    public bool stopping;

    [HideInInspector] public float stoppingSpeed;//等于攻速

    [HideInInspector] public float stoppingDuration;//等于攻击频率

    [HideInInspector] public float stoppingCounter;//计数


    [Header("金钱获取量")]
    [HideInInspector] public float getMoneyValue;//等于伤害

    private void Awake()
    {
        generalProperty = GetComponent<GeneralProperty>();
    }

    private void Start()
    {
        stoppingSpeed = generalProperty.attackSpeedScale;
        stoppingDuration = generalProperty.attackFrequency;
        getMoneyValue = generalProperty.damage;
    }


    private void Update()
    {
        StopingTimeCounter();
        stoppingSpeed = generalProperty.attackSpeedScale;
        stoppingDuration = generalProperty.attackFrequency;
        getMoneyValue = generalProperty.damage;
    }



    public void StopingTimeCounter()
    {
        if (!stopping)
        {
            //采矿开始
            stopping = true;
            stoppingCounter = stoppingDuration;
        }
        if (stopping)
        {
            stoppingCounter -= stoppingSpeed * Time.deltaTime;

            if(stoppingCounter <= 0)
            {
                //采矿结束
                stopping = false;
                GetMoney();
            }
        }
    }


    public void GetMoney()
    {

        Money.Instance.ChangeMoney(getMoneyValue);
        
        
    }



}