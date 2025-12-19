using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContinuePlay_UI : MonoBehaviour
{
    public LevelAccomplishDataSO levelAccomplishDataSO;

    public GameObject continueGame;

    private void Start()
    {
        Levels _currentLevel = levelAccomplishDataSO.currentLevel;
        Levels maxLevel = (Levels)Enum.GetValues(typeof(Levels)).Cast<int>().Max();

        //Debug.Log(maxLevel);
        if (_currentLevel == Levels.None || (_currentLevel == maxLevel && levelAccomplishDataSO.levelsAccomplishDict[_currentLevel]))
        {
            continueGame.SetActive(false);
        }
        else
        {
            continueGame.SetActive(true);
        }
    }




}
