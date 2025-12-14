using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Concorde.Timer;
using Unity.VisualScripting;

public class GameFinish : MonoBehaviour
{
    public GameDataSO gameDataSO;

    public LevelAccomplishDataSO levelAccomplishDataSO;

    private Levels thisLevel;

    TimerManager timerManager;

    public GameObject spawnEnemy;

    public GameObject enemyParent;

    public bool isFinish;


    private void Awake()
    {
        timerManager = new TimerManager();
    }

    private void Start()
    {
        timerManager.Start("", 0f);
    }
    private void Update()
    {
        if (timerManager.IsFinished(""))
        {
            CheckGameFinish();

            timerManager.Start("", 3);
        }
    }

    public void CheckGameFinish()
    {
        if (!spawnEnemy.GetComponent<EnemySpawn>() && enemyParent.transform.childCount == 0)
        {
            //”Œœ∑Ω· ¯£°
            isFinish = true;
            thisLevel = gameDataSO.thisLevel;
            levelAccomplishDataSO.levelsAccomplishDict[thisLevel] = true;
            gameDataSO.LevelAccomplish();
            Debug.Log("accpmplish");
        }
    }



}
