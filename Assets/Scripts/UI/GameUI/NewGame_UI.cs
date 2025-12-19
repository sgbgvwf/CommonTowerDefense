using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame_UI : MonoBehaviour
{
    public LevelAccomplishDataSO levelAccomplishData;

    public void ClearAllData()
    {
        foreach(var data in levelAccomplishData._serializedLevelData)
        {
            data.isUnLocked = false;
            data.isAccomplished = false;
        }
        levelAccomplishData._serializedLevelData.Find(p => p.level == Levels.level1).isUnLocked = true;
    }
}
