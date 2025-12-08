using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Concorde.Timer;

public class FadeCanvas : MonoBehaviour
{
    public Image fadeImage;

    private TimerManager timerManager;


    public float fadeTransitionDuration;

    private bool faded;

    private string fadeTimer = "fadeTimer";


    private void Awake()
    {
        fadeImage.raycastTarget = true;
        timerManager = new TimerManager();
        //timerManager.Start(fadeTimer, fadeTransitionDuration);
    }


    private void Update()
    {

        if (!timerManager.Exists(fadeTimer))
        {
            return;
        }


        Color currentColor = this.fadeImage.color;
        
        if (faded )
        {
            fadeImage.raycastTarget = true;
            currentColor.a =  timerManager.GetElapsed(fadeTimer) / fadeTransitionDuration;
        }
        else
        {
            currentColor.a =  (1 - timerManager.GetElapsed(fadeTimer) / fadeTransitionDuration);
            fadeImage.raycastTarget = false;
        }

        //Debug.Log(this.fadeImage.color.a);
        this.fadeImage.color = currentColor;

        if (timerManager.IsFinished(fadeTimer))
        {
            timerManager.Remove(fadeTimer);
        }


    }

    public void EnterFade()
    {

        faded = true;
        if (GetComponent<MouseClickManager>())
        {
            MouseClickManager.Instance.enabled = false;
        }
        timerManager.Start(fadeTimer, fadeTransitionDuration);
    }

    public void ExitFade()
    {

        faded = false;
        if (GetComponent<MouseClickManager>())
        {
            MouseClickManager.Instance.enabled = true;
        }
        timerManager.Start(fadeTimer, fadeTransitionDuration);
    }

    public void newGame()
    {
        Color currentColor = this.fadeImage.color;
        currentColor.a = 1;
        this.fadeImage.color = currentColor;

    }
}
