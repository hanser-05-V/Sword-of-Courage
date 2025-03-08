using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchSwordState : PlayerState
{
    private Transform sword;
    public PlayerCatchSwordState(Player player, PlayerStateMachine stateMachine, string animatorBoolName) : base(player, stateMachine, animatorBoolName)
    {
    }

    public override void OnEntry()
    {
        base.OnEntry();
       
        sword = player.sword.transform;
        if (player.transform.position.x > sword.position.x && player.faceDir == 1)
            player.Filp();
        else if (player.transform.position.x < sword.position.x && player.faceDir == -1)
            player.Filp();

        rb.velocity = new Vector2(player.swordReturnImpact * -player.faceDir, rb.velocity.y);
    }

    public override void OnExit()
    {
        base.OnExit();
        player.StartCoroutine("BusyFor", 0.1f);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if(triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

  
}
