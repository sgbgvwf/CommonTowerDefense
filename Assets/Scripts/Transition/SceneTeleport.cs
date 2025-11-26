using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTeleport : MonoBehaviour
{

    public SceneLoadEventSO loadEventSO;

    public GameSceneSO sceneToGo;

    public Vector2 positionToGo;
    
    [Header("转换场景")]
    public bool isTeleporting;

    [Header("场景渐入渐出")]
    public bool fadeTransition;

    private void Update()
    {
        if (isTeleporting)
        {
            TeleportAction();
            isTeleporting = false;
        }
    }

    public void TeleportAction()
    {
        loadEventSO.RaiseLoadRequestEvent(sceneToGo, positionToGo, fadeTransition);
    }


}
