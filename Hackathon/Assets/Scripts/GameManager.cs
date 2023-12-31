using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    //Reference script to DontDestroyGame
    public DontDestroyGame DDG;

    //Reference script to tipManager
    public Tips tipManagerMoney;
    public Tips tipManagerHappiness;
    public Tips tipManagerPopularity;

    public Tips tipManagerWin;

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

    public Animator transitionAnim;

    //GameObject for investment and employee notification
    public GameObject investmentNotiIcon;
    public GameObject employeeNotiIcon;
    public GameObject insuranceNotiIcon;
    public GameObject healthNotiIcon;
    public GameObject accidentNotiIcon;

    public GameObject miniGameNotiIcon;
    public GameObject miniGameCarNotiIcon;
    public GameObject miniGameGolfNotiIcon;

    public GameObject moneyIcon;
    public GameObject popularIcon;
    public GameObject happinessIcon;

    public GameObject moneyBarOutline;
    public GameObject popularBarOutline;
    public GameObject happinessBarOutline;

    public GameObject insuranceInvestmentNotiIcon;
    public GameObject insuranceAccidentNotiIcon;

    public GameObject cooldownBrowser;
    public GameObject cooldownNoti;

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

    public GameObject winGameBG;

    public GameObject loseMoney;
    public GameObject losePopularity;
    public GameObject loseHappiness;

    public GameObject loseMoneyTipPanel;
    public GameObject losePopularityTipPanel;
    public GameObject loseHappinessTipPanel;

    public GameObject winGamePanel;

    public GameObject plusMoney;
    public GameObject plusPopularity;
    public GameObject plusHappiness;

    public GameObject minusMoney;
    public GameObject minusPopularity;
    public GameObject minusHappiness;

    public GameObject pauseMenu;
    public GameObject pauseBtn;

    public GameObject tutorialImg;
    public GameObject tutorialEmail;
    public GameObject tutorialSnek;
    public GameObject tutorialSlot;
    public GameObject tutorialResource;
    public GameObject tutorialBrowser;
    public GameObject tutorialInsurance;
    public GameObject tutorialConfirmation;
    public GameObject tutorialAftConfirmation;
    public GameObject finalTutorial;
    public GameObject investmentTutorial;
    public GameObject highProfileInvestment;
    public GameObject lowProfileInvesment;
    public GameObject objectiveTutorial;

    public GameObject cooldownPanel;

    //Button for snekGame, slotGame and doNut
    public Button snekGameButton;
    public Button slotGameButton;
    public Button doNutButton;

    //Sliders for happiness, money and popularity
    public Slider happinessSlider;
    public Slider moneySlider;
    public Slider popularitySlider;

    //AudioSource for btnClick
    public AudioSource btnClick;

    //Float for maxHappiness, money and popularity
    public float maxHappiness = 90;
    public float maxMoney = 90;
    public float maxPopularity = 90;

    public float transitionTime = 1f;

    //Int for randomInitialize
    public int randomInitialize;

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

    public float winTime;

    //Bool for isRunning
    public bool isRunning;

    public bool isDialogue;

    public float animationDur = 5f;

    public float currentMoney;
    public float currentHappiness;
    public float currentPopularity;

    public float elapsedTime = 0;

    //Bool for activating lose tips
    private bool hasActivatedLoseHappinessTip = false;
    private bool hasActivatedLosePopularityTip = false;
    private bool hasActivatedLoseMoneyTip = false;
    private bool hasActivatedWin = false;

    // Start is called before the first frame update
    void Start()
    {
        isDialogue = false;

        cooldownPanel.SetActive(false);
        pauseBtn.SetActive(true);
        pauseMenu.SetActive(false);

        plusMoney.SetActive(false);
        plusHappiness.SetActive(false);
        plusPopularity.SetActive(false);

        minusMoney.SetActive(false);
        minusHappiness.SetActive(false);
        minusPopularity.SetActive(false);

        cooldownNoti.SetActive(false);
        cooldownBrowser.SetActive(false);

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

        winGameBG.SetActive(false);
        winGamePanel.SetActive(false);

        loseHappinessTipPanel.SetActive(false);
        loseMoneyTipPanel.SetActive(false);
        losePopularityTipPanel.SetActive(false);

        tutorialImg.SetActive(false);
        tutorialEmail.SetActive(false);
        tutorialSnek.SetActive(false);
        tutorialSlot.SetActive(false);
        tutorialResource.SetActive(false);
        tutorialBrowser.SetActive(false);
        tutorialInsurance.SetActive(false);
        tutorialConfirmation.SetActive(false);
        tutorialAftConfirmation.SetActive(false);
        finalTutorial.SetActive(false);
        investmentTutorial.SetActive(false);
        highProfileInvestment.SetActive(false);
        lowProfileInvesment.SetActive(false);
        objectiveTutorial.SetActive(false);

        //Randomize timer
        delayTimer = Random.Range(1f, 3f);
        Debug.Log(delayTimer);

        //set timer to delayTimer
        timer = delayTimer;
        loseTimeTipDuration = 2f;
        winTime = 2f;

        //set isRunning to false on start
        isRunning = false;

        //set a random value on start for happiness, money and a fixed value for popularity
        happinessSlider.value = Random.Range(20f, 40f);
        moneySlider.value = Random.Range(30f, 55f);
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
        Debug.Log(isDialogue);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
            Time.timeScale = 0;
        }

        //if isRunning equals to false, the randomInitialize will set a random.range to it, then the timer will countdown
        //if the timer hits less or equals to 0, isRunning will set to true to prevent the timer from continuing
        //the randomInitalize number will then choose between 0 to 2, if its on 0, it will enable the email button for investment.
        //and if it lands on 1, it will enable the email button for employees.
        if (isRunning == false)
        {
            randomInitialize = Random.Range(1, 56);
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                isRunning = true;

                if (randomInitialize >= 1 && randomInitialize <= 7 )
                {
                    investManager.scenarioButton.enabled = true;

                    investmentNotiIcon.SetActive(true);
                    investmentButton.SetActive(true);
                }
                else if (randomInitialize > 7 && randomInitialize <= 14)
                {
                    holidayManager.scenarioButton.enabled = true;

                    employeeButton.SetActive(true);
                    employeeNotiIcon.SetActive(true);
                }
                else if(randomInitialize > 14 && randomInitialize <= 25)
                {
                    healthManager.scenarioButton.enabled = true;

                    healthButton.SetActive(true);
                    healthNotiIcon.SetActive(true);
                }
                else if(randomInitialize > 25 && randomInitialize <= 29)
                {
                    accidentManager.scenarioButton.enabled = true;

                    accidentButton.SetActive(true);
                    accidentNotiIcon.SetActive(true);
                }
                else if(randomInitialize > 29 && randomInitialize <= 34)
                {
                    MGManager.scenarioButton.enabled = true;

                    miniGameButton.SetActive(true);
                    miniGameNotiIcon.SetActive(true);
                }
                else if(randomInitialize > 34 && randomInitialize <= 39)
                {
                    MGCar.scenarioButton.enabled = true;

                    miniGameCarButton.SetActive(true);
                    miniGameCarNotiIcon.SetActive(true);
                }
                else if(randomInitialize > 39 && randomInitialize <= 44)
                {
                    MGGolf.scenarioButton.enabled = true;

                    miniGameGolfButton.SetActive(true);
                    miniGameGolfNotiIcon.SetActive(true);
                }
                else if (randomInitialize > 44 && randomInitialize <= 48)
                {
                    insuranceManager.scenarioButton.enabled = true;

                    insuranceHealthButton.SetActive(true);
                    insuranceNotiIcon.SetActive(true);
                }
                else if(randomInitialize > 48 && randomInitialize <= 52)
                {
                    AccidentManager.scenarioButton.enabled = true;

                    insuranceAccidentButton.SetActive(true);
                    insuranceAccidentNotiIcon.SetActive(true);
                }
                else if(randomInitialize > 52 && randomInitialize <= 56)
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
            StoreGame.winAchCount += 1;
            GameObject.FindGameObjectWithTag("store_game").GetComponent<AudioSource>().Stop();

            winGameBG.SetActive(true);
            pauseBtn.SetActive(false);

            background1.SetActive(false);
            background2.SetActive(false);
            background3.SetActive(false);

            if (!hasActivatedWin)
            {
                StartCoroutine(ActivateWinGame());
                hasActivatedWin = true;
            }
        }
    }

    private IEnumerator ActivateWinGame()
    {
        yield return new WaitForSeconds(winTime);

        tipManagerWin.CallWinDialogue();

        winGamePanel.SetActive(true);
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
        doNutButton.enabled = false;
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
        doNutButton.enabled = false;
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
        doNutButton.enabled = false;
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
        doNutButton.enabled = false;
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
        doNutButton.enabled = false;
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
        doNutButton.enabled = false;
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
        doNutButton.enabled = false;
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
        doNutButton.enabled = false;
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
        doNutButton.enabled = false;
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
        doNutButton.enabled = false;
    }

    //LevelChange checks based on the popularity that the player has gathered.
    //if popularity is less than 30 then it stays at level 1 background.
    //else if the popularity is more or equals to 30, it goes to level two and if it's more or equals to 60 then it goes to level 3
    public void LevelChange()
    {
        if (popularity < 30)
        {
            Debug.Log("Level 1");
            background1.SetActive(true);
            background2.SetActive(false);
            background3.SetActive(false);
        }
        else if (popularity < 60)
        {
            Debug.Log("Level 2");
            background1.SetActive(false);
            background2.SetActive(true);
            background3.SetActive(false);
        }
        else
        {
            Debug.Log("Level 3");
            background1.SetActive(false);
            background2.SetActive(false);
            background3.SetActive(true);
        }
    }

    //SnakeGame loads the snake game and finds the main game by tag and hides it
    public void SnakeGame()
    {
        StartCoroutine(TransitionSnakeLevel());
    }

    //SlotMachine loads the snake game and finds the main game by tag and hides it
    public void SlotMachine()
    {
        StartCoroutine(TransitionSlotLevel());
    }

    IEnumerator TransitionSnakeLevel()
    {
        transitionAnim.SetTrigger("Start");

        currentHappiness = happiness;
        currentMoney = money;
        currentPopularity = popularity;

        elapsedTime = animationDur;

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("Snake");
        GameObject.FindGameObjectWithTag("main_game").SetActive(false);
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().levelAudioChange.Stop();
    }

    IEnumerator TransitionSlotLevel()
    {
        transitionAnim.SetTrigger("Start");

        currentHappiness = happiness;
        currentMoney = money;
        currentPopularity = popularity;

        elapsedTime = animationDur;

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("SlotMachine");
        GameObject.FindGameObjectWithTag("main_game").SetActive(false);
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().levelAudioChange.Stop();
    }

    //if money is less or equals to 0, it will set an active scene to true. if the boolean is not active yet, it will then start a coroutine
    //after the coroutine, it will then set to true which then the Restart and Quit button will appear
    public void LoseMoneyCondition()
    {
        if(money <= 0)
        {
            StoreGame.loseMoneyAchCount += 01;

            loseMoney.SetActive(true);
            pauseBtn.SetActive(false);

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

    //if popularity is less or equals to 0, it will set an active scene to true. if the boolean is not active yet, it will then start a coroutine
    //after the coroutine, it will then set to true which then the Restart and Quit button will appear
    public void LosePopularityCondition()
    {
        if(popularity <= 0)
        {
            StoreGame.popularityAchCount += 09;

            losePopularity.SetActive(true);
            pauseBtn.SetActive(false);

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

    //if happiness is less or equals to 0, it will set an active scene to true. if the boolean is not active yet, it will then start a coroutine
    //after the coroutine, it will then set to true which then the Restart and Quit button will appear
    public void LoseHappinessCondition()
    {
        if(happiness <= 0)
        {
            StoreGame.loseHappinessAchCount += 1;

            loseHappiness.SetActive(true);
            pauseBtn.SetActive(false);

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

    //when player presses the Pause button, it sets the timeScale to 0 and sets pauseMenu active to true
    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    //when player presses the Resume button, it sets the timeScale to 1 and sets pauseMenu active to true
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    //when player presses the Tutorial button, it sets the timeScale to 1 and pauseMenu to set active false
    //then it sets the tutorial image to true
    public void Tutorial()
    {
        pauseBtn.SetActive(false);
        Time.timeScale = 1;

        Debug.Log(Time.timeScale);

        tutorialImg.SetActive(true);
    }

    //then it sets the tutorial image to false and tutorial email image to true
    public void TutorialNextPage1()
    {
        tutorialImg.SetActive(false);

        tutorialEmail.SetActive(true);
    }

    public void TutorialNextPage2()
    {
        tutorialEmail.SetActive(false);

        tutorialSnek.SetActive(true);
    }

    public void TutorialNextPage3()
    {
        tutorialSnek.SetActive(false);

        tutorialSlot.SetActive(true);
    }

    public void TutorialNextPage4()
    {
        tutorialSlot.SetActive(false);

        tutorialResource.SetActive(true);
    }

    public void TutorialNextPage5()
    {
        tutorialResource.SetActive(false);

        tutorialBrowser.SetActive(true);
    }

    public void TutorialNextPage6()
    {
        tutorialBrowser.SetActive(false);

        tutorialInsurance.SetActive(true);
    }

    public void TutorialNextPage7()
    {
        tutorialInsurance.SetActive(false);

        tutorialConfirmation.SetActive(true);
    }

    public void TutorialNextPage8()
    {
        tutorialConfirmation.SetActive(false);

        tutorialAftConfirmation.SetActive(true);
    }

    public void TutorialNextPage9()
    {
        tutorialAftConfirmation.SetActive(false);

        finalTutorial.SetActive(true);
    }

    public void TutorialNextPage10()
    {
        finalTutorial.SetActive(false);

        investmentTutorial.SetActive(true);
    }

    public void TutorialNextPage11()
    {
        investmentTutorial.SetActive(false);

        highProfileInvestment.SetActive(true);
    }
    
    public void TutorialNextPage12()
    {
        highProfileInvestment.SetActive(false);

        lowProfileInvesment.SetActive(true);
    }

    public void TutorialNextPage13()
    {
        lowProfileInvesment.SetActive(false);

        objectiveTutorial.SetActive(true);
    }

    public void TutorialResumeGame()
    {
        objectiveTutorial.SetActive(false);
    }

    //when player presses Quit button, it goes to Main Menu and sets the timeScale to 1
    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    //if currentMoney equals to money, the outline and moneyIcon will set its animator to false
    //else if currentMoney is greater than money, it sets the outline and moneyIcon animator to true which it will flash red
    //else it will flash green
    public IEnumerator AnimateMoneySlider()
    {
        if (currentMoney == money)
        {
            moneyBarOutline.GetComponent<Outline>().enabled = false;
            moneyIcon.GetComponent<Animator>().enabled = false;
        }
        else if (currentMoney> money)
        {
            moneyBarOutline.GetComponent<Outline>().enabled = true;
            moneyIcon.GetComponent<Animator>().enabled = true;
            moneyBarOutline.GetComponent<Outline>().effectColor = Color.red;
        }
        else
        {
            moneyBarOutline.GetComponent<Outline>().enabled = true;
            moneyIcon.GetComponent<Animator>().enabled = true;
            moneyBarOutline.GetComponent<Outline>().effectColor = Color.green;
        }
        elapsedTime = 0;
        Debug.Log(elapsedTime);

        while (elapsedTime < animationDur)
        {
            elapsedTime += Time.deltaTime;
            float normTime = Mathf.Clamp01(elapsedTime / animationDur);

            moneySlider.value = Mathf.Lerp(currentMoney, money, normTime);

            yield return null;
        }

        moneySlider.value = money;
        moneyBarOutline.GetComponent<Outline>().enabled = false;
        moneyIcon.GetComponent<Animator>().enabled = false;
    }

    //if currentPopularity equals to popularity, the outline and popularityIcon will set its animator to false
    //else if currentPopularity is greater than popularity, it sets the outline and popularityIcon animator to true which it will flash red
    //else it will flash green
    public IEnumerator AnimatePopularitySlider()
    {
        if (currentPopularity == popularity)
        {
            popularBarOutline.GetComponent<Outline>().enabled = false;
            popularIcon.GetComponent<Animator>().enabled = false;
        }
        else if (currentPopularity > popularity)
        {
            popularBarOutline.GetComponent<Outline>().enabled = true;
            popularIcon.GetComponent<Animator>().enabled = true;
            popularBarOutline.GetComponent<Outline>().effectColor = Color.red;
        }
        else
        {
            popularBarOutline.GetComponent<Outline>().enabled = true;
            popularIcon.GetComponent<Animator>().enabled = true;
            popularBarOutline.GetComponent<Outline>().effectColor = Color.green;
        }
        elapsedTime = 0;
        Debug.Log(elapsedTime);

        while (elapsedTime < animationDur)
        {
            elapsedTime += Time.deltaTime;
            float normTime = Mathf.Clamp01(elapsedTime / animationDur);

            popularitySlider.value = Mathf.Lerp(currentPopularity, popularity, normTime);

            yield return null;
        }

        popularitySlider.value = popularity;
        popularBarOutline.GetComponent<Outline>().enabled = false;
        popularIcon.GetComponent<Animator>().enabled = false;
    }

    //if currentHappiness equals to happiness, the outline and happinessIcon will set its animator to false
    //else if currentHappiness is greater than happiness, it sets the outline and happinessIcon animator to true which it will flash red
    //else it will flash green
    public IEnumerator AnimateHappinessSlider()
    {
        
        if(currentHappiness == happiness)
        {
            happinessBarOutline.GetComponent<Outline>().enabled = false;
            happinessIcon.GetComponent<Animator>().enabled = false;
        }
        else if (currentHappiness > happiness)
        {
            happinessBarOutline.GetComponent<Outline>().enabled = true;
            happinessIcon.GetComponent<Animator>().enabled = true;
            happinessBarOutline.GetComponent<Outline>().effectColor = Color.red;
        }
        else
        {
            happinessBarOutline.GetComponent<Outline>().enabled = true;
            happinessIcon.GetComponent<Animator>().enabled = true;
            happinessBarOutline.GetComponent<Outline>().effectColor = Color.green;
        }
        
        elapsedTime = 0;
        Debug.Log(elapsedTime);

        while (elapsedTime < animationDur)
        {
            elapsedTime += Time.deltaTime;
            float normTime = Mathf.Clamp01(elapsedTime / animationDur);

            happinessSlider.value = Mathf.Lerp(currentHappiness, happiness, normTime);

            yield return null;
        }

        happinessSlider.value = happiness;
        happinessBarOutline.GetComponent<Outline>().enabled = false;
        happinessIcon.GetComponent<Animator>().enabled = false;
    }
}
