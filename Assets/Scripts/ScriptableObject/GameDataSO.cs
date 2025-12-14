using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Game Data/GameDataSO")]

public class GameDataSO : ScriptableObject
{
    public Dictionary<Levels, GameSceneSO> levelsDict = new Dictionary<Levels, GameSceneSO>();

    public Levels thisLevel;

    public int coreHealth;

    public float money;

    public bool maxMoney;

    public float maxMoneyCount;
    //public bool levelAccomplish;


    public UnityAction HealthReduction;
    public void CoreHealthReduceEvent()
    {
        HealthReduction?.Invoke();
    }

    public UnityAction Death;
    public void GameOverEvent()
    {
        Death?.Invoke();
    }

    public UnityAction accomplish;
    public void LevelAccomplish()
    {
        accomplish?.Invoke();
    }

}
