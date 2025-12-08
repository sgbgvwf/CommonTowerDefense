using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{

    public GameObject settingWindow;
    public GameObject Buttons;


    public bool settingWindowIsEnter;


    private void Awake()
    {
        settingWindowIsEnter = false;

        settingWindow.SetActive(false);
        Buttons.SetActive(false);
    }

    public void SettingWindowDisplay()
    {
        if (!settingWindowIsEnter)
        {
            Pause.Instance.PauseGame();

            settingWindow.SetActive(true); 
            Buttons.SetActive(true);

            settingWindowIsEnter = true;
        }
        
    }

    public void CancelSetting()
    {
        if(settingWindowIsEnter)
        {
            Pause.Instance.ContinueGame();

            settingWindow.SetActive(false);
            Buttons.SetActive(false);


            settingWindowIsEnter = false;
        }
    }

    public void ExitLevel()
    {
        Time.timeScale = 1f;
    }


}
