using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveBeforeState : IState
{
    
    private PlayerController playerController;

    private AnimatorStateInfo animatorStateInfo;
    public PlayerMoveBeforeState(PlayerController playerController)
    {
        this.playerController = playerController;
     
    }
    public void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        animatorStateInfo = playerController.GetCurAnimStateInfo();
     
        playerController.Play("playerMoveBefore");

        Debug.Log("进入移动前倾状态");
        Debug.Log(animatorStateInfo.normalizedTime);
        Debug.Log(animatorStateInfo.length);
    }

    public void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
       
    }

    public void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {

        playerController.ChangeXvelocity(playerController.xInput * playerInfo.moveSpeed);


        if(animatorStateInfo.normalizedTime >=1.5f)
        {
            Debug.Log("前倾完毕，播放移动");
            playerController.ChangeState(StateType.Move);


        }
    }
}
