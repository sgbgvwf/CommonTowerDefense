using Concorde.Timer;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AttackLaunchTimer
{
    private TimerManager timerManager = new TimerManager();

    public void BeginTimer()
    {
        if (timerManager.Exists("AttackFrequency"))
        {
            if (timerManager.IsFinished("AttackFrequency"))
            {
                //Debug.Log("TimeEnd");
                timerManager.Start("AttackFrequency", 0f);
            }
        }
        else
        {
            //Debug.Log("NoTimer");
            timerManager.Start("AttackFrequency", 0f);
        }
    }

    public bool DetectTimer()
    {
        if (!timerManager.Exists("AttackFrequency"))
        {
            return false;
        }
        if (timerManager.IsFinished("AttackFrequency"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void EndTimer(float attackFrequency, float delayTime, float attackSpeedScale, bool delay)
    {
        if (delay)
        {
            timerManager.Remove("AttackFrequency");
            timerManager.Start("AttackFrequency", (attackFrequency + delayTime) * attackSpeedScale);
        }
        else
        {
            timerManager.Remove("AttackFrequency");
            timerManager.Start("AttackFrequency", attackFrequency * attackSpeedScale);
        }
    }




}
