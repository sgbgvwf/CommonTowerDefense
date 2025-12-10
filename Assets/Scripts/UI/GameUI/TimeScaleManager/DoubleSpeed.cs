using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleSpeed : MonoBehaviour
{
    private static DoubleSpeed instance;
    public static DoubleSpeed Instance;

    public bool doubleSpeed;

    public Image doubleSpeedButton;

    public Sprite enterDoubleSpeedImage;
    public Sprite exitDoubleSpeedImage;

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



    public void ChangeSpeed()
    {
        if (!Pause.Instance.isPause)
        {
            if (!doubleSpeed)
            {
                doubleSpeedButton.sprite = exitDoubleSpeedImage;
                Time.timeScale = 2f;
                doubleSpeed = true;
            }
            else
            {
                doubleSpeedButton.sprite = enterDoubleSpeedImage;
                Time.timeScale = 1f;
                doubleSpeed = false;
            }
        }
        else
        {
            if (!doubleSpeed)
            {
                doubleSpeedButton.sprite = exitDoubleSpeedImage;

                doubleSpeed = true;
            }
            else
            {
                doubleSpeedButton.sprite = enterDoubleSpeedImage;

                doubleSpeed = false;
            }
        }

    }

}
