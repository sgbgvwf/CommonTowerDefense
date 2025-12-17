using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLevelWindowDisplayButton_UI : MonoBehaviour
{

    public RectTransform TowerLevelUp;

    public bool isDisplay;

    private void Awake()
    {
        isDisplay = true;
    }

    public void Display()
    {
        if (!isDisplay)
        {
            TowerLevelUp.anchoredPosition = new Vector3(1920, -540, 0);

            isDisplay = true;
        }
        else
        {
            TowerLevelUp.anchoredPosition = new Vector3(1920+350, -540, 0);


            isDisplay = false;
        }
    }


}
