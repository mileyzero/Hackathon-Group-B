using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Investment : MonoBehaviour
{
    //Reference script to gmTime
    public MiniGameTimer gmTime;

    //Reference script to GM
    public GameManager GM;

    //Reference script to browserManager
    public Browser browserManager;

    //Reference script to DM
    public DialogueManager DM;

    //Reference script to profileGM;
    public Profile profileGM;

    //private GameObject for scam and goodObject
    private GameObject scamObject;
    private GameObject investmentObject;
    private GameObject spawned;

    public  GameObject randomObject;

    //a List to set how many personas to randomize
    public List<GameObject> spawnObjects;

    //spawnArea for player model
    public GameObject spawnArea;

    //reference GameObject for investmentScenario
    public GameObject investmentScenario;

    //Button for interaction of scenario
    public Button scenarioButton;

    //GameObjects for scenario
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject investmentDialogue;
    public GameObject nameBox;

    public int minValue = 0;
    public int maxValue = 20;

    // Start is called before the first frame update
    void Start()
    {
        //referencing GameManager GM's investmemnt button and notification icon to false
        GM.investmentNotiIcon.SetActive(false);
        GM.investmentButton.SetActive(false);

        //set button enabled to false
        enabled = false;

        //set scenario, button and dialogue to false
        investmentScenario.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        investmentDialogue.SetActive(false);
        nameBox.SetActive(false);
    }

    //in SpawnObject, there will be an randomRange that detects how many objects are in the array spawnObjects
    //then, a new vector3 spawnPosition equals to spawnArea's position
    //a randomObject will take the current index of spawnObjects' array
    //which then spawned will instantiate the randomObject chosen on the current spawnPosition
    //spawned will then be set under a parent object's position
    public void SpawnObject()
    {
        Debug.Log("Spawned");

        int randomRange = Random.Range(0, spawnObjects.Count);
        Vector3 spawnPosition = spawnArea.transform.position;
        randomObject = spawnObjects[randomRange];
        spawned = Instantiate(randomObject, spawnPosition, Quaternion.identity);
        spawned.transform.SetParent(spawnArea.transform, false);
        spawned.transform.position = spawnArea.transform.position;
    }

    //In SpawnScenario, it will call SpawnObject method, then set invesmentScenario and nameBox set active to true
    //the scenarioButton will then set to false to prevent spamming of dialogues appearing and only one to appear
    //lastly, StartCoroutine of AnimationPlay which is another method in 0.5 seconds
    public void SpawnScenario()
    {
        SpawnObject();

        investmentScenario.SetActive(true);
        nameBox.SetActive(true);

        scenarioButton.enabled = false;
        Debug.Log(scenarioButton.enabled);

        StartCoroutine(AnimationPlay(0.5f));
    }

    //In AnimationPlay, it will return a float of seconds and set yes, no and investmentDialogue set active to true
    IEnumerator AnimationPlay(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        yesButton.SetActive(true);
        noButton.SetActive(true);
        bool isActive = investmentDialogue.activeSelf;
        Debug.Log("Invesment AnimDialogue is " + isActive);
        investmentDialogue.SetActive(true);
    }

    //ScenarioTransition only occurs during Investment scenarios, there will be two int that will randomize between two values of minValue and maxValue.
    //If randomVal1 is more than 1 and less than 5, randomVal2 is more than 1 and less than 5, it hides the main game and load the 'Car' mini game
    //If randomVal1 is more than 5 and less than 10, randomVal2 is more than 5 and less than 10, it hides the main game and load the 'DoodleJump' mini game
    //If randomVal1 is more than 10 and less than 15, randomVal2 is more than 10 and less than 15, it hides the main game and load the 'SlotMachine' mini game
    //If randomVal1 is more than 15 and less than 20, randomVal2 is more than 15 and less than 20, it hides the main game and load the 'Golf' mini game
    public void ScenarioTransition()
    {
        int randomVal1 = Random.Range(minValue, maxValue);
        int randomVal2 = Random.Range(minValue, maxValue);

        if(gmTime.MGTimer <= 0)
        {
            if ((randomVal1 > 1 && randomVal1 < 6) || (randomVal2 > 1 && randomVal2 < 6))
            {
                Debug.Log("Random Values: " + randomVal1 + ", " + randomVal2);
                GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>()._maingame.gameObject.SetActive(false);
                Debug.Log("Scene: Car");
                SceneManager.LoadScene("Car");
            }
            if ((randomVal1 > 6 && randomVal1 < 12) || (randomVal2 > 6 && randomVal2 < 12))
            {
                Debug.Log("Random Values: " + randomVal1 + ", " + randomVal2);
                GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>()._maingame.gameObject.SetActive(false);
                Debug.Log("Scene: DoodleJump");
                SceneManager.LoadScene("DoodleJump");
            }
            if ((randomVal1 > 12 && randomVal1 < 18) || (randomVal2 > 12 && randomVal2 < 18))
            {
                Debug.Log("Random Values: " + randomVal1 + ", " + randomVal2);
                GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>()._maingame.gameObject.SetActive(false);
                Debug.Log("Scene: Golf");
                SceneManager.LoadScene("Golf");
            }
            else
            {
                Debug.Log("Scene transition condition not met. Random Values: " + randomVal1 + ", " + randomVal2);
            }
        }
        else
        {
            Debug.Log("No Scene Played due to timer");
        }
    }

    public void DestroyObject()
    {
        //investmentObject to find tag "investment"
        investmentObject = GameObject.FindGameObjectWithTag("good");

        //scamObject to find tag "scam"
        scamObject = GameObject.FindGameObjectWithTag("scam");

        //if the randomObject tag is "investment", destroy gameObject
        if (randomObject.tag == "good")
        {
            Destroy(investmentObject);
        }

        //if the randomObject tag is "scam", destroy gameObject
        else if (randomObject.tag == "scam")
        {
            Destroy(scamObject);
        }
    }

    public void YesClick()
    {
        int index = 0;

        ScenarioTransition();

        //if randomObject tag equals to "scam"
        if (randomObject.tag == "scam")
        {
            //investmentScenario will set active to false
            investmentScenario.SetActive(false);

            //in a foreach, there's a variable option set for a reference script of DM (Dialogue Manager) taking reference from investmentLines
            //in each of the case, each dialogue will give a different value for different stats

            switch (DM.investmentLines[index])
            {
                case "Hi Boss! Our accountants have noticed that we have a surplus in capital. They suggested that you expand the business and offices. Would you like to follow through?":
                    {
                        if (browserManager.investmentInsurance != true)
                        {
                            Debug.Log("1 Scam");
                            GM.money -= Random.Range(3f, 8f);
                            GM.popularity -= Random.Range(5f, 7f);

                            browserManager.insuranceActive.SetActive(false);
                            browserManager.insuranceGreyed.SetActive(true);
                        }
                        break;
                    }
                case "Hey Pal! Heard your business has been thriving. I'm writing to ask you whether you would like to invest in one business project. You will receive a good margin of the profits!":
                    {
                        if(browserManager.investmentInsurance != true)
                        {
                            Debug.Log("2 Scam");
                            GM.money -= Random.Range(5f, 9f);
                            GM.popularity -= Random.Range(4f, 10f);

                            browserManager.insuranceActive.SetActive(false);
                            browserManager.insuranceGreyed.SetActive(true);

                        }
                        break;
                    }
                case "Hi. Would you like to provide some funds for my start-up business? We will pay you handsomely once things start to pick up.":
                    {
                        if(browserManager.investmentInsurance != true)
                        {
                            Debug.Log("3 Scam");
                            GM.money -= Random.Range(5f, 10f);
                            GM.popularity -= Random.Range(3f, 7f);

                            browserManager.insuranceActive.SetActive(false);
                            browserManager.insuranceGreyed.SetActive(true);
                        }
                        break;
                    }
                case "Hi, I am a representative of an Energy Company called Operate Energy. Our partnership will help us harness and distribute energy. Would you like to invest?":
                    {
                        if (browserManager.investmentInsurance != true)
                        {
                            Debug.Log("4 Scam");
                            GM.money -= Random.Range(7f, 15f);
                            GM.popularity -= Random.Range(4f, 8f);

                            browserManager.insuranceActive.SetActive(false);
                            browserManager.insuranceGreyed.SetActive(true);
                        }
                        break;
                    }
                case "Hey there! I'm the developer of Among Us, and I could really use some financial support to help me develop this game! Would you help me?":
                    {
                        if(browserManager.investmentInsurance != true)
                        {
                            Debug.Log("5 Scam");
                            GM.money -= Random.Range(5f, 9f);
                            GM.popularity -= Random.Range(3f, 6f);

                            browserManager.insuranceActive.SetActive(false);
                            browserManager.insuranceGreyed.SetActive(true);
                        }
                        break;
                    }
                case "Hi! I would like to provide an upgrade of ads to your company! Do you want some traction for your ads?":
                    {
                        if(browserManager.investmentInsurance != true)
                        {
                            Debug.Log("6 Scam");
                            GM.money -= Random.Range(5f, 15f);
                            GM.popularity -= Random.Range(4f, 9f);

                            browserManager.insuranceActive.SetActive(false);
                            browserManager.insuranceGreyed.SetActive(true);
                        }
                        break;
                    }
                case "Hey there! Want to be the face of our awesome brand? We're hiring models to help us advertise - interested?":
                    {
                        if(browserManager.investmentInsurance != true)
                        {
                            Debug.Log("7 Scam");
                            GM.money -= Random.Range(5f, 15f);
                            GM.popularity -= Random.Range(4f, 9f);

                            browserManager.insuranceActive.SetActive(false);
                            browserManager.insuranceGreyed.SetActive(true);
                        }
                        break;
                    }
                case "Hi! I am a representative of a clothing brand called Doo Nut, we would be thrilled to offer you a deal with our clothing brand - are you interested?":
                    {
                        if(browserManager.investmentInsurance != true)
                        {
                            Debug.Log("8 Scam");
                            GM.money -= Random.Range(5f, 10f);
                            GM.popularity -= Random.Range(3f, 9f);

                            browserManager.insuranceActive.SetActive(false);
                            browserManager.insuranceGreyed.SetActive(true);
                        }
                        break;
                    }
                case "Hi, I would like to catch everyone's attention and spread your company's name by advertising it on a billboard! Are you interested?":
                    {
                        if(browserManager.investmentInsurance != true)
                        {
                            Debug.Log("9 Scam");
                            GM.money -= Random.Range(5f, 20f);
                            GM.popularity -= Random.Range(5f, 10f);

                            browserManager.insuranceActive.SetActive(false);
                            browserManager.insuranceGreyed.SetActive(true);
                        }
                        break;
                    }
                case "Hi, I would like to provide services to upgrade your company's office area, are you interested?":
                    {
                        if(browserManager.investmentInsurance != true)
                        {
                            Debug.Log("10 Scam");
                            GM.money -= Random.Range(5f, 20f);
                            GM.popularity -= Random.Range(4f, 10f);

                            browserManager.insuranceActive.SetActive(false);
                            browserManager.insuranceGreyed.SetActive(true);
                        }
                        break;
                    }
                default:
                    {
                        Debug.Log("Default case or unrecognized dialogue.");
                        break;
                    }
            }

            GM.happinessSlider.value = GM.happiness;
            GM.moneySlider.value = GM.money;
            GM.popularitySlider.value = GM.popularity;

            index++;


            profileGM.trustValue = 0;
            profileGM.successValue = 0;

            if(browserManager.investmentInsurance == true)
            {
                browserManager.investmentInsurance = false;
                Debug.Log(browserManager.investmentInsurance);
            }

            DestroyObject();
        }
        //if randomObject tag equals to "investment"
        else if (randomObject.tag == "good")
        {
            //investmentScenario will set active to false
            investmentScenario.SetActive(false);

            //in a foreach, there's a variable option set for a reference script of DM (Dialogue Manager) taking reference from investmentLines
            //in each of the case, each dialogue will give a different value for different stats
            switch (DM.investmentLines[index])
            {
                case "Hi Boss! Our accountants have noticed that we have a surplus in capital. They suggested that you expand the business and offices. Would you like to follow through?":
                    {
                        Debug.Log("1 Investment");
                        GM.money += Random.Range(3f, 8f);
                        GM.popularity += Random.Range(5f, 7f);

                        break;
                    }
                case "Hey Pal! Heard your business has been thriving. I'm writing to ask you whether you would like to invest in one business project. You will receive a good margin of the profits!":
                    {
                        Debug.Log("2 Investment");
                        GM.money += Random.Range(5f, 9f);
                        GM.popularity += Random.Range(4f, 10f);

                        break;
                    }
                case "Hi. Would you like to provide some funds for my start-up business? We will pay you handsomely once things start to pick up.":
                    {
                        Debug.Log("3 Investment");
                        GM.money += Random.Range(5f, 10f);
                        GM.popularity += Random.Range(3f, 7f);

                        break;
                    }
                case "Hi, I am a representative of an Energy Company called Operate Clean Energy. We believe our proposal for a mutually beneficial partnership will revolutionize the way we harness and distribute energy. Would you like to invest in our company?":
                    {
                        Debug.Log("4 Investment");
                        GM.money += Random.Range(7f, 15f);
                        GM.popularity += Random.Range(4f, 8f);

                        break;
                    }
                case "Hey there! I'm the developer of Among Us, and I could really use some financial support to help me develop this game! Would you help me?":
                    {
                        Debug.Log("5 Investment");
                        GM.money -= Random.Range(5f, 9f);
                        GM.popularity += Random.Range(3f, 6f);

                        break;
                    }
                case "Hi! I would like to provide an upgrade of ads to your company! Do you want some traction for your ads?":
                    {
                        Debug.Log("6 Investment");
                        GM.money -= Random.Range(4f, 10f);
                        GM.popularity += Random.Range(5f, 15f);

                        break;
                    }
                case "Hey there! Want to be the face of our awesome brand? We're hiring models to help us advertise - interested?":
                    {
                        Debug.Log("7 Investment");
                        GM.money -= Random.Range(5f, 10f);
                        GM.popularity += Random.Range(4f, 9f);

                        break;
                    }
                case "Hi! I am a representative of a clothing brand called Doo Nut, we would be thrilled to offer you a deal with our clothing brand - are you interested?":
                    {
                        Debug.Log("8 Investment");
                        GM.money -= Random.Range(5f, 10f);
                        GM.popularity += Random.Range(3f, 9f);

                        break;
                    }
                case "Hi, I would like to catch everyone's attention and spread your company's name by advertising it on a billboard! Are you interested?":
                    {
                        Debug.Log("9 Investment");
                        GM.money -= Random.Range(5f, 20f);
                        GM.popularity += Random.Range(5f, 10f);

                        break;
                    }
                case "Hi, I would like to provide services to upgrade your company's office area, are you interested?":
                    {
                        Debug.Log("10 Investment");
                        GM.money -= Random.Range(5f, 20f);
                        GM.popularity += Random.Range(4f, 10f);

                        break;
                    }
                default:
                    {
                        Debug.Log("Default case or unrecognized dialogue.");
                        break;
                    }
            }

            GM.happinessSlider.value = GM.happiness;
            GM.moneySlider.value = GM.money;
            GM.popularitySlider.value = GM.popularity;

            index++;

            profileGM.trustValue = 0;
            profileGM.successValue = 0;

            DestroyObject();
        }

        GM.snekGameButton.enabled = true;
        GM.FunctionUpdates();
    }

    public void NoClick()
    {
        //if randomObject tag equals to "scam"
        if (randomObject.tag == "scam")
        {
            //investmentScenario will set active to false
            investmentScenario.SetActive(false);

            profileGM.trustValue = 0;
            profileGM.successValue = 0;

            DestroyObject();
        }


        else if (randomObject.tag == "good")
        {
            int index = 0;

            //investmentScenario will set active to false
            investmentScenario.SetActive(false);

            switch (DM.investmentLines[index])
            {
                case "Hi Boss! Our accountants have noticed that we have a surplus in capital. They suggested that you expand the business and offices. Would you like to follow through?":
                    {
                        Debug.Log("1 No Investment");
                        GM.money -= Random.Range(3f, 8f);
                        GM.popularity -= Random.Range(5f, 7f);

                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hey Pal! Heard your business has been thriving. I'm writing to ask you whether you would like to invest in one business project. You will receive a good margin of the profits!":
                    {
                        Debug.Log("2 No Investment");
                        GM.money -= Random.Range(5f, 9f);
                        GM.popularity -= Random.Range(4f, 10f);

                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hi. Would you like to provide some funds for my start-up business? We will pay you handsomely once things start to pick up.":
                    {
                        Debug.Log("3 No Investment");
                        GM.money -= Random.Range(5f, 10f);
                        GM.popularity -= Random.Range(3f, 7f);

                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hi, I am a representative of an Energy Company called Operate Clean Energy. We believe our proposal for a mutually beneficial partnership will revolutionize the way we harness and distribute energy. Would you like to invest in our company?":
                    {
                        Debug.Log("4 No Investment");
                        GM.money -= Random.Range(7f, 15f);
                        GM.popularity -= Random.Range(4f, 8f);

                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hey there! I'm the developer of Among Us, and I could really use some financial support to help me develop this game! Would you help me?":
                    {
                        Debug.Log("5 No Investment");
                        GM.popularity -= Random.Range(3f, 9f);
                        GM.money -= Random.Range(5f, 9f);

                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hi! I would like to provide an upgrade of ads to your company! Do you want some traction for your ads?":
                    {
                        Debug.Log("6 No Investment");
                        GM.money -= Random.Range(4f, 10f);
                        GM.popularity -= Random.Range(5f, 15f);

                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hey there! Want to be the face of our awesome brand? We're hiring models to help us advertise - interested?":
                    {
                        Debug.Log("7 No Investment");
                        GM.money -= Random.Range(5f, 10f);
                        GM.popularity -= Random.Range(4f, 9f);

                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hi! I am a representative of a clothing brand called Doo Nut, we would be thrilled to offer you a deal with our clothing brand - are you interested?":
                    {
                        Debug.Log("8 No Investment");
                        GM.money -= Random.Range(5f, 10f);
                        GM.popularity -= Random.Range(3f, 9f);

                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hi, I would like to catch everyone's attention and spread your company's name by advertising it on a billboard! Are you interested?":
                    {
                        Debug.Log("9 No Investment");
                        GM.money -= Random.Range(5f, 20f);
                        GM.popularity -= Random.Range(5f, 10f);

                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hi, I would like to provide services to upgrade your company's office area, are you interested?":
                    {
                        Debug.Log("10 No Investment");
                        GM.money -= Random.Range(5f, 20f);
                        GM.popularity -= Random.Range(4f, 10f);

                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                default:
                    {
                        Debug.Log("Default case or unrecognized dialogue.");
                        break;
                    }
            }

            GM.happinessSlider.value = GM.happiness;
            GM.moneySlider.value = GM.money;
            GM.popularitySlider.value = GM.popularity;

            index++;

            profileGM.trustValue = 0;
            profileGM.successValue = 0;

            DestroyObject();
        }

        GM.snekGameButton.enabled = true;
        GM.FunctionUpdates();
    }
}
