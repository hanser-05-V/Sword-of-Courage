using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class Entity : MonoBehaviour // 实体类的公共行为
{
    [Header("对象相关")]
    public Transform targetObject ; // 目标对象

    [Header("检测相关")]
    public Transform groundCheck; // 地面检测点
    public float groundChenkDistance ; // 地面检测距离
    
    public Transform wallCheck; // 墙壁检测点
    public float wallCheckDistance; // 墙壁检测距离

    public Transform headCheck; // 头部检测点
    public float headCheckDistance; // 头部检测距离
    public LayerMask groundLayer; // 地面层

    [Header("组件相关")]
    [SerializeField] protected FSM fsm; // 状态机组件
    [SerializeField] protected Animator animator; // 动画组件
    [SerializeField] protected SpriteRenderer sp; // 角色渲染器组件
    public Rigidbody2D rb; // 刚体组件
    
    public int facing = 1; // 角色的朝向，1为右，-1为左
    private bool isFacingRight = true; // 角色的默认朝向，true为右，false为左

    public float xInput {get ; private set;} // 角色的水平方向输入值


    public SkillManager skill {get ; private set ; }  // 技能管理组件
    
    protected  virtual void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal"); // 获取水平方向输入值 
    }

    #region 速度相关方法

    public void SetVecolity(float xVelocity, float yVelocity)//设置速度
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }

    public void SetZeroVecolity()//设置速度为0
    {
        rb.velocity = new Vector2(0,0);
    }
    #endregion


    #region 动画相关方法  
    public virtual void ChangeState(StateType stateType) // 切换状态方法
    {
        fsm.ChangeState(stateType);
    }
    public virtual void SetBool(string parameterName, bool value) //设置bool
    {
        animator.SetBool(parameterName, value);        
    }

    public virtual void SetFloat(string parameterName, float value)//设置float
    {
        animator.SetFloat(parameterName, value);
    }

    public virtual void SetInt(string parameterName, int value) //设置int
    {
        animator.SetInteger(parameterName, value);
    }
    #endregion


    #region 角色翻转
    public void Flip()
    {
        facing = facing * -1; // 角色的朝向取反
        isFacingRight =!isFacingRight; // 角色的默认朝向取反
        this.transform.Rotate(0, 180, 0); // 角色的物理朝向也要跟着改变
    }   
    public void FlipController(float xVelocity)
    {
        if(xVelocity>0 && !isFacingRight)
        {
            Flip();
        }
        else if(xVelocity<0 && isFacingRight)
        {
            Flip();
        }
    }
    #endregion

    #region 检测相关

    public bool IsGroundDetected() // 检测是否在地面上
    {
        if(Physics2D.Raycast(groundCheck.position,Vector2.down,groundChenkDistance,groundLayer))
        {
            
            return true;   
        }
        else
        {
            return false;
        }
    
    }
    public bool IsWallDetected()// 检测是否在墙壁上
    {

        if(Physics2D.Raycast(wallCheck.position,Vector2.right * facing,wallCheckDistance,groundLayer))
        {
            return true;
        }
        else 
        {
            return false;
        }
    } 
    
    public bool IsheadDetected() //头部检测到物体
    {   
        if(Physics2D.Raycast(headCheck.position,Vector2.up,headCheckDistance,groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion
    protected virtual void  Awake()
    {
        skill = SkillManager.Instance;
    }
    protected virtual void  Start()
    {
        
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position , new Vector3(groundCheck.position.x, groundCheck.position.y - groundChenkDistance, groundCheck.position.z));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x+ wallCheckDistance, wallCheck.position.y , wallCheck.position.z));
        Gizmos.DrawLine(headCheck.position, new Vector3(headCheck.position.x, headCheck.position.y + headCheckDistance, headCheck.position.z));
    }
    
}
