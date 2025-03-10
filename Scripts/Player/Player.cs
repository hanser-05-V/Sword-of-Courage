using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("攻击相关")]
    public float[] AttackMoment;
    public float counterTime;
   
    public bool isBusy;
    [Header("移动相关")]
    public float moveSpeed;
    public float jumpForce;
    public float swordReturnImpact;
    private float defaultMoveSpeed;
    private float defultJumpForce;


    [Header("Dash Info")]

    [SerializeField] private float dashCoolDown;  //冲刺冷却时间
    private float dashUsageTime;    //冲刺使用时间
    public float dashSpeed;//冲刺速度

    public float dashDuration;   //冲刺冷却时间
    private float defultDashSpeed;
    public float dashDir { get;private set; }   //冲刺方向
    [Space]

    public bool canClone;

    public SkillManager skill { get; private set; }
    public GameObject sword { get; private set; }

    #region 状态相关
    public PlayerStateMachine stateMachine;

    public PlayerIdleState idleState;

    public PlayerMoveState moveState;

    public PlayerJumpState jumpState;
    public PlayerAirState airState;
    public PlayerWallSlideState wallSlideState;
    public PlayerWallJumpState wallJumpState;

    public PlayerDashState dashState;

    public PlayerAttack attackState;

    public PlayerCounterAttackState counterAttackState;


    public PlayerAimSwordState aimSwordState;
    public PlayerCatchSwordState catchSwordState;

    public PlayerBlackholeState blackHoleState;

    public PlayerDeadState deadState;
    #endregion


    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState  = new PlayerAirState(this, stateMachine, "Jump");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        attackState = new PlayerAttack(this, stateMachine, "Attack");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");

        aimSwordState = new PlayerAimSwordState(this, stateMachine, "AimSword");
        catchSwordState = new PlayerCatchSwordState(this, stateMachine, "CatchSword");

        blackHoleState = new PlayerBlackholeState(this, stateMachine, "Jump");

        deadState = new PlayerDeadState(this, stateMachine, "Dead");
    }
    protected override void Start()
    {
        base.Start();
        skill = SkillManager.Instance;
        stateMachine.InitState(idleState);

        defaultMoveSpeed = moveSpeed;
        defultJumpForce = jumpForce;
        defultDashSpeed = dashSpeed;
    }

    protected override void Update()
    {
        stateMachine.currentState.OnUpdate();
        CheckDashInput();

    }

    public override void SlowEnityBy(float _slowPercentage, float _slowDuration)
    {
        moveSpeed = moveSpeed * (1 - _slowPercentage);
        jumpForce = jumpForce * (1 - _slowPercentage);
        dashSpeed = dashSpeed * (1 - _slowPercentage);

        animator.speed = animator.speed * (1-_slowPercentage);

        Invoke("ReturnDefultSpeed", _slowDuration);
    }

    protected override void ReturnDefultSpeed()
    {
        base.ReturnDefultSpeed();
        moveSpeed = defaultMoveSpeed;
        jumpForce = defultJumpForce;
        dashSpeed = defultDashSpeed;
    }
    public IEnumerator BusyFor(float waitTime)
    {
        
        isBusy = true;
        yield return new WaitForSeconds(waitTime);
        isBusy = false;
    }

    //检测冲刺输入
    public void CheckDashInput()
    {
        //冲刺遇到墙壁
        if (IsWallDetected())
            return;

        if (Input.GetKeyDown(KeyCode.Q) && SkillManager.Instance.dash.CanUseSkill())
        {
            
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
                dashDir = faceDir;
            
            stateMachine.ChangeState(dashState);
        }
    }
    
    //攻击动画结束
    public bool AnimatorTigger()
    {
        return stateMachine.currentState.triggerCalled = true;
    }


    public void AssignNewSword(GameObject newSword)
    {
        sword = newSword;
    }
    public void CatchSword()
    {
        stateMachine.ChangeState(catchSwordState);
        Destroy(sword);
    }

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
    //public void ExitBlackholeState()
    //{
    //    stateMachine.ChangeState(airState);
    //}
}
