using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public CameraDataSO cameraDataSO;

    public InputController inputController;

    private Rigidbody2D rb;

    public GameObject virtualCamera;

    [Header("位移灵敏度")]
    [HideInInspector]public Vector2 inputDirection;
    public float cameraMoveSensitivity;

    
    [Header("缩放灵敏度")]
    [HideInInspector]public Vector2 inputScale;
    public float cameraScaleSensitivity;
    


    //广播的事件：相机的坐标

    private void Awake()
    {
        inputController = new InputController();

        rb = GetComponent<Rigidbody2D>();

        
    }

    private void Start()
    {

    }

    //启用
    private void OnEnable()
    {
        inputController.Enable();
    }
    //禁用
    private void OnDisable()
    {
        inputController.Disable();
    }


    
    private void Update()
    {
        cameraMoveSensitivity = cameraDataSO.cameraMoveSensitivity;
        cameraScaleSensitivity = cameraDataSO.cameraScaleSensitivity;

        inputDirection = inputController.Camera.Move.ReadValue<Vector2>();
        inputScale = inputController.Camera.Zoom.ReadValue<Vector2>();
        //Debug.Log(inputScale);
        CameraScaleUpdate();
    }

    private void FixedUpdate()
    {
        CameraMoveUpdate();

        CameraMove(virtualCamera.transform.position);

        //CameraScaleUpdate();
    }

    public void CameraMoveUpdate()
    {
        rb.velocity = new Vector2(
            inputDirection.x * (cameraMoveSensitivity * 60) * Time.deltaTime,
            inputDirection.y * (cameraMoveSensitivity * 60) * Time.deltaTime
        );
    }

    public void CameraMove(Vector3 position)
    {
        cameraDataSO.cameraPosition = position;
    }

    public void CameraScaleUpdate()
    {
        virtualCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize -= inputScale.y * cameraScaleSensitivity / 8 * Time.deltaTime;

        if(virtualCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize < 3)
        {
            virtualCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 3;
        }
        else if(virtualCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize > 6)
        {
            virtualCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 6;
        }
    }



}
