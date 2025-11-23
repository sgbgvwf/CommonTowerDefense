using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO") ]

public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, Vector2, bool> LoadRequestEvent;

    /// <summary>
    /// 场景加载请求
    /// </summary>
    /// <param name="locationToLoad">要加载的场景</param>
    /// <param name="positionToGo">加载后相机的目的坐标</param>
    /// <param name="fadeScreen">是否渐入渐出</param>
    public void RaiseLoadRequestEvent(GameSceneSO locationToLoad, Vector2 positionToGo, bool fadeScreen)
    {
        LoadRequestEvent?.Invoke(locationToLoad, positionToGo, fadeScreen);
    }

}
