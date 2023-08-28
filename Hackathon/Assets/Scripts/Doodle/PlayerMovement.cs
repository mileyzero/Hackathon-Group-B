using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Rigidbody2D rb;
    public float moveX;
    public GameObject manager;
    public GameObject tutorial;
    // Start is called before the first frame update
    void Start()
    {
        tutorial.SetActive(true);
        manager = GameObject.FindGameObjectWithTag("doodlemanager");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() //handles the player input to move the player
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed;
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) //move left
        {
            tutorial.SetActive(false);
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = true; //flip sprite
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) //move right
        {
            tutorial.SetActive(false);
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

    private void OnTriggerEnter2D(Collider2D collision) //collison with resources
    {
            if (collision.gameObject.tag == "doodleMoney")
            {
                manager.GetComponent<Manager>().PlayCollect();
                manager.GetComponent<Manager>().moneyCount += 1;
                Destroy(collision.gameObject);
            }

            else if (collision.gameObject.tag == "doodleHappy")
            {
                manager.GetComponent<Manager>().PlayCollect();
                manager.GetComponent<Manager>().happinessCount += 1;
                Destroy(collision.gameObject);
            }

            else if (collision.gameObject.tag == "doodlePopular")
            {
                manager.GetComponent<Manager>().PlayCollect();
                manager.GetComponent<Manager>().popularityCount += 1;
                Destroy(collision.gameObject);
            }

    }
}
