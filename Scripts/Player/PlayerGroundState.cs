using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player _player, PlayerStateMachine _stateMachine, string _animatorBoolName) : base(_player, _stateMachine, _animatorBoolName)
    {
    }

    public override void OnEntry()
    {
        base.OnEntry();
    }
    public override void OnUpdate()
    {

        base.OnUpdate();

        if(xInput==0)//如果x轴输入为0
        {
            stateMachine.ChangeState(player.playerIdleState);
        }
        if(Input.GetKeyDown(KeyCode.Space) && player.IsGround()) // 按下空格键 并且在地面上
        {
            //跳跃
            Debug.Log("向前跳跃");
            stateMachine.ChangeState(player.playerJumpForwordState);

        }
        

    }
    public override void OnExit()
    {
        base.OnExit();
    }
}
