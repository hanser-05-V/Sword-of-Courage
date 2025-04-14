using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState :IState
{
    
    protected PlayerController playerController; //玩家动画控制器
    protected Player player; //玩家对象
    protected string animatorBoolName; //动画bool名称
    protected FSM fsm; //玩家状态机
    public SkillManager skill {get ; private set ; }  // 技能管理组件

    protected Rigidbody2D rb;    

    //检测输入相关
    protected float xInput;
    protected float yInput;
    protected float stateTimer; //状态持续时间计时器
    public bool triggerCalled; //记录攻击结束
    
    public PlayerState(Player player,FSM fsm,string animatorBoolName)
    {
        this.player = player;
        this.fsm = fsm;
        this.animatorBoolName = animatorBoolName;
    }
    public virtual void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        player.animator.SetBool(animatorBoolName, true);
        rb = player.rb;
        playerController = player.playerController;
        skill = SkillManager.Instance; //获取技能管理组件
    }

    public virtual void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        player.animator.SetBool(animatorBoolName, false);
    }

    public virtual void Onupdate(PlayerInfo playerInfo , PlayerStats playerStats)
    {
        stateTimer-=Time.deltaTime; //逐帧减少持续时间 进入攻击状态重置
        xInput = Input.GetAxisRaw("Horizontal"); //获取水平方向输入
        yInput = Input.GetAxisRaw("Vertical"); //获取垂直方向输入
        player.animator.SetFloat("Yvelocity",rb.velocity.y);
    }
 
}
