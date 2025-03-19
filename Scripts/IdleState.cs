using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{

    private PlayerController playerController;

    public IdleState(PlayerController playerController)
    {
        this.playerController = playerController; 
    }
    public void OnEnter(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        Debug.Log("进入Idle状态");
        playerController.Play("playerIdle");
    }

    public void OnExit(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        Debug.Log("退出Idle状态");
    }
    

    public void Onupdate(PlayerInfo playerInfo, PlayerStats playerStats)
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerController.ChangeState(StateType.move);
        }
    }
}
