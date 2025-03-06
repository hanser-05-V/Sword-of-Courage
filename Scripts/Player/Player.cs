using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;

public class Player : Entity
{

    [Header("移动跳跃相关")]
    public float movrSpeed;
    public float jumpForce;
    public int jumpCount {get;private set;}

    #region  组件相关
    public PlayerStateMachine stateMachine ;
    #endregion

    #region  状态相关
    public PlayerIdleState playerIdleState {get;private set;}
    public PlayerMoveState playerMoveState {get;private set;}
    public PlayerJumpForwordState playerJumpForwordState {get;private set;}
    public PlayerJumpBackState playerJumpBackState {get;private set;}


    #endregion

    override protected void Awake()
    {

        base.Awake();
        stateMachine = new PlayerStateMachine();// 创建状态机
      
        playerIdleState = new PlayerIdleState(this, stateMachine, "Idle");// 创建Idle状态
        playerMoveState = new PlayerMoveState(this, stateMachine, "Move");// 创建Move状态
        playerJumpForwordState = new PlayerJumpForwordState(this, stateMachine, "JumpForword");// 创建JumpForword状态
        playerJumpBackState = new PlayerJumpBackState(this, stateMachine, "JumpBack");// 创建JumpBack状态

    }

    override protected void Start()
    {
        base.Start();
        stateMachine.InitState(playerIdleState);   // 初始化状态机
    }
    override protected void Update()
    {
         
        stateMachine.currentState.OnUpdate();
    }
}
