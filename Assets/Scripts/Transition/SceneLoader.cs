using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using static SceneTeleport;

public class SceneLoader : MonoBehaviour
{
    public Transform cameraPosition;
    public Vector2 cameraFirstPosition;

    [Header("事件监听")]
    public SceneLoadEventSO loadEventSo;
    public GameSceneSO firstLoadScene;

    [Header("广播")]
    public VoidEventSO afterSceneLoadedEvent;
    private GameSceneSO currentLoadScene;

    private Vector2 positionToGo;
    private FadeTransition fadeTransition;
    public FadeCanvas fadeCanvas;
    private float fadeDuration;

    public bool isLoading;
    private bool newGame;

    public CameraController cameraController;

    private List<GameSceneSO> allLoadedScenes = new List<GameSceneSO>();

    private void Awake()
    {
        //Addressables.LoadSceneAsync(firstLoadScene.sceneReference, LoadSceneMode.Additive);
        //currentLoadScene = firstLoadScene;
        //currentLoadScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
    }

    private void Start()
    {
        if (fadeCanvas != null)
        {
            fadeDuration = fadeCanvas.fadeTransitionDuration;
            Color black = Color.black;
            fadeCanvas.fadeImage.color = black;
        }
        else
        {
            fadeDuration = 0;
        }
        NewGame();
    }

    private void OnEnable()
    {
        loadEventSo.UnLoadAllThenLoadEvent += OnUnLoadAllThenLoadEvent;
        loadEventSo.CustomSceneOperationEvent += OnCustomSceneOperationEvent;
    }

    private void OnDisable()
    {
        loadEventSo.UnLoadAllThenLoadEvent -= OnUnLoadAllThenLoadEvent;
        loadEventSo.CustomSceneOperationEvent -= OnCustomSceneOperationEvent;
    }

    private void NewGame()
    {
        newGame = true;
        List<GameSceneSO> initScene = new List<GameSceneSO>() { firstLoadScene };
        OnUnLoadAllThenLoadEvent(initScene, cameraFirstPosition, FadeTransition.ExitOnly);
        if (fadeCanvas != null)
        {
            fadeCanvas.newGame();
        }
    }

    /// <summary>
    /// 卸载所有并加载场景
    /// </summary>
    private void OnUnLoadAllThenLoadEvent(List<GameSceneSO> scenesToLoad, Vector2 positionToGo, FadeTransition fadeTransition)
    {
        if (isLoading)
        {
            return;
        }
        isLoading = true;

        this.positionToGo = positionToGo;
        this.fadeTransition = fadeTransition;

        StartCoroutine(UnLoadAllThenLoadCoroutine(scenesToLoad));
    }

    private IEnumerator UnLoadAllThenLoadCoroutine(List<GameSceneSO> scenesToLoad)
    {
        //渐隐判断
        if (fadeCanvas != null)
        {
            switch (fadeTransition)
            {
                case FadeTransition.EnterAndExit:
                case FadeTransition.EnterOnly:
                    if (!newGame)
                    {
                        fadeCanvas.EnterFade(true);
                        yield return new WaitForSeconds(fadeDuration);
                    }
                    break;
                case FadeTransition.ExitOnly:
                    if (!newGame)
                    {
                        fadeCanvas.EnterFade(false);
                    }
                    break;
                default:
                    break;
            }
        }



        // needFade = fadeScreen || newGame;
        // 修复2：newGame状态下跳过EnterFade（因为Start已设为全黑，只需渐出）
        /*
        if (fadeTransition == FadeTransition.EnterAndExit && !newGame)
        {
            fadeCanvas.EnterFade();
            yield return new WaitForSeconds(fadeDuration);
        }
        // newGame状态下直接等待时长（模拟渐入完成，实际已全黑）
        else if (fadeTransition == FadeTransition.EnterAndExit && newGame)
        {
            yield return new WaitForSeconds(fadeDuration);
        }
        */

        //卸载当前场景
        if (currentLoadScene != null)
        {
            yield return currentLoadScene.sceneReference.UnLoadScene();

            allLoadedScenes.Remove(currentLoadScene);
            currentLoadScene = null;
        }

        //卸载全部场景
        foreach (var scene in allLoadedScenes)
        {
            if (scene != null)
            {
                yield return scene.sceneReference.UnLoadScene();//等待卸载
            }

        }
        
        allLoadedScenes.Clear();//清空当前加载的列表

        //加载场景
        if (scenesToLoad != null && scenesToLoad.Count > 0)
        {
            yield return Coroutine_LoadScenes(scenesToLoad, positionToGo, fadeTransition, true);//等待加载
        }

        //yield return new WaitForSeconds(1f);//等待一秒


        //渐出判断
        if (fadeCanvas != null)
        {
            switch (fadeTransition)
            {
                case FadeTransition.EnterAndExit:
                case FadeTransition.ExitOnly:
                    fadeCanvas.ExitFade(true);
                    yield return new WaitForSeconds(fadeDuration);//等待加载时间
                    if (newGame)
                    {
                        newGame = false;
                    }
                    break;
                case FadeTransition.EnterOnly:
                    fadeCanvas.ExitFade(false);
                    break;
                default:
                    break;
            }
        }
        

        
        /*
        if (needFade && fadeCanvas != null)
        {
            fadeCanvas.ExitFade();
            if (newGame)
            {
                newGame = false;
            }
        }
        */
        isLoading = false;
        afterSceneLoadedEvent.RaiseEvent();
    }

    
    private IEnumerator Coroutine_LoadScenes(List<GameSceneSO> scenesToLoad, Vector2 pos, FadeTransition fadeTransition, bool isUnLoadAll = false)
    {
        
        if (!isUnLoadAll && fadeCanvas != null)
        {
            switch (fadeTransition)
            {
                case FadeTransition.EnterAndExit:
                case FadeTransition.EnterOnly:
                    fadeCanvas.EnterFade(true);
                    yield return new WaitForSeconds(fadeDuration);
                    break;
                case FadeTransition.ExitOnly:
                    fadeCanvas.EnterFade(false);
                    break;
                default:
                    break;
            }
            
        }


        /*
        if (!isUnLoadAll && fade && fadeCanvas != null) fadeCanvas.EnterFade();
        if (!isUnLoadAll) yield return new WaitForSeconds(fadeDuration);
        */
        foreach (var scene in scenesToLoad)
        {
            if (scene == null)
            {
                continue;
            }
            //防止重复加载
            if (allLoadedScenes.Contains(scene))
            {
                continue;
            }
            var op = scene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
            yield return op;
            allLoadedScenes.Add(scene);
        }

        cameraPosition.position = pos;

        //最后一个加载的是否是主界面
        if (scenesToLoad.Count > 0 && scenesToLoad[scenesToLoad.Count - 1].name == "MainMenu")
        {
            cameraController.enabled = false;
        }
        else
        {
            cameraController.enabled = true;
        }

        
        if (!isUnLoadAll && fadeCanvas != null)
        {
            

            switch (fadeTransition)
            {
                case FadeTransition.EnterAndExit:
                case FadeTransition.ExitOnly:
                    fadeCanvas.ExitFade(true);
                    yield return new WaitForSeconds(fadeDuration);
                    break;
                case FadeTransition.EnterOnly:
                    fadeCanvas.ExitFade(false);
                    break;
                default:
                    break;
            }

            //if (fade && fadeCanvas != null) fadeCanvas.ExitFade();

            isLoading = false;
            afterSceneLoadedEvent.RaiseEvent();
        }
    }

    /// <summary>
    /// 自定义加载与卸载
    /// </summary>
    /// <param name="scenesToUnLoad">卸载的场景</param>
    /// <param name="scenesToLoad">加载的场景</param>
    /// <param name="pos">相机位置</param>
    /// <param name="fadeTransition">渐隐渐出</param>
    private void OnCustomSceneOperationEvent(List<GameSceneSO> scenesToUnLoad, List<GameSceneSO> scenesToLoad, Vector2 pos, FadeTransition fadeTransition)
    {
        if (isLoading)
        {
            return;
        }
        isLoading = true;

        StartCoroutine(Coroutine_CustomSceneOperation(scenesToUnLoad, scenesToLoad, pos, fadeTransition));
    }

    private IEnumerator Coroutine_CustomSceneOperation(List<GameSceneSO> scenesToUnLoad, List<GameSceneSO> scenesToLoad, Vector2 pos, FadeTransition fadeTransition)
    {
        if (fadeCanvas != null)
        {
            switch (fadeTransition)
            {
                case FadeTransition.EnterAndExit:
                case FadeTransition.EnterOnly:
                    fadeCanvas.EnterFade(true);
                    yield return new WaitForSeconds(fadeDuration);
                    break;
                case FadeTransition.ExitOnly:
                    fadeCanvas.EnterFade(false);
                    break;
                default:
                    break;
            }
        }

        /*
        if (fade && fadeCanvas != null)
        {
            fadeCanvas.EnterFade();
            yield return new WaitForSeconds(fadeDuration);
        }
        */

        //卸载全部
        if (scenesToUnLoad != null && scenesToUnLoad.Count > 0)
        {
            foreach (var scene in scenesToUnLoad)
            {
                if (scene == null || !allLoadedScenes.Contains(scene))
                {
                    continue;
                }
                yield return scene.sceneReference.UnLoadScene();
                allLoadedScenes.Remove(scene);
            }
        }

        //加载全部
        if (scenesToLoad != null && scenesToLoad.Count > 0)
        {
            foreach (var scene in scenesToLoad)
            {
                if (scene == null) 
                { 
                    continue; 
                }

                if (allLoadedScenes.Contains(scene))
                {
                    continue;
                }
                var op = scene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
                yield return op;
                allLoadedScenes.Add(scene);
            }
        }

        if (scenesToLoad != null && scenesToLoad.Count > 0)
        {
            cameraPosition.position = pos;
            if (scenesToLoad[scenesToLoad.Count - 1].name == "MainMenu")
            {
                cameraController.enabled = false;
            }
            else
            {
                cameraController.enabled = true;
            }
        }

        

        if (fadeCanvas != null)
        {
            switch (fadeTransition)
            {
                case FadeTransition.EnterAndExit:
                case FadeTransition.ExitOnly:
                    fadeCanvas.ExitFade(true);
                    yield return new WaitForSeconds(fadeDuration);
                    break;
                case FadeTransition.EnterOnly:
                    fadeCanvas.ExitFade(false);
                    break;
                default:
                    break;
            }
        }

        /*
        if (fade && fadeCanvas != null) fadeCanvas.ExitFade();
        */
        isLoading = false;
        afterSceneLoadedEvent.RaiseEvent();
    }
}

    