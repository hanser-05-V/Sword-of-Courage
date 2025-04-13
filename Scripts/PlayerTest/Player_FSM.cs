using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FSM 
{
    public IState currentState {get; private set;}

    public Dictionary<StateType,IState> stateDic = new Dictionary<StateType,IState>();
    public void InitState(StateType begingState) //初始化状态
    {
        currentState = stateDic[begingState];
        currentState.OnEnter(null,null);
    }

    public void ChangeState(StateType newState) //切换状态方法
    {
        if(currentState != null)
        {
            currentState.OnExit(null,null);
        }
        currentState = stateDic[newState];
        currentState.OnEnter(null,null);
    }

}
