using Concorde.Timer;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PathPoint : MonoBehaviour
{

    public SpriteRenderer point;


    [Header("等待一段时间")]
    public bool wait;

    public float waitDuration;




    Color currentColor = new Color(240 / 255f, 50 / 255f, 50 / 255f, 0);

    private bool display;

    private string displayTimer = "display";

    private TimerManager timerManager;

    private float displayDuration = 0.75f;

    private void Awake()
    {
        timerManager = new TimerManager();
    }



    private void Start()
    {
        point.color = new Color(240/255f, 50/255f, 50/255f, 0);
    }



    private void Update()
    {
        if (!timerManager.Exists(displayTimer))
        {
            return;
        }

        Color currentColor = point.color;

        if (display)
        {
            currentColor.a = timerManager.GetElapsed(displayTimer) / displayDuration;


        }
        else
        {
            currentColor.a = (1 - timerManager.GetElapsed(displayTimer) / displayDuration);
        }

        point.color = currentColor;

        if (timerManager.IsFinished(displayTimer))
        {
            timerManager.Remove(displayTimer);
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag != "PathPoint")
        {
            return;
        }
        Debug.Log(collision);
        StartCoroutine(Display());

    }



    private void EnterDisplay()
    {
        display = true;
        timerManager.Start(displayTimer, displayDuration);

    }

    private void ExitDisplay()
    {
        display = false;
        timerManager.Start(displayTimer, displayDuration);


    }

    private IEnumerator Display()
    {
        EnterDisplay();

        yield return new WaitForSeconds(0.75f);

        ExitDisplay();
    }



}
