using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuffFSM : FSM
{
    private IState _currentState; // 当前激活的状态

    public List<Enum> NowState;//用字典记录buff的存在状态

    private BuffStateManager _buffStateManager;

    public BuffFSM(Blackboard blackboard) : base(blackboard)
    {
        this.NowState = new List<Enum>();

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

        //NowState.Add(State);

    }

    //进入状态
    public void EnterState(Enum state)
    {
        StateDictionary[state]?.OnEnter();
        NowState.Add(state);
    }

    //退出状态
    public void ExitState(Enum state)
    {
        StateDictionary[state]?.OnExit();
        NowState.Remove(state);
    }

    //对每一个激活的状态进行更新
    public void UpdateStates()
    {
        List<Enum> NowStateCopy = NowState.ToList();
        foreach(var state in NowStateCopy)
        {
            
            StateDictionary[state].OnUpdate();

        }
    }




}
