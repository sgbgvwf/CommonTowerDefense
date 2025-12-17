using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AllLevelDataInitialization : MonoBehaviour
{
    public GameDataSO gameDataSO;

    [System.Serializable]
    public struct LevelsToAsset
    {
        public Levels level;

        public GameSceneSO assetReference;
    }

    public List<LevelsToAsset> levelsToAssets;


    private void Start()
    {
        foreach (var reference in levelsToAssets)
        {
            if (!gameDataSO.levelsDict.ContainsKey(reference.level))
            {
                gameDataSO.levelsDict.Add(reference.level, reference.assetReference);
            }
            else
            {

            }


        }
    }



}
