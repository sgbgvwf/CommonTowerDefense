using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static SceneTeleport;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<List<GameSceneSO>, Vector2, FadeTransition> UnLoadAllThenLoadEvent;
    public void RaiseUnLoadAllThenLoadEvent(List<GameSceneSO> scenesToLoad, Vector2 positionToGo, FadeTransition fadeTransition)
    {
        UnLoadAllThenLoadEvent?.Invoke(scenesToLoad, positionToGo, fadeTransition);
    }

    public UnityAction<List<GameSceneSO>, List<GameSceneSO>, Vector2, FadeTransition> CustomSceneOperationEvent;
    public void RaiseCustomSceneOperation(List<GameSceneSO> scenesToUnLoad, List<GameSceneSO> scenesToLoad, Vector2 positionToGo, FadeTransition fadeTransition)
    {
        CustomSceneOperationEvent?.Invoke(scenesToUnLoad, scenesToLoad, positionToGo, fadeTransition);
    }
}