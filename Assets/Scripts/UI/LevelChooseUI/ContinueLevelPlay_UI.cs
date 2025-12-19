using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class ContinueLevelPlay_UI : MonoBehaviour
{
    public GameDataSO gameDataSO;

    public LevelAccomplishDataSO levelAccomplishDataSO;

    public TextMeshProUGUI currentLevel;

    private SceneTeleport sceneTeleport;

    public GameSceneSO Level;

    private void Awake()
    {
        sceneTeleport = GetComponent<SceneTeleport>();
    }


    private void Start()
    {
        Levels _currentLevel = levelAccomplishDataSO.currentLevel;
        var levelPair = levelAccomplishDataSO._serializedLevelData.Find(p => p.level == _currentLevel);
        Levels maxLevel = (Levels)Enum.GetValues(typeof(Levels)).Cast<int>().Max();
        

        sceneTeleport.scenesToLoad.Clear();      
        sceneTeleport.scenesToLoad.Add(Level);

        if (levelAccomplishDataSO.levelsAccomplishDict[_currentLevel])
        {
            if (maxLevel != _currentLevel)
            {
                Levels nextLevel = (Levels)((int)_currentLevel + 1);

                sceneTeleport.scenesToLoad.Add(gameDataSO.levelsDict[nextLevel]);
                currentLevel.text = nextLevel.ToString();

            }

        }
        else
        {
            if (levelPair.isUnLocked)
            {
                sceneTeleport.scenesToLoad.Add(gameDataSO.levelsDict[_currentLevel]);
                currentLevel.text = _currentLevel.ToString();
            }
            else
            {
                sceneTeleport.scenesToLoad.Add(gameDataSO.levelsDict[Levels.level1]);
                currentLevel.text = Levels.level1.ToString();
            }
        }


    }




}
