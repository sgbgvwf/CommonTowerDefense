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


    //暂时存储变量
    private GameSceneSO locationToLoad;

    private Vector2 positionToGo;

    private bool fadeScreen;


    //渐入渐出的等候时间
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

    //TODO:做完MainMenu回来改
    private void Start()
    {
        fadeDuration = fadeCanvas.fadeTransitionDuration;

        NewGame();
    }


    private void OnEnable()
    {
        loadEventSo.LoadRequestEvent += OnLoadRequestEvent;
    }


    private void OnDisable()
    {
        loadEventSo.LoadRequestEvent -= OnLoadRequestEvent;
    }


    private void NewGame()
    {
        newGame = true;

        locationToLoad = firstLoadScene;

        fadeCanvas.newGame();
        OnLoadRequestEvent(locationToLoad, cameraFirstPosition, false);

    }




    /// <summary>
    /// 事件加载请求
    /// </summary>
    /// <param name="locationToLoad"></param>
    /// <param name="positionToGo"></param>
    /// <param name="fadeScreen"></param>
    private void OnLoadRequestEvent(GameSceneSO locationToLoad, Vector2 positionToGo, bool fadeScreen)
    {
        if (isLoading)
        {
            return;
        }

        isLoading = true;

        this.locationToLoad = locationToLoad;
        this.positionToGo = positionToGo;
        this.fadeScreen = fadeScreen;

        StartCoroutine(UnLoadPreviousScene());


        //Debug.Log("场景转换");
    }


    private IEnumerator UnLoadPreviousScene()
    {
        if (fadeScreen)
        {
            fadeCanvas.EnterFade();
        }

        yield return new WaitForSeconds(fadeDuration);

        if(currentLoadScene != null)
        {
            yield return currentLoadScene.sceneReference.UnLoadScene();
            LoadNewScene();
        }
        else
        {
            LoadNewScene();
        }

        

    }




    private void LoadNewScene()
    {
        var loadingOption = locationToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        loadingOption.Completed += OnLoadCompleted;
    }

    /// <summary>
    /// 场景加载完成后
    /// </summary>
    /// <param name="obj"></param>
    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> obj)
    {
        currentLoadScene = locationToLoad;

        cameraPosition.position = positionToGo;


        if(currentLoadScene.name == "MainMenu")
        {
            cameraController.enabled = false;
        }
        else
        {
            cameraController.enabled = true;
        }


            //Debug.Log("COMPLETED");

            StartCoroutine(NewScenePrepare());






        //场景加载完成后事件
        afterSceneLoadedEvent.RaiseEvent();


    }

    private IEnumerator NewScenePrepare()
    {
        yield return new WaitForSeconds(1f);
        //Debug.Log("exit");
        if (fadeScreen)
        {
            fadeCanvas.ExitFade();
        }
        else if (newGame)
        {
            fadeCanvas.ExitFade();
            newGame = false;
        }

        isLoading = false;
    }


}
