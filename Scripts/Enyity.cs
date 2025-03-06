using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity: MonoBehaviour
{


    [Header("碰撞检测相关")]
    public Transform groundCheckPosition;
    public Transform wallCheckPosition;
    public float groundCheckDistance;
    public float wallCheckDistance;

    public LayerMask groundLayer;

    #region  组件相关

    #region 变量相关

    
    public int FacingDirection {get ; private set;} = -1; // 面朝方向 默认朝左
    protected bool isFacingLeft = true; // 是否面朝左边

    
    
    #endregion


    public Animator animator {get ;private set;}
    public Rigidbody2D rb {get ;private set;}

    #endregion
    protected virtual void Awake()
    {
        animator = this.GetComponentInChildren<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
    }
    protected virtual  void Start()
    {
        
        
    }
    
    protected virtual void Update()
    {
        
    }
    

    #region  速度设置相关

    //设置速度为0
    public virtual void SetZeroVelocity() 
    {
        rb.velocity = Vector2.zero;
    }

    //设置速度（带翻转）
    public virtual void SetVelocity(float _x, float _y)
    {

        rb.velocity = new Vector2(_x, _y);

        FlipController(_x);
    }

    #endregion


    #region  翻转相关


     //翻转方法
   public virtual void Flip()
    {
        FacingDirection *= -1;
        isFacingLeft = !isFacingLeft;
        this.transform.Rotate(0f, 180f, 0f);

        
    }

     //方法控制器（什么时候 调用翻转方法）
    public virtual void FlipController(float _x)
    {
        if(_x > 0 && isFacingLeft)
        {
            Flip();
        }
        else if (_x<0 && !isFacingLeft)
        {
            Flip();
        }
    
    }


    #endregion


    #region  检测相关

    //地面检测
    public virtual bool IsGround()
    {
        if(Physics2D.Raycast(groundCheckPosition.position,Vector2.down,groundCheckDistance,groundLayer))
        {
            return true;
        }
        else return false;
    }
    //墙检测
    public virtual bool IsWall()
    {
        if (Physics2D.Raycast(wallCheckPosition.position, Vector2.left, wallCheckDistance, groundLayer))
        {
            return true;
        }
        else return false;
    }
    #endregion


    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheckPosition.position,new Vector3(groundCheckPosition.position.x,groundCheckPosition.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheckPosition.position, new Vector3(wallCheckPosition.position.x - wallCheckDistance, wallCheckPosition.position.y));
    }


}
