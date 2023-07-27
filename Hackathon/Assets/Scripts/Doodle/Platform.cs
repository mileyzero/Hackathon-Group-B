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
    public Vector2 beforelocation;

    public void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("doodlemanager");
        platformanimation = this.GetComponent<Animator>();
        beforelocation = this.GetComponent<Transform>().transform.position;
        edgeCollider = this.GetComponent<EdgeCollider2D>();
    }
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
                StartCoroutine(bounce());
                if (this.gameObject == manager.GetComponent<Manager>().finalplatform)
                {
                   manager.GetComponent<Manager>().win.SetActive(true);
                }
                else if(this.gameObject.tag == "breakplatform")
                {
                    platformanimation.SetTrigger("break");
                    StartCoroutine(breaking());
                }
            }
        }
    }

    IEnumerator breaking()
    {
        
        this.edgeCollider.enabled = false;
        yield return new WaitForSeconds(0.9f);
        Destroy(this.gameObject);
    }

    IEnumerator bounce()
    {
        if (this.gameObject.tag != "breakplatform")
        {
            if (this.gameObject.tag == "bounceplatform")
            {
                this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 0.5f);
                yield return new WaitForSeconds(0.1f);
                this.transform.position = beforelocation;
            }
            else
            {
                this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 0.1f);
                yield return new WaitForSeconds(0.1f);
                this.transform.position = beforelocation;
            }
        }
        

    }
}
