using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class Entity : MonoBehaviour // 实体类的公共行为
{

    [Header("检测相关")]
    public Transform groundCheck; // 地面检测点
    public float groundChenkDistance ; // 地面检测距离
    
    public Transform wallCheck; // 墙壁检测点
    public float wallCheckDistance; // 墙壁检测距离
    public LayerMask groundLayer; // 地面层

    
    [SerializeField] private UnityPhysicsSystem physicsSystem; // 使用接口来访问物理系统
    
    protected int facing = 1; // 角色的朝向，1为右，-1为左
    private bool isFacingRight = true; // 角色的默认朝向，true为右，false为左

    [HideInInspector] public float xInput; // 角色的水平方向输入值
    protected  virtual void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal"); // 获取水平方向输入值
        FlipController(xInput);
    }

    #region 速度相关方法

    public void ApplyJumpForece(float jumpForce,ForceMode2D forceMode) //添加跳跃力
    {
        physicsSystem.ApplyJumpForece(jumpForce,forceMode);
    }
    public void SetVelocity(float xVelocity, float yVelocity) //设置速度
    {
        physicsSystem.SetVelocity(xVelocity, yVelocity);
        FlipController(xVelocity);

    }

    public void SetZeroVelocity()//设置速度为0
    {
        physicsSystem.SetZeroVelocity();
    }

    public void ChangeXvelocity(float xVelocity) //设置x轴速度
    {
        physicsSystem.ChangeXvelocity(xVelocity);
    }
    public void ChangeYvelocity(float yVelocity) //设置y轴速度
    {
        physicsSystem.ChangeYvelocity(yVelocity);
    }

    public Rigidbody2D GetRigidbody2D() //获取刚体
    {
        return physicsSystem.GetRigidbody2D();
    }

    public float GetYVelocity() //获取y轴速度
    {
        return physicsSystem.GetYVelocity();
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
    

    #endregion

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position , new Vector3(groundCheck.position.x, groundCheck.position.y - groundChenkDistance, groundCheck.position.z));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x+ wallCheckDistance, wallCheck.position.y , wallCheck.position.z));
    }
    
}
