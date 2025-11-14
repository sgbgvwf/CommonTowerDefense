using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseClickManager : MonoBehaviour
{
    public InputController inputControl;

    public MouseRelativePosition mouseRelativePosition;









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
    //½ûÓÃ
    private void OnDisable()
    {
        inputControl.Disable();
    }



    public void LeftClick(InputAction.CallbackContext leftClick)
    {
        //Debug.Log("leftClick.performed");








    }

    public void RightClick(InputAction.CallbackContext rightClick)
    {
        //Debug.Log("rightClick.performed");













    }


}
