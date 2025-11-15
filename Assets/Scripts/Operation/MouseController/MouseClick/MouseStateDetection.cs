using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
public enum MousePointState
{

    Air,//空气

    Place,//无物体

    DefenseTower//防御塔

}

public class MouseStateDetection : MonoBehaviour
{
    public MousePointState currentState;



    private void Update()
    {
        
        switch (currentState)
        {
            case MousePointState.Air:

                break;

            case MousePointState.Place:

                break;

            case MousePointState.DefenseTower:

                break;

        }

        //Debug.Log(currentState);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision);

        if (collision.tag == "Air")
        {
            SwitchState(MousePointState.Air);
        }
        else if (collision.tag == "DefenseTower")
        {
            SwitchState(MousePointState.DefenseTower);
        }
        else if (collision.tag == "Ground" && currentState != MousePointState.DefenseTower)
        {
            SwitchState(MousePointState.Place);
        }

        
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        SwitchState(MousePointState.Air);
    }
    



    private void SwitchState(MousePointState newState)
    {
        // 1. 退出当前状态（可选：清理旧状态的残留逻辑）
        ExitState(currentState);

        // 2. 更新当前状态为新状态
        currentState = newState;

        // 3. 进入新状态（可选：初始化新状态的参数）
        EnterState(newState);

        //调试状态切换信息
        Debug.Log($"状态切换：{currentState}");
    }
    
    //退出当前状态的清理
    void ExitState(MousePointState state)
    {
        /*
        switch (state)
        {
            case MousePointState.Place:

                break;
            case MousePointState.Tower:

                break;
        }
        */
    }

    //进入新状态的初始化
    void EnterState(MousePointState state)
    {

    }
    
    
    


    
}
