using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Accomplish Data/LevelAccomplishDataSO")]

public class LevelAccomplishDataSO : ScriptableObject
{
    public Levels currentLevel;
    //[SerializeField]private CurrentAttackingLevel currentAttackingLevel = new CurrentAttackingLevel();

    [SerializeField]public List<LevelAccomplishPair> _serializedLevelData = new List<LevelAccomplishPair>();

    public Dictionary<Levels, bool> levelsAccomplishDict;

    private void OnEnable()
    {
        if (levelsAccomplishDict == null)
        {
            levelsAccomplishDict = new Dictionary<Levels, bool>();
        }
        else
        {
            levelsAccomplishDict.Clear();
        }

        foreach (var pair in _serializedLevelData)
        {
            if (levelsAccomplishDict.ContainsKey(pair.level))
            {
                levelsAccomplishDict[pair.level] = pair.isAccomplished;
            }
            else
            {
                levelsAccomplishDict.Add(pair.level, pair.isAccomplished);
            }
        }
        
    }

    public void UpdateLevelAccomplishState(Levels level, bool isAccomplished)
    {
        //´æ×Öµä
        if (levelsAccomplishDict.ContainsKey(level))
        {
            levelsAccomplishDict[level] = isAccomplished;
        }
        else
        {
            levelsAccomplishDict.Add(level, isAccomplished);
        }

        //´æÁÐ±í
        var existingPair = _serializedLevelData.Find(p => p.level == level);
        if (existingPair != null)
        {
            existingPair.isAccomplished = isAccomplished;
        }
        else
        {
            _serializedLevelData.Add(new LevelAccomplishPair { level = level, isAccomplished = isAccomplished });
        }

        UnLockLevel(level, isAccomplished);
    }

    public void UnLockLevel(Levels level, bool isAccomplished)
    {
        
        Levels maxLevel = (Levels)Enum.GetValues(typeof(Levels)).Cast<int>().Max();
        if (maxLevel != level)
        {
            Levels nextLevel = (Levels)((int)level + 1);
            var existingPair = _serializedLevelData.Find(p => p.level == nextLevel); 
            if (existingPair != null && isAccomplished)
            {
                existingPair.isUnLocked = true;
            }
        }
    }


    public bool GetLevelAccomplishState(Levels level)
    {
        var existingPair = _serializedLevelData.Find(p => p.level == level);
        levelsAccomplishDict[level] = existingPair.isAccomplished;

        if (levelsAccomplishDict.TryGetValue(level, out bool state))
        {
            return state;
        }
        else
        {
            UpdateLevelAccomplishState(level, false);
            return false;
        }

    }


    public void BeginLevel(Levels level)
    {
        currentLevel = level;
    }


}

[System.Serializable]
public class LevelAccomplishPair
{
    public Levels level;
    public bool isUnLocked;
    public bool isAccomplished;

}

[System.Serializable]
public class CurrentAttackingLevel
{
    public Levels level;
}