using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAirState : PlayerState //玩家空中状态
{

    private InputAction jumpAction;

    public PlayerAirState(Player player, FSM fsm, string animatorBoolName) : base(player, fsm, animatorBoolName)
    {
    }

    public override void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.OnEnter(playerInfo, playerStats);
        jumpAction = player.playerInput.actions["Jump"];
        
        jumpAction.Enable();
    }

    public override void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.OnExit(playerInfo, playerStats);
    }

    public override void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        base.Onupdate(playerInfo, playerStats);
      
        if(xInput != 0) //空中水平方向有输入
        {
            player.SetVecolity(playerInfo.moveSpeed * xInput * 0.8f, rb.velocity.y);
        }
        
        //速度判断播放状态
        if(Mathf.Abs(rb.velocity.y) > playerInfo.downYvelocityMin)
        {
            fsm.ChangeState(StateType.Down);
        }
       

        if(player.IsGroundDetected()) //检测到地面 变为静止状态
        {
            
            player.SetVecolity(rb.velocity.x, 0);//设置Y速度为0

            if(xInput!=0) //落地瞬间水平方向有输入
            {
                fsm.ChangeState(StateType.Move);
            }
            else//落地瞬间水平方向无输入
            {
                fsm.ChangeState(StateType.DownToGround);
            }
            // if(Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
            // {
            //     fsm.ChangeState(StateType.Jump); 
            // } 
            if(jumpAction.triggered)
            {
                fsm.ChangeState(StateType.Jump); 
            }

        } 

        if(player.IsheadDetected()) //防止玩家之间飞出去
        {
        
            player.SetVecolity(0,rb.velocity.y);
            fsm.ChangeState(StateType.Down);
        }

        
    }

   
}
