using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayerState : MonoBehaviour
{
    
    protected  PlayerStateMachine stateMachine;//玩家状态机
    protected Player  player;//玩家对象

    protected string animatorBoolName;//动画切换名称
    protected float xInput;//X轴输入

    protected Rigidbody2D rb; //玩家刚体

    public PlayerState(Player _player,PlayerStateMachine _stateMachine , string _animatorBoolName)
    {
        player = _player;
        stateMachine = _stateMachine;
        animatorBoolName = _animatorBoolName;
    }

    public virtual void OnEntry()
    {
        player.animator.SetBool(animatorBoolName, true);

        rb = player.rb;
    }

    public virtual void OnUpdate()
    {
        xInput = Input.GetAxis("Horizontal");   
    }

    public virtual void OnExit()
    {
        player.animator.SetBool(animatorBoolName, false);
    }   
}
