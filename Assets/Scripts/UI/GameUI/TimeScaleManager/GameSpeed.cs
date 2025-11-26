using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeed : MonoBehaviour
{
    private static GameSpeed instance;
    public static GameSpeed Instance;

    private bool doubleSpeed;

    private void Awake()
    {
        if (instance == null)
        {
            Instance = this;
        }
    }



    public void ChangeSpeed()
    {
        if (doubleSpeed)
        {
            Time.timeScale = 2f;
        }
        else
        {
            Time.timeScale = 1f;
        }



    }




}
