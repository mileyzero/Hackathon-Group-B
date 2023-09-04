using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreGame : MonoBehaviour
{
    public GameObject _maingame;
    public GameManager gameManager;
    public Browser accidentinsurance;
    public MiniGameTimer mgTimer;

    public AudioClip level1;
    public AudioClip level2;
    public AudioClip level3;

    public bool hasPlayedLevel1;
    public bool hasPlayedLevel2;
    public bool hasPlayedLevel3;

    public float money;
    public float popularity;
    public float happiness;

    public float moneyslider;
    public float popularityslider;
    public float happinessslider;

    public AudioSource levelAudioChange;

    public bool achActive = false;

    public Texture2D cursorPixel;
    public Texture2D pressedCursor;

    //Lose Money Achievement
    public GameObject loseMoneyAchPanel;
    public GameObject loseMoneyAchDesc;
    public GameObject loseMoneyAchTitle;

    public GameObject loseMoneyAchImage;

    public static int loseMoneyAchCount;

    public int loseMoneyAchTrigger = 01;
    public int loseMoneyAchCode;

    //Slot Achievement
    public GameObject gambleAchPanel;
    public GameObject gambleAchDesc;
    public GameObject gambleAchTitle;

    public GameObject gambleAchImage;

    public static int gambleAchCount;

    public int gambleAchTrigger = 02;
    public int gambleAchCode;

    //Golf Achievement
    public GameObject golfAchPanel;
    public GameObject golfAchDesc;
    public GameObject golfAchTitle;

    public GameObject golfAchImage;

    public static int golfAchCount;

    public int golfAchTrigger = 03;
    public int golfAchCode;

    //Car Win Achievement
    public GameObject carWinAchPanel;
    public GameObject carWinAchDesc;
    public GameObject carWinAchTitle;

    public GameObject carWinAchImage;

    public static int carWinAchCount;

    public int carWinAchTrigger = 04;
    public int carWinAchCode;

    //Car Lose Achievement
    public GameObject carLoseAchPanel;
    public GameObject carLoseAchDesc;
    public GameObject carLoseAchTitle;

    public GameObject carLoseAchImage;

    public static int carLoseAchCount;

    public int carLoseAchTrigger = 05;
    public int carLoseAchCode;

    //Gamble Lucky 7 Achievement
    public GameObject gambleLucky7AchPanel;
    public GameObject gambleLucky7AchDesc;
    public GameObject gambleLucky7AchTitle;

    public GameObject gambleLucky7AchImage;

    public static int gambleLucky7AchCount;

    public int gambleLucky7AchTrigger = 06;
    public int gambleLucky7AchCode;

    //Doodle Jump Win Achievement
    public GameObject doodleWinAchPanel;
    public GameObject doodleWinAchDesc;
    public GameObject doodleWinAchTitle;

    public GameObject doodleWinAchImage;

    public static int doodleWinAchCount;

    public int doodleWinAchTrigger = 07;
    public int doodleWinAchCode;

    //Insurance Achievement
    public GameObject insuranceAchPanel;
    public GameObject insuranceAchDesc;
    public GameObject insuranceAchTitle;

    public GameObject insuranceAchImage;

    public static int insuranceAchCount;

    public int insuranceAchTrigger = 3;
    public int insuranceAchCode;

    //Popularity Achievement
    public GameObject popularityAchPanel;
    public GameObject popularityAchDesc;
    public GameObject popularityAchTitle;

    public GameObject popularityAchImage;

    public static int popularityAchCount;

    public int popularityAchTrigger = 10;
    public int popularityAchCode;

    //Eat self Snake Achievement
    public GameObject snakeEatAchPanel;
    public GameObject snakeEatAchDesc;
    public GameObject snakeEatAchTitle;

    public GameObject snakeEatAchImage;

    public static int snakeEatAchCount;

    public int snakeEatAchTrigger = 11;
    public int snakeEatAchCode;

    //Win Game Achievement
    public GameObject winAchPanel;
    public GameObject winAchDesc;
    public GameObject winAchTitle;

    public GameObject winAchImage;

    public static int winAchCount;

    public int winAchTrigger = 1;
    public int winAchCode;

    //Lose Happiness Achievement
    public GameObject loseHappinessAchPanel;
    public GameObject loseHappinessAchDesc;
    public GameObject loseHappinessAchTitle;

    public GameObject loseHappinessAchImage;

    public static int loseHappinessAchCount;

    public int loseHappinessAchTrigger = 1;
    public int loseHappinessAchCode;

    public bool wonallgolf;
    public bool golfwon_Level1 = false;
    public bool golfwon_Level2 = false;
    public bool golfwon_Level3 = false;
    public bool golfwon_Level4 = false;
    public bool golfwon_Level5 = false;
    public bool golfwon_Level6 = false;

    private CursorMode _cursorMode = CursorMode.ForceSoftware;

    private void Start()
    {
        Cursor.SetCursor(cursorPixel, Vector2.zero, _cursorMode);

        wonallgolf = false;
        golfwon_Level1 = false;
        golfwon_Level2 = false;
        golfwon_Level3 = false;
        golfwon_Level4 = false;
        golfwon_Level5 = false;
        golfwon_Level6 = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(pressedCursor, Vector2.zero, _cursorMode);
            gameManager.btnClick.Play();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursorPixel, Vector2.zero, _cursorMode);
        }

        if (golfwon_Level1 == true && golfwon_Level2 == true && golfwon_Level3 == true && golfwon_Level4 == true && golfwon_Level5 == true && golfwon_Level6 == true)
        {
            wonallgolf = true;
        }

        money = gameManager.money;
        popularity = gameManager.popularity;
        happiness = gameManager.happiness;

        gameManager.moneySlider.value = money;
        gameManager.popularitySlider.value = popularity;
        gameManager.happinessSlider.value = happiness;

        moneyslider = gameManager.moneySlider.value;
        popularityslider = gameManager.popularitySlider.value;
        happinessslider = gameManager.happinessSlider.value;

        if (!hasPlayedLevel1)
        {
            if (popularity < 30)
            {
                levelAudioChange.clip = level1;
                levelAudioChange.Play();

                hasPlayedLevel1 = true;
                hasPlayedLevel2 = false;
                hasPlayedLevel3 = false;
            }
        }

        if (!hasPlayedLevel2)
        {
            if (popularity > 30 && popularity < 60)
            {
                levelAudioChange.clip = level2;
                levelAudioChange.Play();

                hasPlayedLevel1 = false;
                hasPlayedLevel2 = true;
                hasPlayedLevel3 = false;
            }
        }

        if (!hasPlayedLevel3)
        {
            if (popularity > 60 && popularity < 90)
            {
                levelAudioChange.clip = level3;
                levelAudioChange.Play();

                hasPlayedLevel1 = false;
                hasPlayedLevel2 = false;
                hasPlayedLevel3 = true;
            }
        }

        //Lose Money Achievement
        loseMoneyAchCode = PlayerPrefs.GetInt("loseMoneyAch");
        if(loseMoneyAchCount == loseMoneyAchTrigger && loseMoneyAchCode != 00001)
        {
            StartCoroutine(TriggerLoseMoneyAch());
        }

        //Gamble Achievement
        gambleAchCode = PlayerPrefs.GetInt("gambleAch");
        if (gambleAchCount == gambleAchTrigger && gambleAchCode != 00002)
        {
            StartCoroutine(TriggerGambleAch());
        }

        //Golf Achievement
        golfAchCode = PlayerPrefs.GetInt("golfAch");
        if (golfAchCount == golfAchTrigger && golfAchCode != 00003)
        {
            StartCoroutine(TriggerGolfAch());
        }

        //Car Win Achievement
        carWinAchCode = PlayerPrefs.GetInt("carWinAch");
        if(carWinAchCount == carWinAchTrigger && carWinAchCode != 00004)
        {
            StartCoroutine(TriggerCarWinAch());
        }

        //Car Lose Achievement
        carLoseAchCode = PlayerPrefs.GetInt("carLoseAch");
        if (carLoseAchCount == carLoseAchTrigger && carLoseAchCode != 00005)
        {
            StartCoroutine(TriggerCarLoseAch());
        }

        //Gamble Lucky 7 Achievement
        gambleLucky7AchCode = PlayerPrefs.GetInt("gambleLucky7Ach");
        if(gambleLucky7AchCount == gambleLucky7AchTrigger && gambleLucky7AchCode != 00006)
        {
            StartCoroutine(TriggerGambleLucky7Ach());
        }

        //Doodle Jump Win Achievement
        doodleWinAchCode = PlayerPrefs.GetInt("doodleJumpWinAch");
        if (doodleWinAchCount == doodleWinAchTrigger && doodleWinAchCode != 00007)
        {
            StartCoroutine(TriggerDoodleJumpWinAch());
        }

        //Insurance Achievement
        insuranceAchCode = PlayerPrefs.GetInt("insuranceAch");
        if(insuranceAchCount == insuranceAchTrigger && insuranceAchCode != 00009)
        {
            Debug.Log("coroutine started");
            StartCoroutine(TriggerInsuranceAch());
        }

        //Popularity Achievement
        popularityAchCode = PlayerPrefs.GetInt("popularityAch");
        if(popularityAchCount == popularityAchTrigger && popularityAchCode != 00010)
        {
            StartCoroutine(TriggerPopularityAch());
        }

        //Snake Achievement
        snakeEatAchCode = PlayerPrefs.GetInt("snakeEatAch");
        if(snakeEatAchCount == snakeEatAchTrigger && snakeEatAchCode != 00011)
        {
            StartCoroutine(TriggerSnakeEatItselfAch());
        }

        //Win Game Achievement
        winAchCode = PlayerPrefs.GetInt("winAch");
        if (winAchCount == winAchTrigger && winAchCode != 00012)
        {
            StartCoroutine(TriggerWinGameAch());
        }

        //Lose Happiness Achievement
        loseHappinessAchCode = PlayerPrefs.GetInt("loseHappinessAch");
        if(loseHappinessAchCount == loseHappinessAchTrigger && loseHappinessAchCode != 00013)
        {
            StartCoroutine(TriggerLoseHappinessAch());
        }
    }

    IEnumerator TriggerLoseMoneyAch()
    {
        achActive = true;
        loseMoneyAchCode = 00001;

        PlayerPrefs.SetInt("loseMoneyAch", loseMoneyAchCode);

        loseMoneyAchImage.SetActive(true);
        loseMoneyAchPanel.SetActive(true);

        loseMoneyAchTitle.GetComponent<TextMeshProUGUI>().text = "Broke Boi";
        loseMoneyAchDesc.GetComponent<TextMeshProUGUI>().text = "Lose by Money";

        yield return new WaitForSeconds(4);

        //Resetting UI
        loseMoneyAchPanel.SetActive(false);
        loseMoneyAchImage.SetActive(false);
        loseMoneyAchTitle.GetComponent<TextMeshProUGUI>().text = "";
        loseMoneyAchDesc.GetComponent<TextMeshProUGUI>().text = "";
        achActive = false;
    }

    IEnumerator TriggerGambleAch()
    {
        achActive = true;
        loseMoneyAchCode = 00002;

        PlayerPrefs.SetInt("gambleAch", gambleAchCode);

        gambleAchImage.SetActive(true);
        gambleAchPanel.SetActive(true);

        gambleAchTitle.GetComponent<TextMeshProUGUI>().text = "Gambler";
        gambleAchDesc.GetComponent<TextMeshProUGUI>().text = "Spent all their money on the slot machine";

        yield return new WaitForSeconds(4);

        //Resetting UI
        gambleAchPanel.SetActive(false);
        gambleAchImage.SetActive(false);
        gambleAchTitle.GetComponent<TextMeshProUGUI>().text = "";
        gambleAchDesc.GetComponent<TextMeshProUGUI>().text = "";
        achActive = false;
    }

    IEnumerator TriggerGolfAch()
    {
        achActive = true;
        golfAchCode = 00003;

        PlayerPrefs.SetInt("golfAch", golfAchCode);

        golfAchImage.SetActive(true);
        golfAchPanel.SetActive(true);

        golfAchTitle.GetComponent<TextMeshProUGUI>().text = "Tiger Woods' Son";
        golfAchDesc.GetComponent<TextMeshProUGUI>().text = "Win golf first time";

        yield return new WaitForSeconds(4);

        //Resetting UI
        golfAchPanel.SetActive(false);
        golfAchImage.SetActive(false);
        golfAchTitle.GetComponent<TextMeshProUGUI>().text = "";
        golfAchDesc.GetComponent<TextMeshProUGUI>().text = "";
        achActive = false;
    }

    IEnumerator TriggerCarWinAch()
    {
        achActive = true;
        carWinAchCode = 00004;

        PlayerPrefs.SetInt("carWinAch", carWinAchCode);

        carWinAchImage.SetActive(true);
        carWinAchPanel.SetActive(true);

        carWinAchTitle.GetComponent<TextMeshProUGUI>().text = "Inital D";
        carWinAchDesc.GetComponent<TextMeshProUGUI>().text = "Win Car mini-game for the first time";

        yield return new WaitForSeconds(4);

        //Resetting UI
        carWinAchPanel.SetActive(false);
        carWinAchImage.SetActive(false);
        carWinAchTitle.GetComponent<TextMeshProUGUI>().text = "";
        carWinAchDesc.GetComponent<TextMeshProUGUI>().text = "";
        achActive = false;
    }

    IEnumerator TriggerCarLoseAch()
    {
        achActive = true;
        carWinAchCode = 00005;

        PlayerPrefs.SetInt("carLoseAch", carLoseAchCode);

        carLoseAchImage.SetActive(true);
        carLoseAchPanel.SetActive(true);

        carLoseAchTitle.GetComponent<TextMeshProUGUI>().text = "Fast and Furious";
        carLoseAchDesc.GetComponent<TextMeshProUGUI>().text = "Crash your car for the first time";

        yield return new WaitForSeconds(4);

        //Resetting UI
        carLoseAchPanel.SetActive(false);
        carLoseAchImage.SetActive(false);
        carLoseAchTitle.GetComponent<TextMeshProUGUI>().text = "";
        carLoseAchDesc.GetComponent<TextMeshProUGUI>().text = "";
        achActive = false;
    }

    IEnumerator TriggerGambleLucky7Ach()
    {
        achActive = true;
        gambleLucky7AchCode = 00006;

        PlayerPrefs.SetInt("gambleLucky7Ach", gambleLucky7AchCode);

        gambleLucky7AchImage.SetActive(true);
        gambleLucky7AchPanel.SetActive(true);

        gambleLucky7AchTitle.GetComponent<TextMeshProUGUI>().text = "God of Gambling";
        gambleLucky7AchDesc.GetComponent<TextMeshProUGUI>().text = "Get Lucky 7 for the First Time";

        yield return new WaitForSeconds(4);

        //Resetting UI
        gambleLucky7AchPanel.SetActive(false);
        gambleLucky7AchImage.SetActive(false);
        gambleLucky7AchTitle.GetComponent<TextMeshProUGUI>().text = "";
        gambleLucky7AchDesc.GetComponent<TextMeshProUGUI>().text = "";
        achActive = false;
    }

    IEnumerator TriggerDoodleJumpWinAch()
    {
        achActive = true;
        doodleWinAchCode = 00007;

        PlayerPrefs.SetInt("doodleJumpWinAch", doodleWinAchCode);

        doodleWinAchImage.SetActive(true);
        doodleWinAchPanel.SetActive(true);

        doodleWinAchTitle.GetComponent<TextMeshProUGUI>().text = "Jump King";
        doodleWinAchDesc.GetComponent<TextMeshProUGUI>().text = "Win Donut Jump for the First Time";

        yield return new WaitForSeconds(4);

        //Resetting UI
        doodleWinAchPanel.SetActive(false);
        doodleWinAchImage.SetActive(false);
        doodleWinAchTitle.GetComponent<TextMeshProUGUI>().text = "";
        doodleWinAchDesc.GetComponent<TextMeshProUGUI>().text = "";
        achActive = false;
    }

    IEnumerator TriggerInsuranceAch()
    {
        achActive = true;
        insuranceAchCode = 00009;

        PlayerPrefs.SetInt("insuranceAch", insuranceAchCode);

        insuranceAchImage.SetActive(true);
        insuranceAchPanel.SetActive(true);

        insuranceAchTitle.GetComponent<TextMeshProUGUI>().text = "Fully Insured";
        insuranceAchDesc.GetComponent<TextMeshProUGUI>().text = "Bought all Insurance";

        yield return new WaitForSeconds(4);

        //Resetting UI
        insuranceAchPanel.SetActive(false);
        insuranceAchImage.SetActive(false);
        insuranceAchTitle.GetComponent<TextMeshProUGUI>().text = "";
        insuranceAchDesc.GetComponent<TextMeshProUGUI>().text = "";
        achActive = false;
    }

    IEnumerator TriggerPopularityAch()
    {
        achActive = true;
        doodleWinAchCode = 00010;

        PlayerPrefs.SetInt("popularityAch", popularityAchCode);

        popularityAchImage.SetActive(true);
        popularityAchPanel.SetActive(true);

        popularityAchTitle.GetComponent<TextMeshProUGUI>().text = "Cancelled";
        popularityAchDesc.GetComponent<TextMeshProUGUI>().text = "Lose by Popularity";

        yield return new WaitForSeconds(4);

        //Resetting UI
        popularityAchPanel.SetActive(false);
        popularityAchImage.SetActive(false);
        popularityAchTitle.GetComponent<TextMeshProUGUI>().text = "";
        popularityAchDesc.GetComponent<TextMeshProUGUI>().text = "";
        achActive = false;
    }

    IEnumerator TriggerSnakeEatItselfAch()
    {
        achActive = true;
        snakeEatAchCode = 00011;

        PlayerPrefs.SetInt("snakeEatAch", snakeEatAchCode);

        snakeEatAchImage.SetActive(true);
        snakeEatAchPanel.SetActive(true);

        snakeEatAchTitle.GetComponent<TextMeshProUGUI>().text = "Snoob";
        snakeEatAchDesc.GetComponent<TextMeshProUGUI>().text = "Lose in Snake for the First Time";

        yield return new WaitForSeconds(4);

        //Resetting UI
        snakeEatAchPanel.SetActive(false);
        snakeEatAchImage.SetActive(false);
        snakeEatAchTitle.GetComponent<TextMeshProUGUI>().text = "";
        snakeEatAchDesc.GetComponent<TextMeshProUGUI>().text = "";
        achActive = false;
    }

    IEnumerator TriggerWinGameAch()
    {
        achActive = true;
        winAchCode = 00012;

        PlayerPrefs.SetInt("winAch", winAchCode);

        winAchImage.SetActive(true);
        winAchPanel.SetActive(true);

        winAchTitle.GetComponent<TextMeshProUGUI>().text = "#1 CEO!!";
        winAchDesc.GetComponent<TextMeshProUGUI>().text = "Winning for the First Time";

        yield return new WaitForSeconds(4);

        //Resetting UI
        winAchPanel.SetActive(false);
        winAchImage.SetActive(false);
        winAchTitle.GetComponent<TextMeshProUGUI>().text = "";
        winAchDesc.GetComponent<TextMeshProUGUI>().text = "";
        achActive = false;
    }

    IEnumerator TriggerLoseHappinessAch()
    {
        achActive = true;
        loseHappinessAchCode = 00013;

        PlayerPrefs.SetInt("loseHappinessAch", loseHappinessAchCode);

        loseHappinessAchImage.SetActive(true);
        loseHappinessAchPanel.SetActive(true);

        loseHappinessAchTitle.GetComponent<TextMeshProUGUI>().text = "1# Public Enemy";
        loseHappinessAchDesc.GetComponent<TextMeshProUGUI>().text = "Lose by Happiness";

        yield return new WaitForSeconds(4);

        //Resetting UI
        loseHappinessAchPanel.SetActive(false);
        loseHappinessAchImage.SetActive(false);
        loseHappinessAchTitle.GetComponent<TextMeshProUGUI>().text = "";
        loseHappinessAchDesc.GetComponent<TextMeshProUGUI>().text = "";
        achActive = false;
    }
}
