using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

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
    private bool fadeScreen;
    public FadeCanvas fadeCanvas;
    private float fadeDuration;

    public bool isLoading;
    private bool newGame;

    public CameraController cameraController;

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
        OnUnLoadAllThenLoadEvent(initScene, cameraFirstPosition, true);
        if (fadeCanvas != null)
        {
            fadeCanvas.newGame();
        }
    }

    /// <summary>
    /// 卸载所有并加载场景
    /// </summary>
    private void OnUnLoadAllThenLoadEvent(List<GameSceneSO> scenesToLoad, Vector2 positionToGo, bool fadeScreen)
    {
        if (isLoading) return;
        isLoading = true;
        this.positionToGo = positionToGo;
        this.fadeScreen = fadeScreen;
        StartCoroutine(UnLoadAllThenLoadCoroutine(scenesToLoad));
    }

    private IEnumerator UnLoadAllThenLoadCoroutine(List<GameSceneSO> scenesToLoad)
    {
        bool needFade = fadeScreen || newGame;
        // 修复2：newGame状态下跳过EnterFade（因为Start已设为全黑，只需渐出）
        if (needFade && fadeCanvas != null && !newGame)
        {
            fadeCanvas.EnterFade();
            yield return new WaitForSeconds(fadeDuration);
        }
        // newGame状态下直接等待时长（模拟渐入完成，实际已全黑）
        else if (needFade && newGame)
        {
            yield return new WaitForSeconds(fadeDuration);
        }

        if (currentLoadScene != null)
        {
            yield return currentLoadScene.sceneReference.UnLoadScene();
            allLoadedScenes.Remove(currentLoadScene);
            currentLoadScene = null;
        }
        foreach (var scene in allLoadedScenes)
        {
            if (scene != null) yield return scene.sceneReference.UnLoadScene();
        }
        allLoadedScenes.Clear();

        if (scenesToLoad != null && scenesToLoad.Count > 0)
        {
            yield return Coroutine_LoadScenes(scenesToLoad, positionToGo, false, true);
        }

        yield return new WaitForSeconds(1f);
        if (needFade && fadeCanvas != null)
        {
            fadeCanvas.ExitFade();
            if (newGame)
            {
                newGame = false;
            }
        }
        isLoading = false;
        afterSceneLoadedEvent.RaiseEvent();
    }

    /// <summary>
    /// 自定义加载与卸载
    /// </summary>
    /// <param name="scenesToUnLoad">卸载的场景</param>
    /// <param name="scenesToLoad">加载的场景</param>
    /// <param name="pos">相机位置</param>
    /// <param name="fade">是否渐隐渐出</param>
    private void OnCustomSceneOperationEvent(List<GameSceneSO> scenesToUnLoad, List<GameSceneSO> scenesToLoad, Vector2 pos, bool fade)
    {
        if (isLoading) return;
        isLoading = true;
        StartCoroutine(Coroutine_CustomSceneOperation(scenesToUnLoad, scenesToLoad, pos, fade));
    }

    private IEnumerator Coroutine_CustomSceneOperation(List<GameSceneSO> scenesToUnLoad, List<GameSceneSO> scenesToLoad, Vector2 pos, bool fade)
    {
        if (fade && fadeCanvas != null)
        {
            fadeCanvas.EnterFade();
            yield return new WaitForSeconds(fadeDuration);
        }

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

        if (scenesToLoad != null && scenesToLoad.Count > 0)
        {
            foreach (var scene in scenesToLoad)
            {
                if (scene == null) continue;
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

        yield return new WaitForSeconds(1f);
        if (fade && fadeCanvas != null) fadeCanvas.ExitFade();

        isLoading = false;
        afterSceneLoadedEvent.RaiseEvent();
    }


    private IEnumerator Coroutine_LoadScenes(List<GameSceneSO> scenesToLoad, Vector2 pos, bool fade, bool isUnLoadAll = false)
    {
        if (!isUnLoadAll && fade && fadeCanvas != null) fadeCanvas.EnterFade();
        if (!isUnLoadAll) yield return new WaitForSeconds(fadeDuration);

        foreach (var scene in scenesToLoad)
        {
            if (scene == null) continue;
            if (allLoadedScenes.Contains(scene))
            {

                continue;
            }
            var op = scene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
            yield return op;
            allLoadedScenes.Add(scene);
        }

        cameraPosition.position = pos;
        if (scenesToLoad.Count > 0 && scenesToLoad[scenesToLoad.Count - 1].name == "MainMenu")
        {
            cameraController.enabled = false;
        }
        else
        {
            cameraController.enabled = true;
        }

        if (!isUnLoadAll)
        {
            yield return new WaitForSeconds(1f);
            if (fade && fadeCanvas != null) fadeCanvas.ExitFade();
            isLoading = false;
            afterSceneLoadedEvent.RaiseEvent();
        }
    }

    private List<GameSceneSO> allLoadedScenes = new List<GameSceneSO>();
}