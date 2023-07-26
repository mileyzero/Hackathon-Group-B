using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Investment : MonoBehaviour
{
    //Reference script to GM
    public GameManager GM;

    //Reference script to DM
    public DialogueManager DM;

    //Reference script to profileGM;
    public Profile profileGM;

    //private GameObject for scam and goodObject
    private GameObject scamObject;
    private GameObject investmentObject;
    private GameObject spawned;

    //a List to set how many personas to randomize
    public List<GameObject> spawnObjects;

    //spawnArea for player model
    public GameObject spawnArea;

    //reference GameObject for investmentScenario
    public GameObject investmentScenario;

    //Button for interaction of scenario
    public Button scenarioButton;

    //GameObjects for scenario
    public GameObject randomObject;
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject investmentDialogue;
    public GameObject nameBox;

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

        if(randomObject.tag == "investment")
        {
            profileGM.trustSlider.value = Random.Range(40f, 70f);
            profileGM.successSlider.value = Random.Range(40f, 70f);
        }
        else if(randomObject.tag == "scam")
        {
            profileGM.trustSlider.value = Random.Range(10f, 40f);
            profileGM.successSlider.value = Random.Range(25f, 50f);
        }
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

    public void DestroyObject()
    {
        //investmentObject to find tag "investment"
        investmentObject = GameObject.FindGameObjectWithTag("investment");

        //scamObject to find tag "scam"
        scamObject = GameObject.FindGameObjectWithTag("scam");

        //if the randomObject tag is "investment", destroy gameObject
        if (randomObject.tag == "investment")
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
                        Debug.Log("1 Scam");
                        GM.happiness -= Random.Range(0.5f, 2f);
                        GM.money -= Random.Range(0.5f, 2f);
                        GM.popularity -= Random.Range(0.5f, 2f);

                        GM.happinessSlider.value += GM.happiness;
                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hey Pal! Heard your business has been thriving. I'm writing to ask you whether you would like to invest in one business project. You will receive a good margin of the profits!":
                    {
                        Debug.Log("2 Scam");
                        GM.happiness -= Random.Range(1.5f, 3f);
                        GM.money -= Random.Range(1.5f, 3f);
                        GM.popularity -= Random.Range(1.5f, 3f);

                        GM.happinessSlider.value += GM.happiness;
                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hi. Would you like to provide some funds for my start-up business? We will pay you handsomely once things start to pick up.":
                    {
                        Debug.Log("3 Scam");
                        GM.happiness -= Random.Range(1f, 2.5f);
                        GM.money -= Random.Range(1f, 2.5f);
                        GM.popularity -= Random.Range(1f, 2.5f);

                        GM.happinessSlider.value += GM.happiness;
                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hi, I am a representative of an Energy Company called Operate Energy. Our partnership will help us harness and distribute energy. Would you like to invest?":
                    {
                        Debug.Log("4 Scam");
                        GM.happiness -= Random.Range(2f, 4f);
                        GM.money -= Random.Range(2f, 4f);
                        GM.popularity -= Random.Range(2f, 4f);

                        GM.happinessSlider.value += GM.happiness;
                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hey there! I'm the developer of Among Us, and I could really use some financial support to help me develop this game! Would you help me?":
                    {
                        Debug.Log("5 Scam");
                        GM.happiness -= Random.Range(2f, 3.5f);
                        GM.money -= Random.Range(2f, 3.5f);
                        GM.popularity -= Random.Range(2f, 3.5f);

                        GM.happinessSlider.value += GM.happiness;
                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hi! I would like to provide an upgrade of ads to your company! Do you want some traction for your ads?":
                    {
                        Debug.Log("6 Scam");
                        GM.happiness -= Random.Range(1f, 2.5f);
                        GM.money -= Random.Range(1f, 2.5f);
                        GM.popularity -= Random.Range(1f, 2.5f);

                        GM.happinessSlider.value += GM.happiness;
                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hey there! Want to be the face of our awesome brand? We're hiring models to help us advertise - interested?":
                    {
                        Debug.Log("7 Scam");
                        GM.money -= Random.Range(1f, 3.5f);
                        GM.popularity -= Random.Range(1f, 3.5f);

                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hi! I am a representative of a clothing brand called Doo Nut, we would be thrilled to offer you a deal with our clothing brand - are you interested?":
                    {
                        Debug.Log("8 Scam");
                        GM.money -= Random.Range(2f, 4f);
                        GM.popularity -= Random.Range(2f, 4f);

                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hi, I would like to catch everyone's attention and spread your company's name by advertising it on a billboard! Are you interested?":
                    {
                        Debug.Log("9 Scam");
                        GM.money -= Random.Range(3f, 5f);
                        GM.popularity -= Random.Range(3f, 5f);

                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hi, I would like to provide services to upgrade your company's office area, are you interested?":
                    {
                        Debug.Log("10 Scam");
                        GM.happiness -= Random.Range(2f, 5f);
                        GM.money -= Random.Range(2f, 5f);
                        GM.popularity -= Random.Range(2f, 5f);

                        GM.happinessSlider.value += GM.happiness;
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

            DestroyObject();
        }
        //if randomObject tag equals to "investment"
        else if (randomObject.tag == "investment")
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
                        GM.happiness += Random.Range(0.5f, 2f);
                        GM.money += Random.Range(0.5f, 2f);
                        GM.popularity += Random.Range(0.5f, 2f);

                        GM.happinessSlider.value += GM.happiness;
                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hey Pal! Heard your business has been thriving. I'm writing to ask you whether you would like to invest in one business project. You will receive a good margin of the profits!":
                    {
                        Debug.Log("2 Investment");
                        GM.happiness += Random.Range(1f, 3f);
                        GM.money += Random.Range(1f, 3f);
                        GM.popularity += Random.Range(1f, 3f);

                        GM.happinessSlider.value += GM.happiness;
                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hi. Would you like to provide some funds for my start-up business? We will pay you handsomely once things start to pick up.":
                    {
                        Debug.Log("3 Investment");
                        GM.happiness += Random.Range(0.5f, 2f);
                        GM.money += Random.Range(0.5f, 2f);
                        GM.popularity += Random.Range(0.5f, 2f);

                        GM.happinessSlider.value += GM.happiness;
                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hi, I am a representative of an Energy Company called Operate Clean Energy. We believe our proposal for a mutually beneficial partnership will revolutionize the way we harness and distribute energy. Would you like to invest in our company?":
                    {
                        Debug.Log("4 Investment");
                        GM.happiness += Random.Range(0.5f, 2f);
                        GM.money += Random.Range(0.5f, 2f);
                        GM.popularity += Random.Range(0.5f, 2f);

                        GM.happinessSlider.value += GM.happiness;
                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hey there! I'm the developer of Among Us, and I could really use some financial support to help me develop this game! Would you help me?":
                    {
                        Debug.Log("5 Investment");
                        GM.happiness += Random.Range(0.5f, 2f);
                        GM.money -= Random.Range(0.5f, 2f);
                        GM.popularity += Random.Range(0.5f, 2f);

                        GM.happinessSlider.value += GM.happiness;
                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hi! I would like to provide an upgrade of ads to your company! Do you want some traction for your ads?":
                    {
                        Debug.Log("6 Investment");
                        GM.money -= Random.Range(0.5f, 2f);
                        GM.popularity += Random.Range(0.5f, 2f);
                        GM.happiness += Random.Range(0.5f, 2f);

                        GM.happinessSlider.value += GM.happiness;
                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hey there! Want to be the face of our awesome brand? We're hiring models to help us advertise - interested?":
                    {
                        Debug.Log("7 Investment");
                        GM.money -= Random.Range(0.5f, 2f);
                        GM.popularity += Random.Range(0.5f, 2f);

                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hi! I am a representative of a clothing brand called Doo Nut, we would be thrilled to offer you a deal with our clothing brand - are you interested?":
                    {
                        Debug.Log("8 Investment");
                        GM.money -= Random.Range(0.5f, 2f);
                        GM.popularity += Random.Range(0.5f, 2f);

                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hi, I would like to catch everyone's attention and spread your company's name by advertising it on a billboard! Are you interested?":
                    {
                        Debug.Log("9 Investment");
                        GM.money -= Random.Range(0.5f, 2f);
                        GM.popularity += Random.Range(0.5f, 2f);

                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hi, I would like to provide services to upgrade your company's office area, are you interested?":
                    {
                        Debug.Log("10 Investment");
                        GM.happiness += Random.Range(0.5f, 2f);
                        GM.money -= Random.Range(0.5f, 2f);
                        GM.popularity += Random.Range(0.5f, 2f);

                        GM.happinessSlider.value += GM.happiness;
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

            DestroyObject();
        }

        GM.FunctionUpdates();
    }

    public void NoClick()
    {
        //if randomObject tag equals to "scam"
        if (randomObject.tag == "scam")
        {
            //investmentScenario will set active to false
            investmentScenario.SetActive(false);


            DestroyObject();
        }


        else if (randomObject.tag == "investment")
        {
            //investmentScenario will set active to false
            investmentScenario.SetActive(false);


            DestroyObject();
        }

        GM.FunctionUpdates();
    }
}
