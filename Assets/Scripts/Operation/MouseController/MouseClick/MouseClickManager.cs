using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseClickManager : MonoBehaviour
{
    public InputController inputControl;

    public MouseRelativePosition mouseRelativePosition;

    public MouseStateDetection mouseStateDetection;






    private void Awake()
    {
        inputControl = new InputController();

        inputControl.ClickOperation.LeftClick.performed += LeftClick;
        inputControl.ClickOperation.RightClick.performed += RightClick;



    }


    private void OnEnable()
    {
        inputControl.Enable();
    }
    //禁用
    private void OnDisable()
    {
        inputControl.Disable();
    }



    public void LeftClick(InputAction.CallbackContext leftClick)
    {
        //mouseRelativePosition.enabled = true;
        //Debug.Log("leftClick.performed");
        if(mouseStateDetection.currentState == MousePointState.DefenseTower)
        {
            Debug.Log("拆除");
        }







    }

    public void RightClick(InputAction.CallbackContext rightClick)
    {
        //Debug.Log("rightClick.performed");
        if(mouseStateDetection.currentState == MousePointState.Place)
        {
            Debug.Log("建造");
        }

        if (mouseStateDetection.currentState == MousePointState.DefenseTower)
        {
            Debug.Log("查看");
        }











    }


}
