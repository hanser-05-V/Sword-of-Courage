using System;
using System.Collections.Generic;
using UnityEngine;

public enum StateType //状态枚举
{
    idle,
    move,jump,
    attack1,attack2,attaack3,
    died, dash,
}

[Serializable]

public class FSM : MonoBehaviour //状态机基类
{
    [Header("配置文件")]
    [SerializeField] private PlayerInfo playerInfo; //运行时 可修改 配置（速度）
    [SerializeField] private PlayerStats playerStats;//默认加载数据 （血量）
    [Space]
    [SerializeField] private PlayerController playerController;//角色控制器 处理逻辑切换

    private IState currentState; //当前状态
    private Dictionary<StateType,IState> stateDic = new Dictionary<StateType, IState>(); //状态机容器

    void Start()
    {
        //加入状态
        stateDic.Add(StateType.idle, new IdleState(playerController));
        stateDic.Add(StateType.move, new MoveState(playerController));

        ChangeState(StateType.idle); //初始默认为idle状态

    }
    void Update()
    {
        currentState?.Onupdate(playerInfo,playerStats); //把当前状态的update方法调用
    }

    public void ChangeState(StateType stateType) // 切换状态方法
    {
        if(currentState!=null)
           currentState.OnExit(playerInfo,playerStats);//退出当前状态
        currentState = stateDic[stateType]; //更换状态
        currentState.OnEnter(playerInfo,playerStats);//进入新状态
    }
}
