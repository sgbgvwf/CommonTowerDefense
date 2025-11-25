using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCanvasManager : MonoBehaviour
{
    public FadeCanvas fadeCanvas;

    public float fadeDuration;

    private void Start()
    {
        fadeDuration = fadeCanvas.fadeTransitionDuration;
        //fadeCanvas.enabled = false;
    }

    public void BeginFade()
    {
        //fadeCanvas.enabled = true;
        fadeCanvas.EnterFade();
    }


    public void EndFade()
    {
        fadeCanvas.ExitFade();
        //fadeCanvas.enabled = false;
    }

}
