using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public CameraPositionSO cameraPositionSO;

    public InputController inputController;

    private Rigidbody2D rb;

    public GameObject virtualCamera;

    public float cameraMoveSpeed;

    

    public Vector2 inputDirection;

    //广播的事件：相机的坐标


    


    private void Awake()
    {



        inputController = new InputController();

        rb = GetComponent<Rigidbody2D>();


        


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
        inputDirection = inputController.Camera.Move.ReadValue<Vector2>();
    }


    private void FixedUpdate()
    {
        CameraMoveUpdate();

        CameraMove(virtualCamera.transform.position);
    }


    public void CameraMoveUpdate()
    {
        rb.velocity = new Vector2(
            inputDirection.x * cameraMoveSpeed * Time.deltaTime,
            inputDirection.y * cameraMoveSpeed * Time.deltaTime
        );
    }

    public void CameraMove(Vector3 position)
    {
        cameraPositionSO.cameraPosition = position;
    }






}
