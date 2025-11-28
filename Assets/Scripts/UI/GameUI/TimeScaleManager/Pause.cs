using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private static Pause instance;
    public static Pause Instance;

    [Header("暂停")]
    public bool isPause;


    [Header("黑暗处理")]
    public Image pauseImage;

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

    public void PauseGame()
    {
        Time.timeScale = 0;//游戏时间静止

        pauseImage.enabled = true;//黑幕

        //鼠标位置和点击
        MousePositionDisplay.Instance.enabled = false;
        MouseClickManager.Instance.enabled = false;

    }




    public void ContinueGame()
    {
        Time.timeScale = 1f;

        pauseImage.enabled = false;

        MousePositionDisplay.Instance.enabled = true;
        MouseClickManager.Instance.enabled = true;


    }




}
