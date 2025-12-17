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

    private bool fade;

    private bool graduallyFade;

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

        Fade();

        //Debug.Log(this.fadeImage.color.a);

        if (timerManager.IsFinished(fadeTimer))
        {
            timerManager.Remove(fadeTimer);
        }

    }

    /// <summary>
    /// ½øÈëºÚÆÁ
    /// </summary>
    /// <param name="_graduallyFade"></param>
    public void EnterFade(bool _graduallyFade)
    {

        fade = true;
        if (GetComponent<MouseClickManager>())
        {
            MouseClickManager.Instance.enabled = false;
        }
        if (_graduallyFade)
        {
            timerManager.Start(fadeTimer, fadeTransitionDuration);
        }
        else
        {
            timerManager.Start(fadeTimer, 0);
        }
        graduallyFade = _graduallyFade;
    }

    /// <summary>
    /// ÍË³öºÚÆÁ
    /// </summary>
    /// <param name="_graduallyFade"></param>
    public void ExitFade(bool _graduallyFade)
    {

        fade = false;
        if (GetComponent<MouseClickManager>())
        {
            MouseClickManager.Instance.enabled = true;
        }
        if (_graduallyFade)
        {
            timerManager.Start(fadeTimer, fadeTransitionDuration); 
        }
        else
        {
            timerManager.Start(fadeTimer, 0);
        }
        graduallyFade = _graduallyFade;
    }

    public void newGame()
    {
        Color currentColor = this.fadeImage.color;
        currentColor.a = 1;
        this.fadeImage.color = currentColor;
    }

    private void Fade()
    {
        Color currentColor = this.fadeImage.color;

        if (graduallyFade)
        {
            if (!fade)
            {
                fadeImage.raycastTarget = false;
                currentColor.a = (1 - timerManager.GetElapsed(fadeTimer) / fadeTransitionDuration);
            }
            else
            {
                
                fadeImage.raycastTarget = true;
                currentColor.a = timerManager.GetElapsed(fadeTimer) / fadeTransitionDuration;
            }
        }
        else
        {
            if (!fade)
            {
                fadeImage.raycastTarget = false;
                currentColor.a = 0;
            }
            else
            {
                
                fadeImage.raycastTarget = true;
                currentColor.a = 1;
            }
        }

        this.fadeImage.color = currentColor;
    }

}
