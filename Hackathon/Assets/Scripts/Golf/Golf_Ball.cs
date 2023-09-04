using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;


public class Golf_Ball : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject hole;
    public Camera maincamera;

    private bool isDragging;
    
    public Vector2 ball_velocity;
    private Animator animator;
    [SerializeField] private Golf_Manager golf_manager;
    [SerializeField] private bool score;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TilemapCollider2D sand;
    [SerializeField] private TilemapCollider2D ice;
    [SerializeField] private TilemapCollider2D log;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float maxPower = 10f;
    [SerializeField] private float power = 2f;
    [SerializeField] private float drag = 0.9f;
    [SerializeField] private float maxGoalSpeed = 4f;
    [SerializeField] private TextMeshProUGUI movesMade_txt;
    [SerializeField] private int movesMade;

    private bool ball_released;
    private bool cancel;

    public GameObject pauseBtn;

    public GameObject guide;

    public GameObject win_screen;
    public GameObject lose_screen;

    void Start()
    {
        hole = GameObject.FindGameObjectWithTag("goal");
        animator = gameObject.GetComponent<Animator>();
        //movesMade = 10;
        movesMade_txt.text = "Moves Left: " + movesMade.ToString();
        ball_released = false;
        cancel = false;
        win_screen.SetActive(false);
        lose_screen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(ball_released && rb.velocity.magnitude <= 0.2f && cancel ==false) //the moves made will reduce by 1 after the player released the ball and the ball has come to a stop
        {
            if(score == false)
            {
                movesMade--;
            }
            
            if (movesMade == 0)
            {
                movesMade_txt.text = "Moves Left: 0";
            }
            else
            {
                movesMade_txt.text = "Moves Left: " + movesMade.ToString();
            }
            Lose(); //check if the player lose
        }


        if (rb.velocity.magnitude <= 0.2f && score ==false )
        {
            ball_released = false;
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
            if (SceneManager.GetActiveScene().name == "Golf Level 1")
            {
                guide.SetActive(false);
            }
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
        if(distance > 3.5 )
        {
            if(distance > 7)
            {
                maincamera.orthographicSize = 7;
            }
            else
            {
                maincamera.orthographicSize = distance;
            }

        }

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
        maincamera.orthographicSize = 3.5f;
        ball_released = true;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90f));
        animator.SetTrigger("isHit");
        float distance = Vector2.Distance((Vector2)transform.position, pos);
        isDragging = false;
        lineRenderer.positionCount = 0;
        if(distance <0.5f) //cancels if the drag is too short
        {
            cancel = true;
            return;
        }
        else if(distance > 4.5f)
        {
            StartCoroutine(ballfly());
        }
        else
        {
            golf_manager.PlayeLightTap();
        }
        
        cancel = false;
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
        
        if (collision.tag == "goal")
        { CheckWinState(); }

        else if (collision.CompareTag("golf_sand"))
        {
            rb.drag = 10f;
        }
        else if (collision.CompareTag("ice"))
        {
            rb.drag = 0.02f;
        }

        CheckCollectStats(collision);
    }

    public void CheckCollectStats(Collider2D collision)
    {
        if (collision.tag == "doodleHappy")
        {
            golf_manager.PlayCollect();
            golf_manager.happycollected += 1;
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "doodleMoney")
        {
            golf_manager.PlayCollect();
            golf_manager.moneycollected += 1;
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "doodlePopular")
        {
            golf_manager.PlayCollect();
            golf_manager.popularitycollected += 1;
            Destroy(collision.gameObject);
        }
    }


    private void OnTriggerStay2D(Collider2D collision) //checks when the ball is staying in the hole
    {
        Debug.Log(collision.tag);
        if (collision.tag == "goal")
        { CheckWinState(); }
        else if (collision.CompareTag("golf_sand"))
        {
            rb.drag = 10f;
        }
        else if (collision.CompareTag("ice"))
        {
            rb.drag = 0.02f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        //Debug.Log(collision.tag);
        if(collision.CompareTag("ice")|| collision.CompareTag("golf_sand"))
        {
            rb.drag = drag;
        }
    }

    private void Lose()
    {
        if (movesMade == 0 && score == false)
        {
            StartCoroutine(TransitionToMainGame(1.5f));

            Debug.Log("Lose");
        }
    }

    IEnumerator TransitionToMainGame(float timer)
    {
        lose_screen.SetActive(true);
        pauseBtn.SetActive(false);

        this.GetComponent<CircleCollider2D>().enabled = false;

        yield return new WaitForSeconds(1.5f);

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>()._maingame.gameObject.SetActive(true);

        GameObject.FindGameObjectWithTag("cooldown").GetComponent<MiniGameTimer>().StartCooldown();

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().hasPlayedLevel1 = false;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().hasPlayedLevel2 = false;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().hasPlayedLevel3 = false;

        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator goalAnimation() //Coroutine for when ball goes in hole
     {
        rb.velocity = Vector2.zero;
        gameObject.transform.position = hole.transform.position;
        golf_manager.PlayeInHole();
        //loops 10 times
        for(int i = 0; i < 15; i++) //reduces scale by 0.1f every loop and wait for 0.1 seconds
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x - 0.01f, gameObject.transform.localScale.y - 0.01f);
            yield return new WaitForSeconds(0.05f);
        }
        golf_manager.Win();
        gameObject.SetActive(false);
        win_screen.SetActive(true);
    }


    IEnumerator ballTooFast() //Coroutine for when ball goes in hole, the ball will shrink and the ball will slow down then it will return back to normal size
    {

        gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x - 0.04f, gameObject.transform.localScale.y - 0.04f);
        rb.drag += 2.5f;
        yield return new WaitForSeconds(0.2f);
        gameObject.transform.localScale = new Vector2 (0.3f, 0.3f);
        rb.drag = drag;
    }

    IEnumerator ballfly() //Coroutine to make the illusion that the ball is flying
    {
        //disable the sand the hole's colliders to prevent the ball from interacting with them
        golf_manager.PlayeSwing();
        sand.enabled = false;
        ice.enabled = false;
        log.enabled = false;
        this.GetComponent<CircleCollider2D>().radius = 0.5f/15;
        hole.GetComponent<CircleCollider2D>().enabled = false;
        for (int i = 0; i < 15; i++) //for loop to gradually increase size of ball
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x + 0.1f, gameObject.transform.localScale.y + 0.1f);
            yield return new WaitForSeconds(0.05f);
        }

        for (int i = 0; i < 15; i++) //for loop to gradually decrease size of ball to original size
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x - 0.1f, gameObject.transform.localScale.y - 0.1f);
            yield return new WaitForSeconds(0.05f);
        }
        
        //the sand and the hole's colliders are enabled again so the ball can interact with them
        sand.enabled = true; 
        ice.enabled = true;
        this.GetComponent<CircleCollider2D>().radius = 0.5f;

        hole.GetComponent<CircleCollider2D>().enabled = true;
        for (float i = 0.15f; i >= 0.1;i-=0.05f) //last for loop to increase and decrease the ball's size to create the illusion of ball bouncing
        {
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x + i, gameObject.transform.localScale.y + i);
            yield return new WaitForSeconds(0.1f);
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x - i, gameObject.transform.localScale.y - i);
        }
        log.enabled = true;
    }
}
