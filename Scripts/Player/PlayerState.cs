using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerStateMachine stateMachine;

    protected Player player;

    protected Rigidbody2D rb;
    protected string animatorBoolName;
    //水平输入
    protected float xInput;
    protected float yInput;
    //每个状态的计时器，递减判断时间
    protected float stateTimer;

    //记录是否攻击结束 
    public bool triggerCalled;

    public PlayerState(Player player,PlayerStateMachine stateMachine,string animatorBoolName)
    {
        this.player = player;
        this.animatorBoolName = animatorBoolName;
        this.stateMachine = stateMachine;
    }

    public virtual void OnEntry()
    {
        triggerCalled = false;
        player.animator.SetBool(animatorBoolName, true);
        rb = player.rb;
    }

    public virtual void OnUpdate()
    {
        stateTimer -= Time.deltaTime;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.animator.SetFloat("Yveloctity", rb.velocity.y);
    }

    public virtual void OnExit()
    {
        player.animator.SetBool(animatorBoolName, false);
    }    
}
