using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorTrigger : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float downForce = 5;

    [SerializeField] private float jumpForce = 1;

    private void AddDownForce()
    {
        Debug.Log("AddDownForce");

        rb.velocity = new Vector2(rb.velocity.x , -downForce);

    }

    private void AddJumpForce()
    {
        Debug.Log("111");
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
