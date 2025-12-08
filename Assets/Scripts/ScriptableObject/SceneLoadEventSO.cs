using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<List<GameSceneSO>, Vector2, bool> UnLoadAllThenLoadEvent;
    public void RaiseUnLoadAllThenLoadEvent(List<GameSceneSO> scenesToLoad, Vector2 positionToGo, bool fadeScreen)
    {
        UnLoadAllThenLoadEvent?.Invoke(scenesToLoad, positionToGo, fadeScreen);
    }

    public UnityAction<List<GameSceneSO>, List<GameSceneSO>, Vector2, bool> CustomSceneOperationEvent;
    public void RaiseCustomSceneOperation(List<GameSceneSO> scenesToUnLoad, List<GameSceneSO> scenesToLoad, Vector2 positionToGo, bool fadeScreen)
    {
        CustomSceneOperationEvent?.Invoke(scenesToUnLoad, scenesToLoad, positionToGo, fadeScreen);
        //Debug.Log("555");
    }
}