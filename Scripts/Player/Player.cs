using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;

public class Player : MonoBehaviour
{


    [Header("移动跳跃相关")]
    public float movrSpeed;
    public float jumpForce;


    #region  组件相关
    public Animator animator;
    public Rigidbody2D rb;

    public PlayerStateMachine stateMachine ;
    #endregion

    #region  状态相关
    
    public PlayerIdleState playerIdleState {get;private set;}
    public PlayerMoveState playerMoveState {get;private set;}


    #endregion

    void Awake()
    {
        stateMachine = new PlayerStateMachine();// 创建状态机
      
        playerIdleState = new PlayerIdleState(this, stateMachine, "Idle");// 创建Idle状态
        playerMoveState = new PlayerMoveState(this, stateMachine, "Move");// 创建Move状态

    }

    void Start()
    {

        animator = this.GetComponentInChildren<Animator>();
        rb = this.GetComponent<Rigidbody2D>();

        stateMachine.InitState(playerIdleState);   // 初始化状态机
    }
    void Update()
    {
        if(stateMachine == null)
        {   
            Debug.Log("没有状态机");
            return;
        }
        stateMachine.currentState.OnUpdate();
    }
}
