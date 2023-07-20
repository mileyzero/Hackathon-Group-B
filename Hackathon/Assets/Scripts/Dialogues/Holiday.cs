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

    //bool for checking "Yes" and "No" button
    public bool isYes;
    public bool isNo;

    // Start is called before the first frame update
    void Start()
    {
        //isYes and isNo bool for checking if "Yes" or "No" is pressed
        isYes = false;
        isNo = false;

        //referencing GameManager GM's employee's button and notification icon to false
        GM.employeeNotiIcon.SetActive(false);
        GM.employeeButton.SetActive(false);

        //set button enabled to false
        enabled = false;

        //set scenario, button and dialogue to false
        holidayScenario.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        holidayDialogue.SetActive(false);
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

        holidayScenario.SetActive(true);
        nameBox.SetActive(true);

        isYes = true;
        isNo = true;

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
        holidayDialogue.SetActive(true);
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
        if (isYes == true)
        {
            holidayScenario.SetActive(false);

            foreach (var option in DM.employeeLines)
            {
                switch (option)
                {
                    case "Hey Boss! One of your employees is having their birthday TODAY. Would you like to gift a present?":
                        {
                            GM.happiness += Random.Range(1.5f, 3f);
                            GM.money -= Random.Range(1f, 2.5f);
                            break;
                        }
                    case "Good Morning Boss! One of your senior employees would like to see you about a promotion. Would you like me to send them in to discuss his possible promotion?":
                        {
                            GM.happiness += Random.Range(1f, 4f);
                            GM.money -= Random.Range(2f, 5f);
                            break;
                        }
                    case "Hi Boss, One of our employees has reported sick, would you like to help out by paying for his/her medical fees?":
                        {
                            GM.happiness += Random.Range(2f, 5f);
                            GM.money -= Random.Range(1f, 3f);
                            break;
                        }
                    case "Hi Boss! Good news, one of our employees has been working hard lately, as a token of appreciation, would you like to provide them with holiday money this year?":
                        {
                            GM.happiness += Random.Range(3f, 6f);
                            GM.money -= Random.Range(1f, 4f);
                            GM.popularity += Random.Range(1f, 3f);
                            break;
                        }
                    case "Hi Boss, Bad news, one of our employees has gotten into an accident, would you like to provide them with financial assistance to cover their medical bills?":
                        {
                            GM.happiness += Random.Range(4f, 8f);
                            GM.money -= Random.Range(1f, 4f);
                            GM.popularity += Random.Range(2f, 4f);
                            break;
                        }
                    case "Good Morning Boss! In regards for our employees' workspace, would you like to provide them financial assistance to upgrade?":
                        {
                            GM.happiness += Random.Range(2f, 4f);
                            GM.money -= Random.Range(0.5f, 3f);
                            break;
                        }
                    case "Happy New Year Boss! would you like to host a New Year Party for your employees?":
                        {
                            GM.happiness += Random.Range(3f, 6f);
                            GM.money -= Random.Range(1f, 3f);
                            break;
                        }
                }
            }

            DestroyObject();
        }

        GM.FunctionUpdates();
    }

    public void NoClick()
    {
        if (isNo == true)
        {
            holidayScenario.SetActive(false);

            foreach (var option in DM.employeeLines)
            {
                switch (option)
                {
                    case "Hey Boss! One of your employees is having their birthday TODAY. Would you like to gift a present?":
                        {
                            GM.happiness -= Random.Range(1.5f, 3f);
                            break;
                        }
                    case "Good Morning Boss! One of your senior employees would like to see you about a promotion. Would you like me to send them in to discuss his possible promotion?":
                        {
                            GM.happiness -= Random.Range(1f, 4f);
                            break;
                        }
                    case "Hi Boss, One of our employees has reported sick, would you like to help out by paying for his/her medical fees?":
                        {
                            GM.happiness -= Random.Range(3f, 6f);
                            break;
                        }
                    case "Hi Boss! Good news, one of our employees has been working hard lately, as a token of appreciation, would you like to provide them with holiday money this year?":
                        {
                            GM.happiness -= Random.Range(4f, 8f);
                            GM.popularity -= Random.Range(1f, 3f);
                            break;
                        }
                    case "Hi Boss, Bad news, one of our employees has gotten into an accident, would you like to provide them with financial assistance to cover their medical bills?":
                        {
                            GM.happiness -= Random.Range(4f, 8f);
                            GM.popularity -= Random.Range(2f, 4f);
                            break;
                        }
                    case "Good Morning Boss! In regards for our employees' workspace, would you like to provide them financial assistance to upgrade?":
                        {
                            GM.happiness -= Random.Range(2f, 4f);
                            break;
                        }
                    case "Happy New Year Boss! would you like to host a New Year Party for your employees?":
                        {
                            GM.happiness -= Random.Range(2f, 6f);
                            break;
                        }
                }
            }

            DestroyObject();
        }

        GM.FunctionUpdates();
    }
}
