using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CameraSettings_UI : MonoBehaviour
{
    public CameraDataSO cameraDataSO;

    public Slider slider;

    public TextMeshProUGUI value;

    private void Start()
    {
        //Debug.Log(gameObject.name);
        if(gameObject.name == "CameraMoveSeneitivity")
        {
            slider.value = cameraDataSO.cameraMoveSensitivity;
            value.text = slider.value.ToString();
        }
        else if (gameObject.name == "CameraScaleSensitivity")
        {
            slider.value = cameraDataSO.cameraScaleSensitivity;
            value.text = slider.value.ToString();
        }
    }

    /*
    private void OnDisable()
    {
        if (gameObject.name == "CameraMoveSeneitivity")
        {
            cameraDataSO.cameraMoveSensitivity = GetComponent<Slider>().value;
        }
        else if (gameObject.name == "CameraScaleSensitivity")
        {
            cameraDataSO.cameraScaleSensitivity = GetComponent<Slider>().value;
        }
    }
    */
    
    public void CameraMoveChange()
    {
        cameraDataSO.cameraMoveSensitivity = slider.value;
        value.text = slider.value.ToString();
    }

    public void CameraScaleChange()
    {
        cameraDataSO.cameraScaleSensitivity = slider.value;
        value.text = slider.value.ToString();
    }
    


}
