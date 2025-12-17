using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefenseTowerChoose_UI : MonoBehaviour
{

    public DefenseTowerType defenseTowerType;

    public GameObject chooseFramework;

    [HideInInspector]public bool isChoose;

    private void Awake()
    {
        chooseFramework.SetActive(false);
    }

    public void ChooseThisTower()
    {
        if (!isChoose)
        {
            isChoose = true;
            chooseFramework.SetActive(true);
            BuildManager.Instance.SelectTowerToBuild(defenseTowerType, this);

        }
        
    }

    public void ExitChoose()
    {
        isChoose = false;
        chooseFramework.SetActive(false);

    }



}
