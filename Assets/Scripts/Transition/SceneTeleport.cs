using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTeleport : MonoBehaviour
{
    [Header("操作通用配置")]
    public SceneLoadEventSO loadEventSO;

    public GameDataSO gameDataSO;

    public Vector2 cameraTargetPosition;

    public enum FadeTransition
    {
        EnterAndExit,
        NoEnterAndExit,
        EnterOnly,
        ExitOnly,
    }
    [Header("渐隐渐出设置")]
    public FadeTransition fadeTransition;

    //public bool triggerOnUpdate;

    public enum SceneOperationType
    {
        UnLoadAllThenLoad,//卸载所有并加载场景
        CustomSceneOperation//自定义操作
    }
    [Header("场景操作类型选择")]
    public SceneOperationType operationType;

    [Header("仅卸载所有+加载场景")]
    public List<GameSceneSO> scenesToLoadAfterUnloadAll;

    [Header("自定义卸载与加载")]
    public bool autoUnLoadThisLevel;
    //[Header("卸载列表")]
    public List<GameSceneSO> scenesToUnLoad;//卸载列表
    //[Header("加载列表")]
    public List<GameSceneSO> scenesToLoad;//加载列表

    

    /// <summary>
    /// 核心执行方法
    /// </summary>
    public void ExecuteSceneOperation()
    {
        Time.timeScale = 1.0f;
        switch (operationType)
        {
            case SceneOperationType.UnLoadAllThenLoad:
                loadEventSO.RaiseUnLoadAllThenLoadEvent(scenesToLoadAfterUnloadAll, cameraTargetPosition, fadeTransition);
                break;
            case SceneOperationType.CustomSceneOperation:
                scenesToUnLoad.Add(gameDataSO.levelsDict[gameDataSO.thisLevel]);
                loadEventSO.RaiseCustomSceneOperation(scenesToUnLoad, scenesToLoad, cameraTargetPosition, fadeTransition);
                break;
        }
        //Debug.Log("?");
    }
}