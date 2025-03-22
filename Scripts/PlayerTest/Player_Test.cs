using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Test : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

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

        if(xInput != 0)
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
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
