using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceProcessDisplay1 : MonoBehaviour
{

    public ResourceTowerController resourceTowerController;

    private void Update()
    {
        LocalScaleUpdate();
    }

    private void LocalScaleUpdate()
    {
        transform.localScale = new Vector3(
            (float)(0.6 * (1 - resourceTowerController.stopingCounter / resourceTowerController.stopingDuration)),
            (float)(0.6 * (1 - resourceTowerController.stopingCounter / resourceTowerController.stopingDuration)),
            (float)(0.6 * (1 - resourceTowerController.stopingCounter / resourceTowerController.stopingDuration))
        );
    }

}
