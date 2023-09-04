using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MG_Dialogue : MonoBehaviour
{
    //Reference script to mgTimer
    public MiniGameTimer mgTimer;

    //Reference script to GM
    public GameManager GM;

    //Reference script to DM
    public DialogueManager DM;

    //private GameObject for miniGameObject
    private GameObject miniGameObject;

    //a List to set how many personas to randomize
    public List<GameObject> spawnObjects;
    public GameObject spawnArea;

    public Animator transitionAnim;

    //spawnArea for player model
    private GameObject randomObject;
    private GameObject spawned;

    //reference GameObject for miniGameScenario
    public GameObject miniGameScenario;

    //Button for interaction of scenario
    public Button scenarioButton;

    //GameObjects for scenario
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject miniGameDialogue;
    public GameObject nameBox;

    public bool isDoodleGame;

    public float transitionTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        isDoodleGame = false;

        //referencing GameManager GM's employee's button and notification icon to false
        GM.miniGameNotiIcon.SetActive(false);
        GM.miniGameButton.SetActive(false);

        //set button enabled to false
        enabled = false;

        //set scenario, button and dialogue to false
        miniGameScenario.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        miniGameDialogue.SetActive(false);
        nameBox.SetActive(false);
    }

    //in SpawnObject, there will be an randomRange that detects how many objects are in the array spawnObjects
    //then, a new vector3 spawnPosition equals to spawnArea's position
    //a randomObject will take the current index of spawnObjects' array
    //which then spawned will instantiate the randomObject chosen on the current spawnPosition
    //spawned will then be set under a parent object's position
    public void SpawnObject()
    {
        Debug.Log("Spawned Mini Game");
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
        Debug.Log("Spawned Scenario Mini Game");
        miniGameScenario.SetActive(true);
        nameBox.SetActive(true);

        isDoodleGame = true;

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
        miniGameDialogue.SetActive(true);
        bool isActive = miniGameDialogue.activeSelf;
        Debug.Log("Mini Game AnimDialogue is " + isActive);
    }

    public void DoodleJumpScenario()
    {
        StartCoroutine(TransitionDoodleLevel());
    }

    IEnumerator TransitionDoodleLevel()
    {
        transitionAnim.SetTrigger("Start");

        GM.currentHappiness = GM.happiness;
        GM.currentMoney = GM.money;
        GM.currentPopularity = GM.popularity;

        GM.elapsedTime = GM.animationDur;

        yield return new WaitForSeconds(transitionTime);

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().levelAudioChange.Stop();
        GameObject.FindGameObjectWithTag("main_game").SetActive(false);
        SceneManager.LoadScene("DoodleJump");
    }

    //In DestroyObject, holidayObject GameObject will find tag of any GameObject tagged "holiday"
    //if object is then tagged "holiday", destroy holidayObject if it's active
    public void DestroyObject()
    {
        miniGameObject = GameObject.FindGameObjectWithTag("good");

        if (randomObject.tag == "good")
        {
            Destroy(miniGameObject);
        }
    }

    public void YesClick()
    {
        //if randomObject tag equals to "good"
        if (randomObject.tag == "good")
        {
            int index = 0;

            miniGameScenario.SetActive(false);

            DestroyObject();

            switch (DM.miniGameLines[index])
            {
                case "Mini-Game\n \nHello Boss,\nI highly recommend taking a break to try out the new mini game - it's a great opportunity to relax and boost team morale.":
                    {
                        if(mgTimer.IsCooldownActive != true)
                        {
                            if (isDoodleGame == true)
                            {
                                Debug.Log(isDoodleGame);
                                DoodleJumpScenario();
                            }
                        }
                        else
                        {
                            StartCoroutine(Cooldown(3));
                        }

                        break;
                    }
                default:
                    {
                        Debug.Log("Default case or unrecognized dialogue.");
                        break;
                    }
            }

            index++;
        }

        GM.slotGameButton.enabled = true;
        GM.snekGameButton.enabled = true;
        GM.doNutButton.enabled = true;
        GM.FunctionUpdates();

        isDoodleGame = false;
    }

    public void NoClick()
    {
        //if randomObject tag equals to "good"
        if (randomObject.tag == "good")
        {
            int index = 0;

            miniGameScenario.SetActive(false);

            DestroyObject();

            switch (DM.miniGameLines[index])
            {
                case "Mini-Game\n \nHello Boss,\nI highly recommend taking a break to try out the new mini game - it's a great opportunity to relax and boost team morale.":
                    {
                        Debug.Log("No Doodle Game");

                        break;
                    }
                default:
                    {
                        Debug.Log("Default case or unrecognized dialogue.");
                        break;
                    }
            }

            index++;
        }

        GM.slotGameButton.enabled = true;
        GM.snekGameButton.enabled = true;
        GM.doNutButton.enabled = true;
        GM.FunctionUpdates();

        isDoodleGame = false;
    }

    IEnumerator Cooldown(float timer)
    {
        GM.cooldownNoti.SetActive(true);
        GM.cooldownBrowser.SetActive(true);
        GM.cooldownPanel.SetActive(true);

        yield return new WaitForSeconds(3);

        GM.cooldownNoti.SetActive(false);
        GM.cooldownBrowser.SetActive(false);
        GM.cooldownPanel.SetActive(false);
    }
}
