using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTeleport : MonoBehaviour
{
    [Header(" 全局操作通用配置")]
    public SceneLoadEventSO loadEventSO;

    public Vector2 cameraTargetPosition;

    public bool useFadeTransition = true;

    public bool triggerOnUpdate;

    // 仅保留2个核心操作类型
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
    //[Header("卸载列表")]
    public List<GameSceneSO> scenesToUnLoad;//卸载列表
    //[Header("加载列表")]
    public List<GameSceneSO> scenesToLoad;//加载列表

    private void Update()
    {
        if (triggerOnUpdate)
        {
            ExecuteSceneOperation();
            triggerOnUpdate = false;
        }
    }

    /// <summary>
    /// 核心执行方法
    /// </summary>
    public void ExecuteSceneOperation()
    {
        switch (operationType)
        {
            case SceneOperationType.UnLoadAllThenLoad:
                loadEventSO.RaiseUnLoadAllThenLoadEvent(scenesToLoadAfterUnloadAll, cameraTargetPosition, useFadeTransition);
                break;
            case SceneOperationType.CustomSceneOperation:
                loadEventSO.RaiseCustomSceneOperation(scenesToUnLoad, scenesToLoad, cameraTargetPosition, useFadeTransition);
                break;
        }
        //Debug.Log("?");
    }
}