using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    public MiniGameTimer gmTime;

    public float carSpeed;
    Vector3 carPosition;
    public float maxPos;

    public GameObject win;
    public GameObject lose_scn;

    public GameObject spawner;
    public GameObject timer_bar;
    public GameObject panel;

    public Slider carslider;
    public float timer;
    public float currentTime;
    public bool timerrunning = false;
    public bool lose = false;
    public bool Win = false;

    void Start()
    {
        carPosition = transform.position;
        carslider.value = 0;
        timer = Random.Range(timer - 5f, timer);
        panel.SetActive(true);
        spawner.SetActive(false);
        timer_bar.SetActive(false);
    }

    //this function will act as click to start for the game
    public void StartGame()
    {
        panel.SetActive(false);
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
            carslider.value = 1f - (timer/currentTime);
            timer -= Time.deltaTime;
            if (timer <= 0f) //if timer is finished and the player did not lose then they win
            {
                if(lose == false)
                {
                    timerrunning = false;
                    Win = true;
                    win.SetActive(true);
                    Debug.Log("Win");
                }
            }
        }

        if(lose == true && Win ==false) //setting the lose screen
        {
            lose_scn.SetActive(true);

            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>()._maingame.gameObject.SetActive(true);
            SceneManager.LoadScene(0);

            gmTime.Timer();
        }
    }
}
