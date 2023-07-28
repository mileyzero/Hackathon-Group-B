using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Insurance : MonoBehaviour
{
    //Reference script to GM
    public GameManager GM;

    //Reference script to DM
    public DialogueManager DM;

    //private GameObject for investmentObject
    private GameObject insuranceObject;
    private GameObject randomObject;
    private GameObject spawned;

    //a List to set how many personas to randomize
    public List<GameObject> spawnObjects;

    //spawnArea for player model
    public GameObject spawnArea;

    //reference GameObject for insuranceScenario
    public GameObject insuranceScenario;

    //Button for interaction of scenario
    public Button scenarioButton;

    //GameObjects for scenario
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject insuranceDialogue;
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
        insuranceScenario.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        insuranceDialogue.SetActive(false);
        nameBox.SetActive(false);
    }

    //in SpawnObject, there will be an randomRange that detects how many objects are in the array spawnObjects
    //then, a new vector3 spawnPosition equals to spawnArea's position
    //a randomObject will take the current index of spawnObjects' array
    //which then spawned will instantiate the randomObject chosen on the current spawnPosition
    //spawned will then be set under a parent object's position=
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

        insuranceScenario.SetActive(true);
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
        bool isActive = insuranceDialogue.activeSelf;
        Debug.Log("Insurance AnimDialogue is " + isActive);
        insuranceDialogue.SetActive(true);
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
        //if randomObject tag equals to "insurance"
        if (randomObject.tag == "insurance")
        {
            int index = 0;

            insuranceScenario.SetActive(false);

            switch (DM.insuranceLines[index])
            {
                case "BOSS! I received a call regarding one of your warehouse! An accident occurred, which caused a fire. They said not many of the items are damaged, do you want to repair the warehouse?":
                    {
                        Debug.Log("1");
                        GM.money -= Random.Range(10f, 20f);

                        GM.moneySlider.value += GM.money;
                        break;
                    }
                case "Hi Boss! I received a message from employee claiming to be under our company, said that his/her car has broken down and is in need of money, do you want to provide him/her any financial assistance?":
                    {
                        Debug.Log("2");
                        GM.money -= Random.Range(10f, 20f);
                        GM.popularity += Random.Range(3f, 10f);

                        GM.moneySlider.value += GM.money;
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
        //if randomObject tag equals to "insurance"
        if (randomObject.tag == "insurance")
        {
            insuranceScenario.SetActive(false);

            
            DestroyObject();
        }


        GM.FunctionUpdates();
    }
}
