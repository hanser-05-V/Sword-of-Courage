using System;
using System.Collections.Generic;
using UnityEngine;

public enum StateType //状态枚举
{
    Idle,
    Move,MoveBefore,
    Jump,DownToGround,
    Down,DownRepeat,DownHeavy,
    Attack1,Attack2,Attack3,
    Died, Dash,
    Air,Ground,
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


    void Awake()
    {
        // playerController = new PlayerController(this); //实例化角色控制器

        //加入状态
        stateDic.Add(StateType.Idle, new PlayerIdleState(playerController,"Idle"));
        stateDic.Add(StateType.Move, new PlayerMoveState(playerController,"Move"));
        stateDic.Add(StateType.MoveBefore, new PlayerMoveBeforeState(playerController,"MoveBefore"));
        stateDic.Add(StateType.Jump,new PlayerJumpState(playerController,"Jump"));
        stateDic.Add(StateType.DownToGround,new PlayerDownToGroundState(playerController,"DownToGround"));
        stateDic.Add(StateType.Down,new PlayerDownState(playerController,"Down"));
        stateDic.Add(StateType.DownRepeat,new PlayerDownReaptState(playerController,"DownRepeat"));

        stateDic.Add(StateType.Air,new PlayerAirState(playerController,"Jump"));
        // stateDic.Add(StateType.Ground,new PlayerGroundState(playerController));
        stateDic.Add(StateType.Dash,new PlayerDashState(playerController,"Dash"));
    }
    void Start()
    {
        
        InitState(StateType.Idle); // 初始化状态为 静止

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

    public void InitState(StateType stateType) //初始化状态
    {
        currentState = stateDic[stateType]; //更换状态
        currentState.OnEnter(playerInfo,playerStats);//进入新状态
    }
}
