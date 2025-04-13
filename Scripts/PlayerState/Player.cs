using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    
    public  PlayerController playerController;
    
    private PlayerInfo playerInfo;
    private PlayerStats playerStats;
    public Player (PlayerInfo playerInfo, PlayerStats playerStats)
    {
        this.playerInfo = playerInfo;
        this.playerStats = playerStats;
    }
    
    override protected void Start()
    {
        base.Start();
        // controller = GetComponentInChildren<PlayerController>();
    }

    protected override void Awake()
    {
        base.Awake();
          // playerController = new PlayerController(this); //实例化角色控制器

    }

}
