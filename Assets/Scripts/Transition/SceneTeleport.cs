using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTeleport : MonoBehaviour
{

    public SceneLoadEventSO loadEventSO;

    public GameSceneSO sceneToGo;

    public Vector2 positionToGo;


    public void TeleportAction()
    {
        loadEventSO.RaiseLoadRequestEvent(sceneToGo, positionToGo, true);
    }


}
