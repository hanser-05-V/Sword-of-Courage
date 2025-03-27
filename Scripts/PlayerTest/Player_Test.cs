using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Test : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;



    [Header("Jump Settings")]
   
    public float shortJumpForce = 12f;      // 短跳基础力量
    public float maxLongJumpForce = 24f;   // 最长大跳力量

    public float changeSpeed;
    public float shortTimeScale;
    public float longTimeScale;
   
    public  float jumpTimmer;
    public bool isGrounded =true;
   
    private int facing = 1;
    private bool isFacingRight = true;

    private float xInput;
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    
    }
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if(xInput != 0 )
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
            animator.SetBool("Idle", false);
            animator.SetBool("Move",true);
            
        }

        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetBool("Idle", true);
            animator.SetBool("Move", false);
        }
        if(Input.GetKey(KeyCode.Space) && isGrounded)
        {
          
            jumpTimmer = Time.deltaTime * changeSpeed;
      
            if(jumpTimmer <= shortTimeScale )
            {
                rb.velocity = new Vector2(rb.velocity.x, shortJumpForce);
                Debug.Log("地面小跳");
                jumpTimmer = 0;
            }
            else
            {
                Debug.Log(jumpTimmer);
                rb.velocity = new Vector2(rb.velocity.x, maxLongJumpForce);
                Debug.Log("地面长跳");
                jumpTimmer = 0;
            }
        }
      
        FlipController(rb.velocity.x);
    }
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

 
}

