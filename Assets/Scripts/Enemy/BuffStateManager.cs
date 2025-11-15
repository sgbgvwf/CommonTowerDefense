using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffState
{
    Burn,

    Cold,

    Slow,

    Water
}

public class BuffStateManager : MonoBehaviour
{
    public BuffState currentState;















    private void SwitchState(BuffState newState)
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



    //进入新状态的初始化
    void EnterState(BuffState state)
    {


        Debug.Log($"状态加入：{state}");
    }


    //退出当前状态的清理
    void ExitState(BuffState state)
    {


        Debug.Log($"状态退出：{state}");

    }


}
