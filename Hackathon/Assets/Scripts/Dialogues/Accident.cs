using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Accident : MonoBehaviour
{
    //Reference script to GM
    public GameManager GM;

    //Reference script to browserManager
    public Browser browserManager;

    //Reference script to DM
    public DialogueManager DM;

    //private GameObject for holidayObject
    private GameObject accidentObject;

    //a List to set how many personas to randomize
    public List<GameObject> spawnObjects;
    public GameObject spawnArea;

    //spawnArea for player model
    private GameObject randomObject;
    private GameObject spawned;

    //reference GameObject for holidayScenario
    public GameObject accidentScenario;

    //Button for interaction of scenario
    public Button scenarioButton;

    //GameObjects for scenario
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject accidentDialogue;
    public GameObject nameBox;

    // Start is called before the first frame update
    void Start()
    {
        //referencing GameManager GM's employee's button and notification icon to false
        GM.accidentNotiIcon.SetActive(false);
        GM.accidentButton.SetActive(false);

        //set button enabled to false
        enabled = false;

        //set scenario, button and dialogue to false
        accidentScenario.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        accidentDialogue.SetActive(true);
        nameBox.SetActive(false);
    }

    private void Update()
    {
        
    }

    //in SpawnObject, there will be an randomRange that detects how many objects are in the array spawnObjects
    //then, a new vector3 spawnPosition equals to spawnArea's position
    //a randomObject will take the current index of spawnObjects' array
    //which then spawned will instantiate the randomObject chosen on the current spawnPosition
    //spawned will then be set under a parent object's position
    public void SpawnObject()
    {
        Debug.Log("Spawned Holiday");
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
        Debug.Log("Spawned Scenario Holiday");
        accidentScenario.SetActive(true);
        nameBox.SetActive(true);

        scenarioButton.enabled = false;
        Debug.Log(scenarioButton.enabled);

        StartCoroutine(AnimationPlay(0.5f));
    }

    //In AnimationPlay, it will return a float of seconds and set yes, no and investmentDialogue set active to true
    IEnumerator AnimationPlay(float seconds)
    {
        Debug.Log("animation start");
        yield return new WaitForSeconds(seconds);

        yesButton.SetActive(true);
        noButton.SetActive(true);
        accidentDialogue.SetActive(true);
        bool isActive = accidentDialogue.activeSelf;
        Debug.Log("Accident AnimDialogue is " + isActive);
    }

    //In DestroyObject, holidayObject GameObject will find tag of any GameObject tagged "holiday"
    //if object is then tagged "holiday", destroy holidayObject if it's active
    public void DestroyObject()
    {
        accidentObject = GameObject.FindGameObjectWithTag("holiday");

        if (randomObject.tag == "holiday")
        {
            Destroy(accidentObject);
        }
    }

    public void YesClick()
    {
        //if randomObject tag equals to "holiday"
        if (randomObject.tag == "holiday")
        {
            int index = 0;

            GM.currentMoney = GM.money;
            GM.currentPopularity = GM.popularity;
            GM.currentHappiness = GM.happiness;

            accidentScenario.SetActive(false);

            switch (DM.accidentLines[index])
            {
                case "Accident Incident\n \nHi Boss,\nBad news, one of our employees has gotten into an accident, would you like to provide them with financial assistance to cover their medical bills?":
                    {
                        if(browserManager.accidentInsurance != true)
                        {
                            Debug.Log("1 Yes Accident");
                            GM.money -= Random.Range(5f, 10f);
                            GM.happiness += Random.Range(5f, 10f);
                            GM.popularity += Random.Range(5f, 10f);

                            StartCoroutine(MinusMoneyTransition(3));

                            StartCoroutine(PlusHappinessTransition(3));
                            StartCoroutine(PlusPopularityTransition(3));

                            browserManager.accidentActive.SetActive(false);
                            browserManager.accidentGreyed.SetActive(true);
                        }
                        else
                        {
                            GM.happiness += Random.Range(5, 10);
                            GM.popularity += Random.Range(5, 15);

                            StartCoroutine(PlusHappinessTransition(3));
                            StartCoroutine(PlusPopularityTransition(3));

                        }
                        break;
                    }
                case "Accident Incident\n \nGood Morning Boss,\nOne of our department head has recently got into an accident, would you please help out with the medical fees?":
                    {
                        if (browserManager.accidentInsurance != true)
                        {
                            Debug.Log("2 Yes Accident");
                            GM.money -= Random.Range(5f, 10f);

                            GM.happiness += Random.Range(5f, 10f);
                            GM.popularity += Random.Range(5f, 10f);

                            StartCoroutine(MinusMoneyTransition(3));

                            StartCoroutine(PlusHappinessTransition(3));
                            StartCoroutine(PlusPopularityTransition(3));

                            browserManager.accidentActive.SetActive(false);
                            browserManager.accidentGreyed.SetActive(true);
                        }
                        else
                        {
                            GM.happiness += Random.Range(8f, 15f);
                            GM.popularity += Random.Range(8f, 15f);

                            StartCoroutine(PlusHappinessTransition(3));
                            StartCoroutine(PlusPopularityTransition(3));

                        }
                        break;
                    }
                case "Accident Incident\n \nHi Boss,\nthis is one of your employees from one of the departements, my family have been struggling with some financial stuff, can you help us?":
                    {
                        if (browserManager.accidentInsurance != true)
                        {
                            Debug.Log("3 Yes Accident");
                            GM.money -= Random.Range(5f, 10f);

                            GM.happiness += Random.Range(5f, 10f);
                            GM.popularity += Random.Range(5f, 10f);

                            StartCoroutine(MinusMoneyTransition(3));

                            StartCoroutine(PlusHappinessTransition(3));
                            StartCoroutine(PlusPopularityTransition(3));

                            browserManager.accidentActive.SetActive(false);
                            browserManager.accidentGreyed.SetActive(true);
                        }
                        else
                        {
                            GM.happiness += Random.Range(8f, 15f);
                            GM.popularity += Random.Range(8f, 15f);

                            StartCoroutine(PlusHappinessTransition(3));
                            StartCoroutine(PlusPopularityTransition(3));

                        }
                        break;
                    }
                case "Accident Incident\n \nHi Sir,\nI have submitted an accident bill for reimbursement, can I have an approval to proceed with the payment?":
                    {
                        if (browserManager.accidentInsurance != true)
                        {
                            Debug.Log("4 Yes Accident");
                            GM.money -= Random.Range(5f, 10f);

                            GM.happiness += Random.Range(5f, 10f);
                            GM.popularity += Random.Range(5f, 10f);

                            StartCoroutine(MinusMoneyTransition(3));

                            StartCoroutine(PlusHappinessTransition(3));
                            StartCoroutine(PlusPopularityTransition(3));

                            browserManager.accidentActive.SetActive(false);
                            browserManager.accidentGreyed.SetActive(true);
                        }
                        else
                        {
                            GM.happiness += Random.Range(8f, 15f);
                            GM.popularity += Random.Range(8f, 15f);

                            StartCoroutine(PlusHappinessTransition(3));
                            StartCoroutine(PlusPopularityTransition(3));
                        }
                        break;
                    }
                case "Accident Incident\n \nHello Boss,\nI appreciate your understanding in the matter, but I was wondering if the company could possibly cover the accident fees?":
                    {
                        if (browserManager.accidentInsurance != true)
                        {
                            Debug.Log("5 Yes Accident");
                            GM.money -= Random.Range(5f, 10f);

                            GM.happiness += Random.Range(5f, 10f);
                            GM.popularity += Random.Range(5f, 10f);

                            StartCoroutine(MinusMoneyTransition(3));

                            StartCoroutine(PlusHappinessTransition(3));
                            StartCoroutine(PlusPopularityTransition(3));

                            browserManager.accidentActive.SetActive(false);
                            browserManager.accidentGreyed.SetActive(true);
                        }
                        else
                        {
                            GM.happiness += Random.Range(8f, 15f);
                            GM.popularity += Random.Range(8f, 15f);

                            StartCoroutine(PlusHappinessTransition(3));
                            StartCoroutine(PlusPopularityTransition(3));
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

            GM.StartCoroutine(GM.AnimateMoneySlider());
            GM.StartCoroutine(GM.AnimatePopularitySlider());
            GM.StartCoroutine(GM.AnimateHappinessSlider());
            
            index++;

            if (browserManager.accidentInsurance == true)
            {
                Debug.Log(browserManager.healthInsurance);

                browserManager.accidentInsurance = false;
            }

            DestroyObject();
        }

        GM.slotGameButton.enabled = true;
        GM.snekGameButton.enabled = true;
        GM.doNutButton.enabled = true;
        GM.FunctionUpdates();
    }

    public void NoClick()
    {
        //if randomObject tag equals to "holiday"
        if (randomObject.tag == "holiday")
        {
            int index = 0;

            GM.currentMoney = GM.money;
            GM.currentPopularity = GM.popularity;
            GM.currentHappiness = GM.happiness;

            accidentScenario.SetActive(false);

            switch (DM.accidentLines[index])
            {
                case "Accident Incident\n \nHi Boss,\nBad news, one of our employees has gotten into an accident, would you like to provide them with financial assistance to cover their medical bills?":
                    {
                        Debug.Log("1 No Accident");

                        GM.money += 3f;

                        GM.happiness -= Random.Range(4f, 8f);
                        GM.popularity -= Random.Range(4f, 8f);

                        StartCoroutine(PlusMoneyTransition(3));

                        StartCoroutine(MinusHappinessTransition(3));
                        StartCoroutine(MinusPopularityTransition(3));

                        break;
                    }
                case "Accident Incident\n \nGood Morning Boss,\nOne of our department head has recently got into an accident, would you please help out with the medical fees?":
                    {
                        Debug.Log("2 No Accident");

                        GM.money += 3f;

                        GM.happiness -= Random.Range(4f, 8f);
                        GM.popularity -= Random.Range(4f, 8f);

                        StartCoroutine(PlusMoneyTransition(3));

                        StartCoroutine(MinusHappinessTransition(3));
                        StartCoroutine(MinusPopularityTransition(3));

                        break;
                    }
                case "Accident Incident\n \nHi Boss,\nthis is one of your employees from one of the departements, my family have been struggling with some financial stuff, can you help us?":
                    {
                        Debug.Log("3 No Accident");

                        GM.money += 3f;

                        GM.happiness -= Random.Range(4f, 8f);
                        GM.popularity -= Random.Range(4f, 8f);

                        StartCoroutine(PlusMoneyTransition(3));

                        StartCoroutine(MinusHappinessTransition(3));
                        StartCoroutine(MinusPopularityTransition(3));

                        break;
                    }
                case "Accident Incident\n \nHi Sir,\nI have submitted an accident bill for reimbursement, can I have an approval to proceed with the payment?":
                    {
                        Debug.Log("4 No Accident");
                        GM.money += 3f;

                        GM.happiness -= Random.Range(4f, 8f);
                        GM.popularity -= Random.Range(4f, 8f);

                        StartCoroutine(PlusMoneyTransition(3));

                        StartCoroutine(MinusHappinessTransition(3));
                        StartCoroutine(MinusPopularityTransition(3));

                        break;
                    }
                case "Accident Incident\n \nHello Boss,\nI appreciate your understanding in the matter, but I was wondering if the company could possibly cover the accident fees?":
                    {
                        Debug.Log("5 No Accident");
                        GM.money += 3f;

                        GM.happiness -= Random.Range(4f, 8f);
                        GM.popularity -= Random.Range(4f, 8f);

                        StartCoroutine(PlusMoneyTransition(3));

                        StartCoroutine(MinusHappinessTransition(3));
                        StartCoroutine(MinusPopularityTransition(3));

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

            GM.StartCoroutine(GM.AnimateMoneySlider());
            GM.StartCoroutine(GM.AnimatePopularitySlider());
            GM.StartCoroutine(GM.AnimateHappinessSlider());

            index++;

            DestroyObject();
        }

        GM.slotGameButton.enabled = true;
        GM.snekGameButton.enabled = true;
        GM.doNutButton.enabled = true;
        GM.FunctionUpdates();
    }

    IEnumerator PlusHappinessTransition(float timer)
    {
        GM.plusHappiness.SetActive(true);

        yield return new WaitForSeconds(3);

        GM.plusHappiness.SetActive(false);
    }

    IEnumerator PlusPopularityTransition(float timer)
    {
        GM.plusPopularity.SetActive(true);

        yield return new WaitForSeconds(3);

        GM.plusPopularity.SetActive(false);
    }

    IEnumerator MinusMoneyTransition(float timer)
    {
        GM.minusMoney.SetActive(true);

        yield return new WaitForSeconds(3);

        GM.minusMoney.SetActive(false);
    }

    IEnumerator PlusMoneyTransition(float timer)
    {
        GM.plusMoney.SetActive(true);

        yield return new WaitForSeconds(3);

        GM.plusMoney.SetActive(false);
    }

    IEnumerator MinusHappinessTransition(float timer)
    {
        GM.minusHappiness.SetActive(true);

        yield return new WaitForSeconds(3);

        GM.minusHappiness.SetActive(false);
    }

    IEnumerator MinusPopularityTransition(float timer)
    {
        GM.minusPopularity.SetActive(true);

        yield return new WaitForSeconds(3);

        GM.minusPopularity.SetActive(false);
    }
}
