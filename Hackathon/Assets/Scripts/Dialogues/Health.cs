using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //Reference script to GM
    public GameManager GM;

    //Reference script to browserManager
    public Browser browserManager;

    //Reference script to DM
    public DialogueManager DM;

    //private GameObject for holidayObject
    private GameObject healthObject;

    //a List to set how many personas to randomize
    public List<GameObject> spawnObjects;
    public GameObject spawnArea;

    //spawnArea for player model
    private GameObject randomObject;
    private GameObject spawned;

    //reference GameObject for holidayScenario
    public GameObject healthScenario;

    //Button for interaction of scenario
    public Button scenarioButton;

    //GameObjects for scenario
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject healthDialogue;
    public GameObject nameBox;

    // Start is called before the first frame update
    void Start()
    {
        //referencing GameManager GM's employee's button and notification icon to false
        GM.healthNotiIcon.SetActive(false);
        GM.healthButton.SetActive(false);

        //set button enabled to false
        enabled = false;

        //set scenario, button and dialogue to false
        healthScenario.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        healthDialogue.SetActive(false);
        nameBox.SetActive(false);
    }

    void Update()
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
        healthScenario.SetActive(true);
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
        healthDialogue.SetActive(true);
        bool isActive = healthDialogue.activeSelf;
        Debug.Log("Holiday AnimDialogue is " + isActive);
    }

    //In DestroyObject, holidayObject GameObject will find tag of any GameObject tagged "holiday"
    //if object is then tagged "holiday", destroy holidayObject if it's active
    public void DestroyObject()
    {
        healthObject = GameObject.FindGameObjectWithTag("holiday");

        if (randomObject.tag == "holiday")
        {
            Destroy(healthObject);
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

            healthScenario.SetActive(false);

            switch (DM.healthLines[index])
            {
                case "Medical Incident\n \nHi Boss,\nOne of our employees has reported sick, would you like to help out with his/her medical fees?":
                    {
                        if (browserManager.healthInsurance != true)
                        {
                            Debug.Log("1 Yes Health");
                            GM.money -= Random.Range(5f, 10f);

                            GM.happiness += Random.Range(4f, 8f);
                            GM.popularity += Random.Range(4f, 8f);

                            StartCoroutine(MinusMoneyTransition(3));

                            StartCoroutine(PlusHappinessTransition(3));
                            StartCoroutine(PlusPopularityTransition(3));

                            browserManager.healthActive.SetActive(false);
                            browserManager.healthGreyed.SetActive(true);
                        }
                        else
                        {
                            GM.popularity += Random.Range(8f, 16f);
                            GM.happiness += Random.Range(8f, 16f);

                            StartCoroutine(PlusHappinessTransition(3));
                            StartCoroutine(PlusPopularityTransition(3));
                        }
                        break;
                    }

                case "Medical Incident\n \nGood Morning Boss,\nOne of our departments has recently got in contact with COVID, would you like to help out by paying for the medical fees?":
                    {
                        if (browserManager.healthInsurance != true)
                        {
                            Debug.Log("2 Yes Health");
                            GM.money -= Random.Range(5f, 10f);

                            GM.popularity += Random.Range(4f, 8f);
                            GM.happiness += Random.Range(4f, 8f);

                            StartCoroutine(MinusMoneyTransition(3));

                            StartCoroutine(PlusHappinessTransition(3));
                            StartCoroutine(PlusPopularityTransition(3));

                            browserManager.healthActive.SetActive(false);
                            browserManager.healthGreyed.SetActive(true);
                        }
                        else
                        {
                            GM.popularity += Random.Range(8f, 16f);
                            GM.happiness += Random.Range(8f, 16f);

                            StartCoroutine(PlusHappinessTransition(3));
                            StartCoroutine(PlusPopularityTransition(3));

                        }
                        break;
                    }

                case "Medical Incident\n \nHi Sir,\nI have submitted a medical bill for reimbursement, can I have an approval to proceed with the payment?":
                    {
                        if (browserManager.healthInsurance != true)
                        {
                            Debug.Log("3 Yes Health");
                            GM.money -= Random.Range(5f, 10f);

                            GM.popularity += Random.Range(4f, 8f);
                            GM.happiness += Random.Range(4f, 8f);

                            StartCoroutine(MinusMoneyTransition(3));

                            StartCoroutine(PlusHappinessTransition(3));
                            StartCoroutine(PlusPopularityTransition(3));

                            browserManager.healthActive.SetActive(false);
                            browserManager.healthGreyed.SetActive(true);
                        }
                        else
                        {
                            GM.popularity += Random.Range(8f, 16f);
                            GM.happiness += Random.Range(8f, 16f);

                            StartCoroutine(PlusHappinessTransition(3));
                            StartCoroutine(PlusPopularityTransition(3));

                        }
                        break;
                    }

                case "Medical Incident\n \nHi Boss,\nthis is urgent! I need your help with some financial help as I can't pay for my medical bills. Please help me!":
                    {
                        if (browserManager.healthInsurance != true)
                        {
                            Debug.Log("3 Yes Health");
                            GM.money -= Random.Range(5f, 10f);

                            GM.popularity += Random.Range(4f, 8f);
                            GM.happiness += Random.Range(4f, 8f);

                            StartCoroutine(MinusMoneyTransition(2));

                            StartCoroutine(PlusHappinessTransition(3));
                            StartCoroutine(PlusPopularityTransition(3));

                            browserManager.healthActive.SetActive(false);
                            browserManager.healthGreyed.SetActive(true);
                        }
                        else
                        {
                            GM.popularity += Random.Range(8f, 16f);
                            GM.happiness += Random.Range(8f, 16f);

                            StartCoroutine(PlusHappinessTransition(3));
                            StartCoroutine(PlusPopularityTransition(3));

                        }
                        break;
                    }

                case "Medical Incident\n \nHello Sir,\nWould you please consider covering the medical fees as it is directly related to my work.":
                    {
                        if (browserManager.healthInsurance != true)
                        {
                            Debug.Log("3 Yes Health");
                            GM.money -= Random.Range(5f, 10f);

                            GM.popularity += Random.Range(4f, 8f);
                            GM.happiness += Random.Range(4f, 8f);

                            StartCoroutine(MinusMoneyTransition(3));

                            StartCoroutine(PlusHappinessTransition(3));
                            StartCoroutine(PlusPopularityTransition(3));

                            browserManager.healthActive.SetActive(false);
                            browserManager.healthGreyed.SetActive(true);
                        }
                        else
                        {
                            GM.popularity += Random.Range(8f, 16f);
                            GM.happiness += Random.Range(8f, 16f);

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

            if (browserManager.healthInsurance == true)
            {
                Debug.Log(browserManager.healthInsurance);
                browserManager.healthInsurance = false;
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

            healthScenario.SetActive(false);

            switch (DM.healthLines[index])
            {
                case "Hi Boss,\nOne of our employees has reported sick, would you like to help out with his/her medical fees?":
                    {
                        Debug.Log(" No Health");

                        GM.money += 5f;

                        GM.popularity -= Random.Range(5f, 10f);
                        GM.happiness -= Random.Range(5f, 15f);

                        StartCoroutine(PlusMoneyTransition(3));

                        StartCoroutine(MinusPopularityTransition(3));
                        StartCoroutine(MinusHappinessTransition(3));

                        break;
                    }
                case "Good Morning Boss,\nOne of our departments has recently got in contact with COVID, would you like to help out by paying for the medical fees?":
                    {
                        Debug.Log("2 No Health");

                        GM.money += 5f;

                        GM.popularity -= Random.Range(5f, 10f);
                        GM.happiness -= Random.Range(5f, 15f);

                        StartCoroutine(PlusMoneyTransition(3));

                        StartCoroutine(MinusPopularityTransition(3));
                        StartCoroutine(MinusHappinessTransition(3));

                        break;
                    }
                case "Hi Sir,\nI have submitted a medical bill for reimbursement, can I have an approval to proceed with the payment?":
                    {
                        Debug.Log("3 No Health");

                        GM.money += 5f;

                        GM.popularity -= Random.Range(5f, 10f);
                        GM.happiness -= Random.Range(5f, 15f);

                        StartCoroutine(PlusMoneyTransition(3));

                        StartCoroutine(MinusPopularityTransition(3));
                        StartCoroutine(MinusHappinessTransition(3));

                        break;
                    }
                case "Hi Boss,\nthis is urgent! I need your help with some financial help as I can't pay for my medical bills. Please help me!":
                    {
                        Debug.Log("4 No Health");

                        GM.money += 5f;

                        GM.popularity -= Random.Range(5f, 10f);
                        GM.happiness -= Random.Range(5f, 15f);

                        StartCoroutine(PlusMoneyTransition(3));

                        StartCoroutine(MinusPopularityTransition(3));
                        StartCoroutine(MinusHappinessTransition(3));

                        break;
                    }
                case "Hello Sir,\nI believe it would be in the best interest of the company, as well as my own well-being, for you to consider covering the medical fees as it is directly related to my work.":
                    {
                        Debug.Log("5 No Health");

                        GM.money += 5f;

                        GM.popularity -= Random.Range(5f, 10f);
                        GM.happiness -= Random.Range(5f, 15f);

                        StartCoroutine(PlusMoneyTransition(3));

                        StartCoroutine(MinusPopularityTransition(3));
                        StartCoroutine(MinusHappinessTransition(3));

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
