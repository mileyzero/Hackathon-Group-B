using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float jumpPower = 10f;
    public Animator animator;
    public Animator platformanimation;
    public Rigidbody2D rb;
    public GameObject manager;
    public EdgeCollider2D edgeCollider;
    public Vector2 beforelocation; //original position of the platform

    public void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("doodlemanager");
        platformanimation = this.GetComponent<Animator>();
        beforelocation = this.GetComponent<Transform>().transform.position;
        edgeCollider = this.GetComponent<EdgeCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision) //handles collsion between player and the different platforms
    {
        if(collision.relativeVelocity.y <= 0f)
        {
            animator = collision.gameObject.GetComponent<Animator>();
            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if(rb != null )
            {
                
                animator.SetTrigger("Jump");
                Vector2 velocity = rb.velocity;
                velocity.y = jumpPower; //changes player's rigibody upward velocity to jump power
                rb.velocity = velocity;
                StartCoroutine(bounce());
                if (this.gameObject.tag == "doodlewin") //when the player collides with the winning platform / last platform
                {
                    manager.GetComponent<Manager>().player.SetActive(false);
                    manager.GetComponent<Manager>().win.SetActive(true);
                    manager.GetComponent<AudioSource>().Stop();
                    manager.GetComponent <Manager>().DisplayStats();
                }
                else if(this.gameObject.tag == "breakplatform") //if player collides with breakable platform
                {
                    platformanimation.SetTrigger("break");
                    StartCoroutine(breaking());
                }
            }
        }
    }

    IEnumerator breaking()//coroutine for breakable platform
    {
        manager.GetComponent<Manager>().PlayBreakPlatform();
        this.edgeCollider.enabled = false; //disables collider
        yield return new WaitForSeconds(0.9f);
        Destroy(this.gameObject);
    }

    IEnumerator bounce() //coroutine for normal platform and jump boost platform
    {
        if (this.gameObject.tag != "breakplatform")
        {
            if (this.gameObject.tag == "bounceplatform") //when player collides with jump boost platform
            {
                manager.GetComponent<Manager>().PlayExtraJump();
                this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 0.5f); //move the platform down to create an illusion of platform reaction to the player's bounce
                yield return new WaitForSeconds(0.1f);
                this.transform.position = beforelocation; //return to original position
            }
            else //collision with normal platform
            {
                manager.GetComponent<Manager>().PlayeBounce();
                this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 0.1f);
                yield return new WaitForSeconds(0.1f);
                this.transform.position = beforelocation;
            }
        }
        

    }
}
