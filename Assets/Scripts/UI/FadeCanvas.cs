using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Concorde.Timer;

public class FadeCanvas : MonoBehaviour
{
    public Image fadeImage;

    public TimerManager timerManager;

    public float fadeTransitionDuration;

    public float fadeTransitionCounter;




    public void EnterFade()
    {
        timerManager.Start("fadeTimer", fadeTransitionDuration);

    }



}
