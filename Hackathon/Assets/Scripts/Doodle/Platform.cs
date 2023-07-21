using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float jumpPower = 10f;
    public Animator animator;
    public Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.y <= 0f)
        {
            animator = collision.gameObject.GetComponent<Animator>();
            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if(rb != null )
            {
                animator.SetTrigger("Jump");
                Vector2 velocity = rb.velocity;
                velocity.y = jumpPower;
                rb.velocity = velocity;
            }
        }
    }
}
