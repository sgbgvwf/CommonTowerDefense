using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChooseObjectMove_UI : MonoBehaviour
{
    public CameraDataSO cameraDataSO;

    //public CameraController cameraController;

    public float relativeCoefficient;

    private RectTransform rectTransform;

    public GameObject squareLeft;

    public GameObject squareRight;

    private void Awake()
    {
        //cameraController = new CameraController();

        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Move(cameraDataSO.cameraPosition);
    }

    public void Move(Vector3 newPosition)
    {
        rectTransform.anchoredPosition = new Vector3(-rectTransform.sizeDelta.x * ((newPosition.x - squareLeft.transform.position.x) / (squareRight.transform.position.x - squareLeft.transform.position.x)  ) * relativeCoefficient, 0, 0);
    }

}
