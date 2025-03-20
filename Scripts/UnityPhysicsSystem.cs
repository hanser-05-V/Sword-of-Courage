using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


public interface IPhysicsSystem
{
    void ApplyJumpForece(float jumpForce , ForceMode2D forceMode);//添加跳跃力
    bool IsGrounded();//判断是否在地面上

    void SetVelocity(float xVelocity, float yVelocity);

    void ChangeXvelocity(float xVelocity); //改变x方向速度
    void ChangeYvelocity(float yVelocity); //改变y方向速度

    float GetYVelocity(); //获取y方向速度
    void SetZeroVelocity();
}


public class UnityPhysicsSystem : MonoBehaviour , IPhysicsSystem
{

    private Rigidbody2D rb;

    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void ApplyJumpForece(float jumpForce, ForceMode2D forceMode) //添加跳跃力
    {
        rb.AddForce(new Vector2(0,jumpForce), forceMode);
    }

    public bool IsGrounded() //判断是否在地面上
    {
        return isGrounded;
    }


    public Rigidbody2D GetRigidbody2D()
    {
        return rb;
    }
    #region 速度相关
    public void SetVelocity(float xVelocity, float yVelocity) //设置速度
    {
       rb.velocity = new Vector2(xVelocity, yVelocity);
    }

    public void SetZeroVelocity() //设置速度为0
    {
        rb.velocity = Vector2.zero;
    }

    public void ChangeXvelocity(float xVelocity)
    {
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }

    public void ChangeYvelocity(float yVelocity)

    {
       rb.velocity = new Vector2(rb.velocity.x, yVelocity);
    }

    public float GetYVelocity()
    {
        return rb.velocity.y;
    }

    #endregion
}
