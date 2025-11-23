using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public InputController inputController;

    private Rigidbody2D rb;


    public float cameraMoveSpeed;


    public Vector2 inputDirection;


    private void Awake()
    {
        inputController = new InputController();

        rb = GetComponent<Rigidbody2D>();


        //订阅输入的事件

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
        CameraMove();
    }


    public void CameraMove()
    {
        rb.velocity = new Vector2(
            inputDirection.x * cameraMoveSpeed * Time.deltaTime,
            inputDirection.y * cameraMoveSpeed * Time.deltaTime
        );
    }








}
