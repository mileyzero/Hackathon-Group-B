using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CarController : MonoBehaviour
{
    private Car_Collect stats;

    public MiniGameTimer gmTime;

    public float carSpeed;
    Vector3 carPosition;
    public float maxPos;

    public GameObject win;
    public GameObject lose_scn;

    public GameObject pauseBtn;

    public GameObject spawner;
    public GameObject timer_bar;
    public GameObject panel;
    public GameObject guide;

    public AudioSource audioPlayer;
    public AudioClip lose_sfx;
    public AudioClip win_sfx;

    public Slider carslider;
    public float timer;
    public float currentTime;
    public bool timerrunning = false;
    public bool lose = false;
    public bool Win = false;

    public TextMeshProUGUI moneytext;
    public TextMeshProUGUI populartext;
    public TextMeshProUGUI happytext;

    void Start()
    {
        carPosition = transform.position;
        carslider.value = 0;
        timer = Random.Range(timer - 5f, timer);
        lose= false;
        Win = false;
        panel.SetActive(true);
        guide.SetActive(true);
        spawner.SetActive(false);
        timer_bar.SetActive(false);
        stats = GetComponent<Car_Collect>();
    }

    //this function will act as click to start for the game
    public void StartGame()
    {
        panel.SetActive(false);
        guide.SetActive(false);
        spawner.SetActive(true);
        timer_bar.SetActive(true);
        StartTimer();
    }

    //this will start timer for the game
    public void StartTimer()
    {
        currentTime = timer;
        timerrunning = true;
    }
    // Update is called once per frame
    void Update()
    {
        carPosition.x += Input.GetAxis("Horizontal") * carSpeed * Time.deltaTime;
        carPosition.x = Mathf.Clamp(carPosition.x, -maxPos, maxPos);
        transform.position = carPosition;

        //if timer is still running then the slider bar will increase
        if (timerrunning)
        {
            carslider.value = 1f - (timer / currentTime);
            timer -= Time.deltaTime;
            if (timer <= 0f) //if timer is finished and the player did not lose then they win
            {
                if (lose == false)
                {
                    timerrunning = false;
                    Win = true;
                    StartCoroutine(showstats());

                    gmTime.StartCooldown();
                    StartCoroutine(TransitionToMain(1.5f));
                }
            }

            if (lose == true && Win == false) //setting the lose screen
            {
                gmTime.StartCooldown();
                StartCoroutine(TransitionToMain(1.5f));
            }
        }
    }

    public void PlayerLose() //sfx when the player lose
    {
        audioPlayer.clip = lose_sfx;
        audioPlayer.loop = false;
        audioPlayer.pitch = 0.4f;
        audioPlayer.Play();
    }

    public void PlayerWin() //sfx when the player wins
    {
        audioPlayer.clip = win_sfx;
        audioPlayer.loop =false;
        audioPlayer.Play();
    }

    IEnumerator showstats() //coroutine to show the number for the stats after player wins
    {
        //animation to change the number after you win   
        for (int money = 0; money <= stats.moneyCollected; money++) //for loop will loop through money collected and show from 0 to the actual amount
        {
            if (moneytext.text == "+" + stats.moneyCollected.ToString())
            {
                break;
            }
            else
            {
                moneytext.text = "+" + money.ToString();
                yield return new WaitForSeconds(0.2f); //delay between number change
            }

        }

        for (int popularity = 0; popularity <= stats.popularCollected; popularity++)
        {
            if (populartext.text == "+" + stats.popularCollected.ToString())
            {
                break;
            }
            else
            {
                populartext.text = "+" + popularity.ToString();
                yield return new WaitForSeconds(0.2f);
            }

        }

        for (int happy = 0; happy <= stats.happyCollected; happy++)
        {
            if (happytext.text == "+" + stats.happyCollected.ToString())
            {
                break;
            }
            else
            {
                happytext.text = "+" + happy.ToString();
                yield return new WaitForSeconds(0.2f);
            }

        }

        //store the values into store game so it can be added to the main game
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money += stats.moneyCollected + 4;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.popularity += stats.popularCollected + 4;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.happiness += stats.happyCollected + 4;
    }

    IEnumerator TransitionToMain(float timer)
    {
        //transition to main game
        if (lose)
        {
            pauseBtn.SetActive(false);
            lose_scn.SetActive(true);
            yield return new WaitForSeconds(timer);
            //if player has accident insurance, it will set it to false. If user does not have insurance then reduce money
            if (GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().accidentinsurance.accidentInsurance != true)
            {
                GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money -= 15f;
            }
            else
            {
                GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().accidentinsurance.accidentInsurance = false;
            }

            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>()._maingame.gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("cooldown").GetComponent<MiniGameTimer>().StartCooldown();

            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.hasPlayedLevel1 = false;
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.hasPlayedLevel2 = false;
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.hasPlayedLevel3 = false;
        }
        else if (Win)
        {
            pauseBtn.SetActive(false);
            spawner.SetActive(false);
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            PlayerWin();
            win.SetActive(true);
            yield return new WaitForSeconds(3.5f);
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>()._maingame.gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("cooldown").GetComponent<MiniGameTimer>().StartCooldown();

            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.hasPlayedLevel1 = false;
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.hasPlayedLevel2 = false;
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.hasPlayedLevel3 = false;


        }
        SceneManager.LoadScene("SampleScene");
    }
}
