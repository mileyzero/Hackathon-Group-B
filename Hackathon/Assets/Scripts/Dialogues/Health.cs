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
        healthDialogue.SetActive(true);
        nameBox.SetActive(false);
    }

    //in SpawnObject, there will be an randomRange that detects how many objects are in the array spawnObjects
    //then, a new vector3 spawnPosition equals to spawnArea's position
    //a randomObject will take the current index of spawnObjects' array
    //which then spawned will instantiate the randomObject chosen on the current spawnPosition
    //spawned will then be set under a parent object's position
    public void SpawnObject()
    {
        Debug.Log("Spawned Health");

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
        Debug.Log("Spawned Scenario Health");
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
        Debug.Log("Health AnimDialogue is " + isActive);
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
            GM.snekGameBtn.enabled = true;

            int index = 0;

            healthScenario.SetActive(false);

            switch (DM.healthLines[index])
            {
                case "Good Morning Boss, One of our employees has reported sick, would you like to help out by paying for his/her medical fees?":
                    {
                        if (browserManager.healthInsurance != true)
                        {
                            Debug.Log("1 Yes Health");
                            GM.money -= Random.Range(10f, 15f);

                            GM.happiness += Random.Range(2.5f, 5f);
                            GM.popularity += Random.Range(2.5f, 7.5f);
                        }
                        else
                        {
                            GM.happiness += Random.Range(5f, 10f);
                            GM.popularity += Random.Range(5f, 15f);
                        }
                        break;
                    }
                case "Hi Boss, Our department has reported multiple sick due to a recent outbreak, please help out by paying for medical fees..":
                    {
                        if (browserManager.healthInsurance != true)
                        {
                            Debug.Log("2 Yes Health");
                            GM.money -= Random.Range(10f, 15f);

                            GM.happiness += Random.Range(2.5f, 5f);
                            GM.popularity += Random.Range(2.5f, 7.5f);
                        }
                        else
                        {
                            GM.happiness += Random.Range(5f, 10f);
                            GM.popularity += Random.Range(5f, 15f);
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


            if (browserManager.healthInsurance == true)
            {
                Debug.Log(browserManager.healthInsurance);

                browserManager.healthActive.SetActive(false);
                browserManager.healthGreyed.SetActive(true);

                browserManager.healthInsurance = false;
            }

            index++;

            DestroyObject();
        }

        GM.FunctionUpdates();
    }

    public void NoClick()
    {
        //if randomObject tag equals to "holiday"
        if (randomObject.tag == "holiday")
        {
            GM.snekGameBtn.enabled = true;

            int index = 0;

            healthScenario.SetActive(false);

            switch (DM.employeeLines[index])
            {
                case "Good Morning Boss, One of our employees has reported sick, would you like to help out by paying for his/her medical fees?":
                    {
                        Debug.Log("1 No Health");
                        GM.happiness -= Random.Range(5f, 10f);
                        GM.popularity -= Random.Range(5f, 15f);

                        break;
                    }
                case "Hi Boss, Our department has reported multiple sick due to a recent outbreak, please help out by paying for medical fees..":
                    {
                        Debug.Log("2 No Health");
                        GM.happiness -= Random.Range(5f, 10f);
                        GM.popularity -= Random.Range(5f, 15f);

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
}
