using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : Entity
{   

   [Header("移动相关")]
    public float moveSpeed;
    public float idleTime;
    public float balletTime;
    private float defaultMoveSpeed;

    [Header("巡逻点相关")]
    [SerializeField] protected Transform[] patrolPoints; //巡逻点
    protected int currentPatrolPoint;//当前巡逻点索引
    public bool isPatorling;//是否正在巡逻

    [Header("攻击检测相关")]
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform attackCheckPos;
    public float attackDIstance;
    public float ballteleChenkDistance;
    
    [Header("击退相关")]
    [SerializeField] private float hitSpeed;
    [SerializeField] Vector2 hitDirction;
    public bool isHit;
    // public  new int facing = -1; //敌人默认朝向为左
    // private bool isFacingRight = false; //敌人默认朝向为左

    public EnemyFSM enemyFSM { get; private set; }
    public EnemyStateType enemyStateType;
    protected override void  Awake()
    {
        base.Awake();
        enemyFSM = new EnemyFSM(); //初始化敌人状态机
    }
    protected override void  Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

   #region 速度相关方法
    public override void SetVecolity(float xVelocity, float yVelocity)//设置速度
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }
    public override void SetZeroVecolity()//设置速度为0
    {
        rb.velocity = new Vector2(0,0);
    }
    #endregion

    #region 角色翻转
    // public override void Flip()
    // {
    //     facing = facing * -1; // 角色的朝向取反
    //     isFacingRight =!isFacingRight; // 角色的默认朝向取反
    //     this.transform.Rotate(0, 180, 0); // 角色的物理朝向也要跟着改变
    // }   
    // public override void FlipController(float xVelocity)
    // {
    //     if(xVelocity>0 && !isFacingRight)
    //     {
    //         Flip();
    //     }
    //     else if(xVelocity<0 && isFacingRight)
    //     {
    //         Flip();
    //     }
    // }
    #endregion

    //玩家检测相关
    public RaycastHit2D isPlayerDetected()
    {
        float checkDistance  = 50f; //敌人检测距离为 50

        return  Physics2D.Raycast(attackCheckPos.position, Vector2.right * facing, checkDistance, whatIsPlayer);
    }




    //击退效果
    public void GetHit(float _hitTime)
    {
        StartCoroutine(Hit(_hitTime));
    }
    IEnumerator Hit(float hitTime)
    {
        isHit = true;
        
        //TODO: 实现击退效果 反向
        rb.velocity = new Vector2(-facing * hitSpeed * hitDirction.x, hitSpeed * hitDirction.y);

        yield return new WaitForSeconds(hitTime);

        isHit = false;

    }

}
