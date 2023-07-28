using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Rigidbody2D rb;
    public float moveX;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed;
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = false;
        }

        
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = moveX;
        rb.velocity = velocity;
        SwapX();
    }

    private void SwapX() //swap the x value of player if the x position of the player goes over a certain range
    {
        if(this.gameObject.transform.position.x >=11f || this.gameObject.transform.position.x <= -11f)
        {
            this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x * -1f, this.gameObject.transform.position.y);
        }
    }
}
