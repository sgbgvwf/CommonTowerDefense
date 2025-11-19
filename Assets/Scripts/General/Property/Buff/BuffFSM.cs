using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffFSM : FSM
{
    private IState _currentState; // 当前激活的状态

    public Dictionary<Enum, bool> NowState;//用字典记录buff的存在状态

    private BuffStateManager _buffStateManager;

    public BuffFSM(Blackboard blackboard) : base(blackboard)
    {
        this.NowState = new Dictionary<Enum, bool>();

        this.StateDictionary = new Dictionary<Enum, IState>();
        this.blackboard = blackboard;

    }

    public new void AddState(Enum State, IState state)
    {
        if (StateDictionary.ContainsKey(State))
        {
            Debug.Log("[AddState] >>>>>>>>>> map has contain key: " + State);
            return;
        }
        StateDictionary.Add(State, state);

        NowState.Add(State, false);

    }

    //进入状态
    public void EnterState(Enum state)
    {
        
        _currentState?.OnEnter();
        NowState[state] = true;
        

    }

    //退出状态
    public void ExitState(Enum state)
    {

        _currentState?.OnExit();
        NowState[state] = false;
    }






}
