using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Camera Data/CameraDataSO")]
public class CameraDataSO : ScriptableObject
{
    public Vector3 cameraPosition;

    public float cameraMoveSensitivity;

    public float cameraScaleSensitivity;
}
