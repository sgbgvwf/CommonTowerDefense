using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopingProcessDisplay : MonoBehaviour
{

    public StopingController stopingController;

    


    private void Update()
    {
        LocalScaleUpdate();
    }


    private void LocalScaleUpdate()
    {
        transform.localScale = new Vector3(
            (float)(0.6 * (1 - stopingController.stopingCounter / stopingController.stopingDuration)),
            (float)(0.6 * (1 - stopingController.stopingCounter / stopingController.stopingDuration)),
            (float)(0.6 * (1 - stopingController.stopingCounter / stopingController.stopingDuration))
        );
    }



}
