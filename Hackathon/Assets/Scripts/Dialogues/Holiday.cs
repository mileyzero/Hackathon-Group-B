using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Holiday : MonoBehaviour
{
    //Reference script to GM
    public GameManager GM;

    //Reference script to DM
    public DialogueManager DM;

    //private GameObject for holidayObject
    private GameObject holidayObject;

    //a List to set how many personas to randomize
    public List<GameObject> spawnObjects;
    public GameObject spawnArea;

    //spawnArea for player model
    private GameObject randomObject;
    private GameObject spawned;

    //reference GameObject for holidayScenario
    public GameObject holidayScenario;

    //Button for interaction of scenario
    public Button scenarioButton;

    //GameObjects for scenario
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject holidayDialogue;
    public GameObject nameBox;

    // Start is called before the first frame update
    void Start()
    {
        //referencing GameManager GM's employee's button and notification icon to false
        GM.employeeNotiIcon.SetActive(false);
        GM.employeeButton.SetActive(false);

        //set button enabled to false
        enabled = false;

        //set scenario, button and dialogue to false
        holidayScenario.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        holidayDialogue.SetActive(true);
        nameBox.SetActive(false);
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
        holidayScenario.SetActive(true);
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
        holidayDialogue.SetActive(true);
        bool isActive = holidayDialogue.activeSelf;
        Debug.Log("Holiday AnimDialogue is " + isActive);
    }

    //In DestroyObject, holidayObject GameObject will find tag of any GameObject tagged "holiday"
    //if object is then tagged "holiday", destroy holidayObject if it's active
    public void DestroyObject()
    {
        holidayObject = GameObject.FindGameObjectWithTag("holiday");

        if (randomObject.tag == "holiday")
        {
            Destroy(holidayObject);
        }
    }

    public void YesClick()
    {
        //if randomObject tag equals to "holiday"
        if (randomObject.tag == "holiday")
        {
            int index = 0;

            holidayScenario.SetActive(false);

            switch (DM.employeeLines[index])
            {
                case "Hey Boss! One of your employees is having their birthday TODAY. Would you like to gift a present?":
                    {
                        Debug.Log("1 Yes Holiday");
                        GM.happiness += Random.Range(3.5f, 9f);
                        GM.money -= Random.Range(5f, 10f);

                        GM.happinessSlider.value += GM.happiness;
                        GM.moneySlider.value += GM.money;
                        break;
                    }
                case "Good Morning Boss! One of your senior employees would like to see you about a promotion. Would you like me to send them in to discuss his possible promotion?":
                    {
                        Debug.Log("2 Yes Holiday");
                        GM.happiness += Random.Range(5f, 7f);
                        GM.money -= Random.Range(2f, 8f);

                        GM.happinessSlider.value += GM.happiness;
                        GM.moneySlider.value += GM.money;
                        break;
                    }
                case "Hi Boss, One of our employees has reported sick, would you like to help out by paying for his/her medical fees?":
                    {
                        Debug.Log("3 Yes Holiday");
                        GM.happiness += Random.Range(5f, 15f);
                        GM.money -= Random.Range(2f, 7f);

                        GM.happinessSlider.value += GM.happiness;
                        GM.moneySlider.value += GM.money;
                        break;
                    }
                case "Hi Boss! Good news, one of our employees has been working hard lately, as a token of appreciation, would you like to provide them with holiday money this year?":
                    {
                        Debug.Log("4 Yes Holiday");
                        GM.happiness += Random.Range(4f, 9.5f);
                        GM.money -= Random.Range(2f, 6f);
                        GM.popularity += Random.Range(2.5f, 6f);

                        GM.happinessSlider.value += GM.happiness;
                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hi Boss, Bad news, one of our employees has gotten into an accident, would you like to provide them with financial assistance to cover their medical bills?":
                    {
                        Debug.Log("5 Yes Holiday");
                        GM.happiness += Random.Range(4f, 8f);
                        GM.money -= Random.Range(2f, 5f);
                        GM.popularity += Random.Range(5f, 9f);

                        GM.happinessSlider.value += GM.happiness;
                        GM.moneySlider.value += GM.money;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Good Morning Boss! In regards for our employees' workspace, would you like to provide them financial assistance to upgrade?":
                    {
                        Debug.Log("6 Yes Holiday");
                        GM.happiness += Random.Range(5f, 15f);
                        GM.money -= Random.Range(3f, 6f);

                        GM.happinessSlider.value += GM.happiness;
                        GM.moneySlider.value += GM.money;
                        break;
                    }
                case "Happy New Year Boss! would you like to host a New Year Party for your employees?":
                    {
                        Debug.Log("7 Yes Holiday");
                        GM.happiness += Random.Range(5f, 10f);
                        GM.money -= Random.Range(4f, 8f);

                        GM.happinessSlider.value += GM.happiness;
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
        //if randomObject tag equals to "holiday"
        if (randomObject.tag == "holiday")
        {
            int index = 0;

            holidayScenario.SetActive(false);

            switch (DM.employeeLines[index])
            {
                case "Hey Boss! One of your employees is having their birthday TODAY. Would you like to gift a present?":
                    {
                        Debug.Log("1 No Holiday");
                        GM.happiness -= Random.Range(3.5f, 9f);

                        GM.happinessSlider.value += GM.happiness;
                        break;
                    }
                case "Good Morning Boss! One of your senior employees would like to see you about a promotion. Would you like me to send them in to discuss his possible promotion?":
                    {
                        Debug.Log("2 No Holiday");
                        GM.happiness -= Random.Range(5f, 7f);

                        GM.happinessSlider.value += GM.happiness;
                        break;
                    }
                case "Hi Boss, One of our employees has reported sick, would you like to help out by paying for his/her medical fees?":
                    {
                        Debug.Log("3 No Holiday");
                        GM.happiness -= Random.Range(5f, 15f);

                        GM.happinessSlider.value += GM.happiness;
                        break;
                    }
                case "Hi Boss! Good news, one of our employees has been working hard lately, as a token of appreciation, would you like to provide them with holiday money this year?":
                    {
                        Debug.Log("4 No Holiday");
                        GM.happiness -= Random.Range(4f, 9.5f);
                        GM.popularity -= Random.Range(2.5f, 6f);

                        GM.happinessSlider.value += GM.happiness;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Hi Boss, Bad news, one of our employees has gotten into an accident, would you like to provide them with financial assistance to cover their medical bills?":
                    {
                        Debug.Log("5 No Holiday");
                        GM.happiness -= Random.Range(4f, 8f);
                        GM.popularity -= Random.Range(5f, 9f);

                        GM.happinessSlider.value += GM.happiness;
                        GM.popularitySlider.value += GM.popularity;
                        break;
                    }
                case "Good Morning Boss! In regards for our employees' workspace, would you like to provide them financial assistance to upgrade?":
                    {
                        Debug.Log("6 No Holiday");
                        GM.happiness -= Random.Range(5f, 15f);

                        GM.happinessSlider.value += GM.happiness;
                        break;
                    }
                case "Happy New Year Boss! would you like to host a New Year Party for your employees?":
                    {
                        Debug.Log("7 No Holiday");
                        GM.happiness -= Random.Range(5f, 10f);

                        GM.happinessSlider.value += GM.happiness;
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