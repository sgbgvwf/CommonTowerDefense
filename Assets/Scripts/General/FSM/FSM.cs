using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using System;





public class FSM
{

    private IState _currentState; // 当前激活的状态
 
    public Dictionary<Enum, IState> StateDictionary;
    public Blackboard blackboard;

    public FSM(Blackboard blackboard)
    {
        this.StateDictionary = new Dictionary<Enum, IState>();
        this.blackboard = blackboard;
    }


    public void AddState(Enum State, IState state)
    {
        if(StateDictionary.ContainsKey(State))
        {
            Debug.Log("[AddState] >>>>>>>>>> map has contain key: " + State);
            return;
        }
        StateDictionary.Add(State, state);


    }

    //切换状态
    public void SwitchState(Enum newState)
    {
        _currentState?.OnExit();

        _currentState = StateDictionary[newState];

        _currentState?.OnEnter();
    }

    //每帧调用
    public void UpdateState()
    {
        _currentState?.OnUpdate();
    }

}
