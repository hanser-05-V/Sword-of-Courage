using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpTest : MonoBehaviour
{
    public float jumpForce = 10f; // 最大跳跃力
    public float maxJumpHeight = 5f; // 最大跳跃高度
    public float jumpTime = 0.5f; // 跳跃持续时间
    private float currentJumpTime = 0f; // 当前跳跃时间

    private bool isJumping = false;
    private bool isGrounded = false; // 检查角色是否着地
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 检测角色是否在地面上（避免空中多次跳跃）
        isGrounded = Mathf.Abs(rb.velocity.y) < 0.1f;

        // 如果角色着地，按下空格键可以跳跃
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            currentJumpTime = 0f; // 重置跳跃时间
            rb.velocity = new Vector2(rb.velocity.x, 0); // 确保在起跳时清除之前的垂直速度
        }

        // 如果空格键持续按下并且角色还在跳跃
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            // 增加跳跃时间，控制跳跃高度
            currentJumpTime += Time.deltaTime;

            // 计算跳跃力，使其随着跳跃时间增长而增加，直到达到最大跳跃时间
            if (currentJumpTime <= jumpTime)
            {
                float force = Mathf.Lerp(0, jumpForce, currentJumpTime / jumpTime);
                rb.velocity = new Vector2(rb.velocity.x, force); // 施加跳跃力
            }
        }

        // 如果空格键松开或达到最大跳跃时间，则停止增加跳跃力
        if (Input.GetKeyUp(KeyCode.Space) || currentJumpTime >= jumpTime)
        {
            isJumping = false;
        }
    }
}
