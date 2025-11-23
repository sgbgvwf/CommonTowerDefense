using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeData : MonoBehaviour
{


    private void Start()
    {
        DataInitialization();
    }

    public void DataInitialization()
    {
        Money.Instance.InitializeMoneyData();

        CoreHealth.Instance.InitializeHealthData();



    }


















}
