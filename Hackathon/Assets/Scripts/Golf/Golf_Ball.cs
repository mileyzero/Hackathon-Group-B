using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Golf_Ball : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject hole;

    private bool isDragging;
    private bool score;
    public Vector2 ball_velocity;
    private Animator animator;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TilemapCollider2D sand;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private float power = 2f;
    [SerializeField] private float drag = 0.9f;
    [SerializeField] private float maxGoalSpeed = 4f;
    [SerializeField] private TextMeshProUGUI movesMade_txt;
    [SerializeField] private int movesMade = 0;

    void Start()
    {
        hole = GameObject.FindGameObjectWithTag("goal");
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movesMade_txt.text = "Moves Made: "+movesMade.ToString();
        if (rb.velocity.magnitude <= 0.2f && score ==false )
        {
            rb.drag = drag;
            Player_Input();
        }
        
    }
    float angle;
    private void Player_Input() //Handles the player's input to move the ball
    {
        Vector2 input = Camera.main.ScreenToWorldPoint(Input.mousePosition); //mouse position in the game
        float distance = Vector2.Distance(transform.position, input); //calculate distance between ball and mouse
        Vector2 direction = input - (Vector2)transform.position; //calcuate the direction from ball to mouse
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //calculating the angle of rotation to face towards the mouse

        if (Input.GetMouseButtonDown(0) && distance <= 0.5f) //when player drags from the ball
        {
            DragStart();
        }

        if(Input.GetMouseButton(0) && isDragging) 
        {
            DragChange(input);
        }

        if(Input.GetMouseButtonUp(0) && isDragging) 
        {
            DragRelease(input);
        }
    }

    private void DragStart()
    {
        isDragging = true;
        lineRenderer.positionCount = 2;
    }

    private void DragChange(Vector2 pos) //draws the line between ball and mouse in the opposite direction to act like a guide
    {
        Vector2 dir = (Vector2)transform.position - pos;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, (Vector2)transform.position + Vector2.ClampMagnitude((dir * power) / 2, maxPower / 2));
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90f));
        float distance = Vector2.Distance((Vector2)transform.position, pos);
        if(distance > 4.5f)
        {
            lineRenderer.startColor = Color.red;
        }
        else
        {
            lineRenderer.startColor = Color.white;
        }
    }
    
    private void DragRelease(Vector2 pos) //calculates distance between mouse and ball then adds velocity to the ball in the opposite direction of the drag
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90f));
        animator.SetTrigger("isHit");
        movesMade += 1;
        float distance = Vector2.Distance((Vector2)transform.position, pos);
        isDragging = false;
        lineRenderer.positionCount = 0;
        if(distance <0.5f) //cancels if the drag is too short
        {
            return;
        }
        else if(distance > 4.5f)
        {
            StartCoroutine(ballfly());
        }
        Vector2 dir = (Vector2)transform.position - pos;
        rb.velocity = Vector2.ClampMagnitude(dir * power, maxPower);//calculates velocity to add to ball and limits the velcoity to maxPower
    }

    private void CheckWinState() //check if the player scored the ball
    {
        if (score) return;

        if(rb.velocity.magnitude <= maxGoalSpeed)
        {
            score = true;
            StartCoroutine(goalAnimation()); //ball goes in hole
        }
        else
        {
            StartCoroutine(ballTooFast());
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //check for collsion with hole
    {
        if (collision.tag == "goal") CheckWinState();
        else if (collision.tag == "golf_sand")
        {
            rb.drag += 2f;
        }
    }


    private void OnTriggerStay2D(Collider2D collision) //checks when the ball is staying in the hole
    {
        if (collision.tag == "goal") CheckWinState();
    }

    IEnumerator goalAnimation() //Coroutine for when ball goes in hole
     {
        rb.velocity = Vector2.zero;
        gameObject.transform.position = hole.transform.position;
        //loops 10 times
        for(int i = 0; i < 15; i++) //reduces scale by 0.1f every loop and wait for 0.1 seconds
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x - 0.01f, gameObject.transform.localScale.y - 0.01f);
            yield return new WaitForSeconds(0.05f);
        }    
        gameObject.SetActive(false);
    }

    IEnumerator ballTooFast() //Coroutine for when ball goes in hole
    {

        gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x - 0.04f, gameObject.transform.localScale.y - 0.04f);
        rb.drag += 2.5f;
        yield return new WaitForSeconds(0.2f);
        gameObject.transform.localScale = new Vector2 (0.3f, 0.3f);
    }

    IEnumerator ballfly()
    {
        sand.enabled = false;
        hole.GetComponent<CircleCollider2D>().enabled = false;
        for (int i = 0; i<10; i++)
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x + 0.05f, gameObject.transform.localScale.y + 0.05f);
            yield return new WaitForSeconds(0.05f);
        }

        for (int i = 0; i < 10; i++)
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x - 0.05f, gameObject.transform.localScale.y - 0.05f);
            yield return new WaitForSeconds(0.05f);
        }

        sand.enabled = true;
        hole.GetComponent<CircleCollider2D>().enabled = true;
        for (float i = 0.15f; i >= 0.1;i-=0.05f)
        {
            Debug.Log(i);
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x + i, gameObject.transform.localScale.y + i);
            yield return new WaitForSeconds(0.1f);
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x - i, gameObject.transform.localScale.y - i);
        }
        
    }
}
