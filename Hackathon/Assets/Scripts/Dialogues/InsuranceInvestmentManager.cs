using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsuranceInvestmentManager : MonoBehaviour
{
    //Reference script to GM
    public GameManager GM;

    //Reference script to DM
    public DialogueManager DM;

    //Reference script to browserManager
    public Browser browserManager;

    //private GameObject for investmentObject
    private GameObject insuranceObject;
    private GameObject randomObject;
    private GameObject spawned;

    //a List to set how many personas to randomize
    public List<GameObject> spawnObjects;

    //spawnArea for player model
    public GameObject spawnArea;

    //reference GameObject for insuranceScenario
    public GameObject insuranceInvestmentScenario;

    //Button for interaction of scenario
    public Button scenarioButton;

    //GameObjects for scenario
    public GameObject yesButton;
    public GameObject yesDisabledBtn;
    public GameObject noButton;
    public GameObject insuranceInvestmentDialogue;
    public GameObject nameBox;

    // Start is called before the first frame update
    void Start()
    {
        //referencing GameManager GM's insurance button and notification icon to false
        GM.investmentNotiIcon.SetActive(false);
        GM.investmentButton.SetActive(false);

        //set button enabled to false
        enabled = false;

        //set scenario, button and dialogue to false
        insuranceInvestmentScenario.SetActive(false);
        yesButton.SetActive(false);
        yesDisabledBtn.SetActive(false);
        noButton.SetActive(false);
        insuranceInvestmentDialogue.SetActive(false);
        nameBox.SetActive(false);
    }

    void Update()
    {

    }

    //in SpawnObject, there will be an randomRange that detects how many objects are in the array spawnObjects
    //then, a new vector3 spawnPosition equals to spawnArea's position
    //a randomObject will take the current index of spawnObjects' array
    //which then spawned will instantiate the randomObject chosen on the current spawnPosition
    //spawned will then be set under a parent object's position=
    public void SpawnObject()
    {
        Debug.Log("Accident Insurance Spawned");
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

        insuranceInvestmentScenario.SetActive(true);
        nameBox.SetActive(true);

        scenarioButton.enabled = false;
        Debug.Log(scenarioButton.enabled);

        StartCoroutine(AnimationPlay(0.5f));
    }

    //In AnimationPlay, it will return a float of seconds and set yes, no and investmentDialogue set active to true
    IEnumerator AnimationPlay(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (browserManager.investmentInsurance != true)
        {
            yesButton.SetActive(true);
            yesDisabledBtn.SetActive(false);
        }
        else
        {
            yesDisabledBtn.SetActive(true);
            yesButton.SetActive(false);
        }

        noButton.SetActive(true);
        bool isActive = insuranceInvestmentDialogue.activeSelf;
        Debug.Log("Insurance Accident AnimDialogue is " + isActive);
        insuranceInvestmentDialogue.SetActive(true);
    }

    //In DestroyObject, insuranceObject GameObject will find tag of any GameObject tagged "insurance"
    //if object is then tagged "insurance", destroy insuranceObject if it's active
    public void DestroyObject()
    {
        insuranceObject = GameObject.FindGameObjectWithTag("insurance");

        if (randomObject.tag == "insurance")
        {
            Destroy(insuranceObject);
        }
    }

    public void YesClick()
    {
        GM.currentMoney = GM.money;

        //if randomObject tag equals to "insurance"
        if (randomObject.tag == "insurance")
        {
            int index = 0;

            insuranceInvestmentScenario.SetActive(false);

            switch (DM.insuranceInvestmentLines[index])
            {
                case "Investment Insurance Oppurtunity\n \nGood Morning Boss!,\nWould you like to buy an Investment Insurance at a lower rate to keep yourself safe from scams?":
                    {
                        if (browserManager.investmentInsurance != true)
                        {
                            StoreGame.insuranceAchCount += 1;
                            Debug.Log(StoreGame.insuranceAchCount);

                            Debug.Log("Bought Investment Insurance");
                            GM.money -= 3f;
                            browserManager.investmentInsurance = true;

                            StartCoroutine(MinusMoneyTransition(3));

                            browserManager.investmentBtnDisabled.SetActive(true);

                            browserManager.insuranceGreyed.SetActive(false);
                            browserManager.insuranceActive.SetActive(true);
                        }
                        else
                        {
                            Debug.Log("You are already covered with Investment insurance");
                            browserManager.investmentBtnDisabled.SetActive(false);
                        }

                        break;
                    }
                default:
                    {
                        Debug.Log("Default case or unrecognized dialogue.");
                        break;
                    }
            }

            GM.moneySlider.value = GM.money;

            GM.StartCoroutine(GM.AnimateMoneySlider());

            index++;

            DestroyObject();
        }

        GM.slotGameButton.enabled = true;
        GM.snekGameButton.enabled = true;
        GM.doNutButton.enabled = true;
        GM.FunctionUpdates();
    }

    public void NoClick()
    {
        GM.currentMoney = GM.money;

        //if randomObject tag equals to "insurance"
        if (randomObject.tag == "insurance")
        {
            insuranceInvestmentScenario.SetActive(false);

            GM.money += 5f;

            StartCoroutine(PlusMoneyTransition(3));

            GM.moneySlider.value = GM.money;

            GM.StartCoroutine(GM.AnimateMoneySlider());

            DestroyObject();
        }

        GM.slotGameButton.enabled = true;
        GM.snekGameButton.enabled = true;
        GM.doNutButton.enabled = true;
        GM.FunctionUpdates();
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
}
