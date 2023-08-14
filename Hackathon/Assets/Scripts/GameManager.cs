using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Reference script to tipManager
    public Tips tipManagerMoney;
    public Tips tipManagerHappiness;
    public Tips tipManagerPopularity;

    //Reference script to holidayManager
    public Holiday holidayManager;

    //Reference script to investManager
    public Investment investManager;

    //Reference script to insuranceManager;
    public Insurance insuranceManager;

    //Reference script to healthManager
    public Health healthManager;

    //Reference script to accidentManager
    public Accident accidentManager;

    //Reference script to MGManager
    public MG_Dialogue MGManager;

    //Reference script to MGCar
    public MG_Car_Dialogue MGCar;

    //Reference script to MGGolf
    public MG_Golf_Dialogue MGGolf;

    //Reference script to AccidentManager
    public InsuranceAccidentManager AccidentManager;

    //Reference script to InvestmentManager
    public InsuranceInvestmentManager InvestmentManager;

    //Variable for NameGenerator
    public NameGenerator nameManager;

    //GameObject for investment and employee notification
    public GameObject investmentNotiIcon;
    public GameObject employeeNotiIcon;
    public GameObject insuranceNotiIcon;
    public GameObject healthNotiIcon;
    public GameObject accidentNotiIcon;

    public GameObject miniGameNotiIcon;
    public GameObject miniGameCarNotiIcon;
    public GameObject miniGameGolfNotiIcon;

    public GameObject insuranceInvestmentNotiIcon;
    public GameObject insuranceAccidentNotiIcon;

    //GameObject for investment and employee envelope icon
    public GameObject investmentButton;
    public GameObject employeeButton;
    public GameObject insuranceHealthButton;
    public GameObject accidentButton;
    public GameObject healthButton;

    public GameObject miniGameButton;
    public GameObject miniGameCarButton;
    public GameObject miniGameGolfButton;

    public GameObject insuranceInvestmentButton;
    public GameObject insuranceAccidentButton;
    
    public GameObject background1;
    public GameObject background2;
    public GameObject background3;

    public GameObject loseMoney;
    public GameObject losePopularity;
    public GameObject loseHappiness;

    public GameObject loseMoneyTipPanel;
    public GameObject losePopularityTipPanel;
    public GameObject loseHappinessTipPanel;

    //Button for snekGame
    public Button snekGameButton;
    public Button slotGameButton;

    //Sliders for happiness, money and popularity
    public Slider happinessSlider;
    public Slider moneySlider;
    public Slider popularitySlider;

    //AudioSource for btnClick
    public AudioSource btnClick;

    //Float for maxHappiness, money and popularity
    public int maxHappiness = 90;
    public int maxMoney = 90;
    public int maxPopularity = 90;

    //Float for happiness, money and popularity
    public float happiness;
    public float money;
    public float popularity;

    //Float for original and delay timers
    public float originalTimer;
    public float delayTimer;

    //Float for timer, loseTimeTipDuration
    public float timer;
    public float loseTimeTipDuration;

    //Int for randomInitialize
    public int randomInitialize;

    //Bool for isRunning
    public bool isRunning;

    //Bool for activating lose tips
    private bool hasActivatedLoseHappinessTip = false;
    private bool hasActivatedLosePopularityTip = false;
    private bool hasActivatedLoseMoneyTip = false;

    // Start is called before the first frame update
    void Start()
    {
        btnClick = GetComponent<AudioSource>();

        //Enable Snek button for players to play
        snekGameButton.enabled = true;
        slotGameButton.enabled = true;

        //Set background 2 and 3 to false as it has not reached it's current threshold
        background2.SetActive(false);
        background3.SetActive(false);

        loseHappiness.SetActive(false);
        loseMoney.SetActive(false);
        losePopularity.SetActive(false);

        loseHappinessTipPanel.SetActive(false);
        loseMoneyTipPanel.SetActive(false);
        losePopularityTipPanel.SetActive(false);

        //Randomize timer
        delayTimer = Random.Range(1f, 3f);
        Debug.Log(delayTimer);

        //set timer to delayTimer
        timer = delayTimer;
        loseTimeTipDuration = 2f;

        //set isRunning to false on start
        isRunning = false;

        //set a random value on start for happiness, money and a fixed value for popularity
        happinessSlider.value = Random.Range(20f, 55f);
        moneySlider.value = Random.Range(30f, 65f);
        popularitySlider.value = Random.Range(10f, 20f);

        happiness = happinessSlider.value;
        money = moneySlider.value;
        popularity = popularitySlider.value;

        //set investment, employee notification icons to false
        investmentNotiIcon.SetActive(false);
        employeeNotiIcon.SetActive(false);
        insuranceNotiIcon.SetActive(false);

        miniGameNotiIcon.SetActive(false);
        miniGameCarNotiIcon.SetActive(false);
        miniGameGolfNotiIcon.SetActive(false);

        insuranceAccidentNotiIcon.SetActive(false);
        insuranceInvestmentNotiIcon.SetActive(false);

        //set investment, employee email button to false
        investmentButton.SetActive(false);
        employeeButton.SetActive(false);
        insuranceHealthButton.SetActive(false);

        insuranceInvestmentButton.SetActive(false);
        insuranceAccidentButton.SetActive(false);

        miniGameButton.SetActive(false);
        miniGameCarButton.SetActive(false);
        miniGameGolfButton.SetActive(false);
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
            randomInitialize = Random.Range(0, 10);
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

                    insuranceHealthButton.SetActive(true);
                    insuranceNotiIcon.SetActive(true);
                }
                else if(randomInitialize == 3)
                {
                    healthManager.scenarioButton.enabled = true;

                    healthButton.SetActive(true);
                    healthNotiIcon.SetActive(true);
                }
                else if(randomInitialize == 4)
                {
                    accidentManager.scenarioButton.enabled = true;

                    accidentButton.SetActive(true);
                    accidentNotiIcon.SetActive(true);
                }
                else if(randomInitialize == 5)
                {
                    MGManager.scenarioButton.enabled = true;

                    miniGameButton.SetActive(true);
                    miniGameNotiIcon.SetActive(true);
                }
                else if(randomInitialize == 6)
                {
                    MGCar.scenarioButton.enabled = true;

                    miniGameCarButton.SetActive(true);
                    miniGameCarNotiIcon.SetActive(true);
                }
                else if(randomInitialize == 7)
                {
                    MGGolf.scenarioButton.enabled = true;

                    miniGameGolfButton.SetActive(true);
                    miniGameGolfNotiIcon.SetActive(true);
                }
                else if(randomInitialize == 8)
                {
                    AccidentManager.scenarioButton.enabled = true;

                    insuranceAccidentButton.SetActive(true);
                    insuranceAccidentNotiIcon.SetActive(true);
                }
                else if(randomInitialize == 9)
                {
                    InvestmentManager.scenarioButton.enabled = true;

                    insuranceInvestmentButton.SetActive(true);
                    insuranceInvestmentNotiIcon.SetActive(true);
                }

                Debug.Log(randomInitialize);
            }
        }

        LevelChange();
        MaxMoneyEarned();
        MaxHappinessEarned();
        MaxPopularityEarned();

        LoseMoneyCondition();
        LoseHappinessCondition();
        LosePopularityCondition();

        WinGame();
    }

    public void WinGame()
    {
        if(popularity >= maxPopularity && money >= maxMoney && happiness >= maxHappiness)
        {
            SceneManager.LoadScene("WinGame");
        }
    }

    public void PlayBtnSound()
    {
        btnClick.Play();
    }

    public void MaxPopularityEarned()
    {
        if(popularity > maxPopularity)
        {
            popularity = maxPopularity;
        }
    }

    public void MaxMoneyEarned()
    {
        if(money > maxMoney)
        {
            money = maxMoney;
        }
    }

    public void MaxHappinessEarned()
    {
        if(happiness > maxHappiness)
        {
            happiness = maxHappiness;
        }
    }

    //This function returns the value of its original value when its being called.
    public void FunctionUpdates()
    {
        timer = delayTimer;
        isRunning = false;
    }

    public void InitializeInvestmentInsurance()
    {
        Debug.Log("Investment Insurance Game");

        insuranceInvestmentButton.SetActive(false);
        insuranceInvestmentNotiIcon.SetActive(false);

        InvestmentManager.GetComponent<InsuranceInvestmentManager>().SpawnScenario();
        nameManager.GetComponent<NameGenerator>().NameRandomList();

        snekGameButton.enabled = false;
        slotGameButton.enabled = false;
    }

    public void InitializeAccidentInsurance()
    {
        Debug.Log("Accident Insurance Game");

        insuranceAccidentButton.SetActive(false);
        insuranceAccidentNotiIcon.SetActive(false);

        AccidentManager.GetComponent<InsuranceAccidentManager>().SpawnScenario();
        nameManager.GetComponent<NameGenerator>().NameRandomList();

        snekGameButton.enabled = false;
        slotGameButton.enabled = false;
    }

    //when InitializeGolfGame is called, it will set the email button and notification icon to false to prevent players from spamming it.
    //which then it will grab from the MG_Golf_Dialogue and nameManager methods
    public void InitializeGolfGame()
    {
        Debug.Log("Golf Game");

        miniGameGolfButton.SetActive(false);
        miniGameGolfNotiIcon.SetActive(false);

        MGGolf.GetComponent<MG_Golf_Dialogue>().SpawnScenario();
        nameManager.GetComponent<NameGenerator>().NameRandomList();

        snekGameButton.enabled = false;
        slotGameButton.enabled = false;
    }

    //when InitializeCarGame is called, it will set the email button and notification icon to false to prevent players from spamming it.
    //which then it will grab from the MG_Car_Dialogue and nameManager methods
    public void InitializeCarGame()
    {
        Debug.Log("Car Game");

        miniGameCarButton.SetActive(false);
        miniGameCarNotiIcon.SetActive(false);

        MGCar.GetComponent<MG_Car_Dialogue>().SpawnScenario();
        nameManager.GetComponent<NameGenerator>().NameRandomList();

        snekGameButton.enabled = false;
        slotGameButton.enabled = false;
    }

    //when InitializeMiniGame is called, it will set the email button and notification icon to false to prevent players from spamming it.
    //which then it will grab from the MGManager and nameManager methods
    public void InitializeMiniGame()
    {
        Debug.Log("Doodle Mini-Game");

        miniGameButton.SetActive(false);
        miniGameNotiIcon.SetActive(false);

        MGManager.GetComponent<MG_Dialogue>().SpawnScenario();
        nameManager.GetComponent<NameGenerator>().NameRandomList();

        snekGameButton.enabled = false;
        slotGameButton.enabled = false;
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

        snekGameButton.enabled = false;
        slotGameButton.enabled = false;
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

        snekGameButton.enabled = false;
        slotGameButton.enabled = false;
    }

    //when InitializeInsurance is called, it will set the email button and notification icon to false to prevent players from spamming it.
    //which then it will grab from the holidayManager and nameManager methods
    public void InitializeInsurance()
    {
        Debug.Log("Insurance");

        insuranceNotiIcon.SetActive(false);
        insuranceHealthButton.SetActive(false);

        insuranceManager.GetComponent<Insurance>().SpawnScenario();
        nameManager.GetComponent<NameGenerator>().NameRandomList();

        snekGameButton.enabled = false;
        slotGameButton.enabled = false;
    }

    //when InitalizeAccident is called, it will set the email button and notification icon to false to prevent players from spamming it.
    //which then it will grab from the holidayManager and nameManager methods
    public void InitalizeAccident()
    {
        Debug.Log("Accident");

        accidentNotiIcon.SetActive(false);
        accidentButton.SetActive(false);

        accidentManager.GetComponent<Accident>().SpawnScenario();
        nameManager.GetComponent<NameGenerator>().NameRandomList();

        snekGameButton.enabled = false;
        slotGameButton.enabled = false;
    }

    //when InitalizeHealth is called, it will set the email button and notification icon to false to prevent players from spamming it.
    //which then it will grab from the holidayManager and nameManager methods
    public void InitalizeHealth()
    {
        Debug.Log("Health");

        healthNotiIcon.SetActive(false);
        healthButton.SetActive(false);

        healthManager.GetComponent<Health>().SpawnScenario();
        nameManager.GetComponent<NameGenerator>().NameRandomList();

        snekGameButton.enabled = false;
        slotGameButton.enabled = false;
    }

    //LevelChange checks based on the popularity that the player has gathered.
    //if popularity is less than 30 then it stays at level 1 background.
    //else if the popularity is more or equals to 30, it goes to level two and if it's more or equals to 60 then it goes to level 3
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

    //SnakeGame loads the snake game and finds the main game by tag and hides it
    public void SnakeGame()
    {
        SceneManager.LoadScene("Snake");
        GameObject.FindGameObjectWithTag("main_game").SetActive(false);
    }

    public void SlotMachine()
    {
        SceneManager.LoadScene("SlotMachine");
        GameObject.FindGameObjectWithTag("main_game").SetActive(false);
    }

    public void LoseMoneyCondition()
    {
        if(money <= 0)
        {
            loseMoney.SetActive(true);

            if (!hasActivatedLoseMoneyTip)
            {
                StartCoroutine(ActivateLoseMoneyTipWithDelay());
                hasActivatedLoseMoneyTip = true;
            }
        }
    }

    private IEnumerator ActivateLoseMoneyTipWithDelay()
    {
        yield return new WaitForSeconds(loseTimeTipDuration);

        tipManagerMoney.CallTipMoney();

        loseMoneyTipPanel.SetActive(true);
    }

    public void LosePopularityCondition()
    {
        if(popularity <= 0)
        {
            losePopularity.SetActive(true);

            if (!hasActivatedLosePopularityTip)
            {
                StartCoroutine(ActivateLosePopularityTipWithDelay());
                hasActivatedLosePopularityTip = true;
            }
        }
    }

    private IEnumerator ActivateLosePopularityTipWithDelay()
    {
        yield return new WaitForSeconds(loseTimeTipDuration);

        tipManagerPopularity.CallTipPopularity();

        losePopularityTipPanel.SetActive(true);
    }

    public void LoseHappinessCondition()
    {
        if(happiness <= 0)
        {
            loseHappiness.SetActive(true);

            if (!hasActivatedLoseHappinessTip)
            {
                StartCoroutine(ActivateLoseHappinessTipWithDelay());
                hasActivatedLoseHappinessTip = true;
            }
        }
    }

    private IEnumerator ActivateLoseHappinessTipWithDelay()
    {
        yield return new WaitForSeconds(loseTimeTipDuration);

        tipManagerHappiness.CallTipHappiness();

        loseHappinessTipPanel.SetActive(true);
    }
}
