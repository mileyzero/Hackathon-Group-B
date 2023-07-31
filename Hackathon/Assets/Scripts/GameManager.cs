using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //Reference script to holidayManager
    public Holiday holidayManager;

    //Reference script to investManager
    public Investment investManager;

    //Reference script to insuranceManager;
    public Insurance insuranceManager;

    //Variable for NameGenerator
    public NameGenerator nameManager;

    //GameObject for investment and employee notification
    public GameObject investmentNotiIcon;
    public GameObject employeeNotiIcon;
    public GameObject insuranceNotiIcon;

    //GameObject for investment and employee envelope icon
    public GameObject investmentButton;
    public GameObject employeeButton;
    public GameObject insuranceButton;

    public GameObject background1;
    public GameObject background2;
    public GameObject background3;

    public GameObject _mainGame;
    //Sliders for happiness, money and popularity
    public Slider happinessSlider;
    public Slider moneySlider;
    public Slider popularitySlider;

    //Float for maxHappiness, money and popularity
    public float maxHappiness = 90f;
    public float maxMoney = 90f;
    public float maxPopularity = 90f;

    //Float for happiness, money and popularity
    public float happiness;
    public float money;
    public float popularity;

    //Float for original and delay timers
    public float originalTimer;
    public float delayTimer;

    //Float for timer
    public float timer;

    //Int for randomInitialize
    public int randomInitialize;

    //Bool for isRunning
    public bool isRunning;

    private void Awake()
    {
        _mainGame = GameObject.FindGameObjectWithTag("main_game");

        if (_mainGame != null)
        {
            DontDestroyOnLoad(_mainGame);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        background2.SetActive(false);
        background3.SetActive(false);

        //Randomize timer
        delayTimer = Random.Range(1f, 3f);
        Debug.Log(delayTimer);

        //set timer to delayTimer
        timer = delayTimer;

        //set isRunning to false on start
        isRunning = false;

        //set a random value on start for happiness, money and a fixed value for popularity
        happinessSlider.value = Random.Range(20f, 55f);
        moneySlider.value = Random.Range(30f, 65f);
        popularitySlider.value = 20f;

        happiness = happinessSlider.value;
        money = moneySlider.value;
        popularity = popularitySlider.value;

        //set investment, employee notification icons to false
        investmentNotiIcon.SetActive(false);
        employeeNotiIcon.SetActive(false);
        insuranceNotiIcon.SetActive(false);

        //set investment, employee email button to false
        investmentButton.SetActive(false);
        employeeButton.SetActive(false);
        insuranceButton.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isRunning);

        //if isRunning equals to false, the randomInitialize will set a random.range to it, then the timer will countdown
        //if the timer hits less or equals to 0, isRunning will set to true to prevent the timer from continuing
        //the randomInitalize number will then choose between 0 to 2, if its on 0, it will enable the email button for investment.
        //and if it lands on 1, it will enable the email button for employees.
        if (isRunning == false)
        {
            randomInitialize = Random.Range(0, 3);
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                isRunning = true;

                if (randomInitialize == 0)
                {
                    investManager.scenarioButton.enabled = true;

                    investmentNotiIcon.SetActive(true);
                    investmentButton.SetActive(true);
                }

                else if (randomInitialize == 1)
                {
                    holidayManager.scenarioButton.enabled = true;

                    employeeButton.SetActive(true);
                    employeeNotiIcon.SetActive(true);
                }

                else if (randomInitialize == 2)
                {
                    insuranceManager.scenarioButton.enabled = true;

                    insuranceButton.SetActive(true);
                    insuranceNotiIcon.SetActive(true);
                }

                Debug.Log(randomInitialize);
            }
        }

        LevelChange();
    }

    //This function returns the value of its original value when its being called.
    public void FunctionUpdates()
    {
        timer = delayTimer;
        isRunning = false;
    }

    //when InitializeHoliday is called, it will set the email button and notification icon to false to prevent players from spamming it.
    //which then it will grab from the holidayManager and nameManager methods
    public void InitializeHoliday()
    {
        Debug.Log("Holiday");

        employeeButton.SetActive(false);
        employeeNotiIcon.SetActive(false);

        holidayManager.GetComponent<Holiday>().SpawnScenario();
        nameManager.GetComponent<NameGenerator>().NameRandomList();
    }

    //when InitializeInvest is called, it will set the email button and notification icon to false to prevent players from spamming it.
    //which then it will grab from the holidayManager and nameManager methods
    public void InitializeInvest()
    {
        Debug.Log("Investment");

        investmentNotiIcon.SetActive(false);
        investmentButton.SetActive(false);

        investManager.GetComponent<Investment>().SpawnScenario();
        nameManager.GetComponent<NameGenerator>().NameRandomList();
    }

    public void InitializeInsurance()
    {
        Debug.Log("Insurance");

        insuranceNotiIcon.SetActive(false);
        insuranceButton.SetActive(false);

        insuranceManager.GetComponent<Insurance>().SpawnScenario();
        nameManager.GetComponent<NameGenerator>().NameRandomList();
    }

    public void LevelChange()
    {
        if(popularity < 30)
        {
            Debug.Log("Level 1");
            background1.SetActive(true);
        }

        else if(popularity >= 30)
        {
            if (popularity >= 60)
            {
                Debug.Log("Level 3");
                background2.SetActive(false);
                background3.SetActive(true);
            }
            else
            {
                Debug.Log("Level 2");
                background1.SetActive(false);
                background2.SetActive(true);
            }
        }
    }

    public void SnakeGame()
    {
        _mainGame.SetActive(false);
        SceneManager.LoadScene("Snake");
    }
}
