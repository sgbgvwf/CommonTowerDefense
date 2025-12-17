using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private static Pause instance;
    public static Pause Instance;

    public Image pauseButton;

    public Sprite pauseEnterImage;
    public Sprite pauseExitImage;


    [Header("暂停")]
    public bool isPause;


    [Header("黑暗处理")]
    public Image pauseDarkImage;

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

    /*
    private void Update()
    {
        if (isPause)
        {
            PauseGame();
        }
        else
        {
            ContinueGame();
        }
    }
    */

    public void Click()
    {
        if (!isPause)
        {
            PauseGame();
            pauseButton.sprite = pauseExitImage;
            isPause = true;
        }
        else
        {
            ContinueGame();
            pauseButton.sprite = pauseEnterImage;
            isPause = false;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;//游戏时间静止

        pauseDarkImage.enabled = true;//黑幕

        //鼠标位置和点击
        MousePositionDisplay.Instance.enabled = false;
        MouseClickManager.Instance.enabled = false;

    }

    public void ContinueGame()
    {
        if (DoubleSpeed.Instance.doubleSpeed)
        {
            Time.timeScale = 2f;
        }
        else
        {
            Time.timeScale = 1f;
        }


        pauseDarkImage.enabled = false;

        MousePositionDisplay.Instance.enabled = true;
        MouseClickManager.Instance.enabled = true;


    }




}
