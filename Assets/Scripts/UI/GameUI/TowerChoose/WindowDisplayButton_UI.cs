using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowDisplayButton_UI : MonoBehaviour
{

    public RectTransform ChooseDefenseTower;

    public bool isDisplay;

    public void Display()
    {
        if (!isDisplay)
        {
            ChooseDefenseTower.anchoredPosition = new Vector3(-250, 0, 0);

            isDisplay = true;
        }
        else
        {
            ChooseDefenseTower.anchoredPosition = new Vector3(0, 0, 0);


            isDisplay = false;
        }
    }

}
