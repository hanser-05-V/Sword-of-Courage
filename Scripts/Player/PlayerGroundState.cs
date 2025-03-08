using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
    {
    }

    public override void OnEntry()
    {
        base.OnEntry();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        //保证冲刺解释落下后 能播放下落动画
        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);
        //必须是地板层级上面才可以跳跃 （敌人身体等不能跳跃）
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.jumpState);
        //点击鼠标进入攻击
        if (Input.GetMouseButtonDown(0) && !player.isBusy )
        { 
            stateMachine.ChangeState(player.attackState);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            stateMachine.ChangeState(player.counterAttackState);
        }
        if (Input.GetMouseButtonDown(1) && HasNoSword())
        {
            stateMachine.ChangeState(player.aimSwordState);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            stateMachine.ChangeState(player.blackHoleState);
        }

    }
    private bool HasNoSword()
    {
        if (!player.sword)
        {
            return true;
        }
        player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }
}
