using System;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour //状态机基类
{

    public IState currentState{get;private set;}
    public Dictionary<StateType,IState> stateDic = new Dictionary<StateType, IState>(); //状态机容器

    public void ChangeState(StateType stateType) // 切换状态方法
    {
        if(currentState!=null)
           currentState.OnExit(null,null);//退出当前状态
        currentState = stateDic[stateType]; //更换状态
        currentState.OnEnter(null,null);//进入新状态
    }

    public void InitState(StateType stateType) //初始化状态
    {
        currentState = stateDic[stateType]; //更换状态
        currentState.OnEnter(null,null);//进入新状态
    }
}
