using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum StateType //状态枚举
{
    Idle,
    Move,MoveBefore,
    Jump,DownToGround,
    Down,DownRepeat,DownHeavy,
    Attack,Attack2,Attack3,
    Died, Dash,
    Air,Ground,
}

public class Player : Entity
{
    public FSM playerFSM {get; private set;} //玩家状态机
    public PlayerInfo playerInfo ; //玩家信息 
    public PlayerController playerController ;//玩家控制器

    public PlayerInput playerInput {get; private set;} //角色输入组件
    
    private InputAction jumpActionTest; //跳跃输入

    protected override void Awake()
    {
        base.Awake();
        playerInput = this.GetComponent<PlayerInput>(); //获取角色输入组件
        playerFSM = new FSM(); //实例化玩家状态机

        playerFSM.stateDic.Add(StateType.Idle,new PlayerIdleState(this,playerFSM,"Idle"));
        playerFSM.stateDic.Add(StateType.Move,new PlayerMoveState(this,playerFSM,"Move"));
        playerFSM.stateDic.Add(StateType.MoveBefore,new PlayerMoveBeforeState(this,playerFSM,"MoveBefore"));
        playerFSM.stateDic.Add(StateType.Jump,new PlayerJumpState(this,playerFSM,"Jump"));
        playerFSM.stateDic.Add(StateType.Air,new PlayerAirState(this,playerFSM,"Jump"));
        playerFSM.stateDic.Add(StateType.Dash,new PlayerDashState(this,playerFSM,"Dash"));

        playerFSM.stateDic.Add(StateType.DownToGround,new PlayerDownToGroundState(this,playerFSM,"DownToGround"));
        playerFSM.stateDic.Add(StateType.Down,new PlayerDownState(this,playerFSM,"Down"));
        playerFSM.stateDic.Add(StateType.DownRepeat,new PlayerDownReaptState(this,playerFSM,"DownRepeat"));

        playerFSM.stateDic.Add(StateType.Attack,new PlayerCombuAttackState(this,playerFSM,"Attack"));

        playerFSM.InitState(StateType.Idle); //初始化状态
    }
    protected override void Start()
    {
        base.Start();
        // playerController.CreatShadowBegain(); //创建初始残影列表

    }
    protected override void Update()
    {
        base.Update();
        playerFSM.currentState.Onupdate(playerInfo,null);//更新状态状态机
        
        if((Input.GetKeyDown(KeyCode.Q ) || Input.GetButtonDown("Fire3"))  && SkillManager.Instance.dash.CanUseSkill())
        {
            
            StartCoroutine(playerController.StartDash(this));
            
        }
    }

}
