using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MG_Golf_Dialogue : MonoBehaviour
{
    //Reference script to mgTimer
    public MiniGameTimer mgTimer;

    //Reference script to GM
    public GameManager GM;

    //Reference script to DM
    public DialogueManager DM;

    //private GameObject for miniGameObject
    private GameObject miniGameObject;

    public Animator transitionAnim;

    //a List to set how many personas to randomize
    public List<GameObject> spawnObjects;
    public GameObject spawnArea;

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

    public bool isGolfGame;

    public float transitionTime = 1f;

    public int checkInitialize;

    // Start is called before the first frame update
    void Start()
    {
        isGolfGame = false;

        //referencing GameManager GM's employee's button and notification icon to false
        GM.miniGameGolfNotiIcon.SetActive(false);
        GM.miniGameGolfButton.SetActive(false);

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
        Debug.Log("Spawned Mini Golf Game");
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
        Debug.Log("Spawned Scenario Mini Golf Game");
        miniGameScenario.SetActive(true);
        nameBox.SetActive(true);

        isGolfGame = true;

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
        Debug.Log("Mini Golf Game AnimDialogue is " + isActive);
    }

    //ScenarioTransition only occurs during Investment scenarios, there will be two int that will randomize between two values of minValue and maxValue.
    //If randomVal1 is more than 1 and less than 5, randomVal2 is more than 1 and less than 5, it hides the main game and load the 'Car' mini game
    //If randomVal1 is more than 5 and less than 10, randomVal2 is more than 5 and less than 10, it hides the main game and load the 'DoodleJump' mini game
    //If randomVal1 is more than 10 and less than 15, randomVal2 is more than 10 and less than 15, it hides the main game and load the 'SlotMachine' mini game
    //If randomVal1 is more than 15 and less than 20, randomVal2 is more than 15 and less than 20, it hides the main game and load the 'Golf' mini game
    public void GolfGameScenario()
    {
        checkInitialize = Random.Range(0, 5);
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().levelAudioChange.Stop();

        if (checkInitialize == 0)
        {
            Debug.Log(checkInitialize);

            StartCoroutine(TransitionGolfLevel1());
        }

        else if(checkInitialize == 1)
        {
            Debug.Log(checkInitialize);

            StartCoroutine(TransitionGolfLevel2());
        }
        else if (checkInitialize == 2)
        {
            Debug.Log(checkInitialize);

            StartCoroutine(TransitionGolfLevel3());
        }
        else if (checkInitialize == 3)
        {
            Debug.Log(checkInitialize);

            StartCoroutine(TransitionGolfLevel4());
        }
        else if (checkInitialize == 4)
        {
            Debug.Log(checkInitialize);

            StartCoroutine(TransitionGolfLevel5());
        }
        else if (checkInitialize == 5)
        {
            Debug.Log(checkInitialize);

            StartCoroutine(TransitionGolfLevel6());
        }
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

    IEnumerator TransitionGolfLevel1()
    {
        transitionAnim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        GameObject.FindGameObjectWithTag("main_game").SetActive(false);
        SceneManager.LoadScene("Golf Level 1");
    }

    IEnumerator TransitionGolfLevel2()
    {
        transitionAnim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        GameObject.FindGameObjectWithTag("main_game").SetActive(false);
        SceneManager.LoadScene("Golf Level 2");
    }

    IEnumerator TransitionGolfLevel3()
    {
        transitionAnim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        GameObject.FindGameObjectWithTag("main_game").SetActive(false);
        SceneManager.LoadScene("Golf Level 3");
    }

    IEnumerator TransitionGolfLevel4()
    {
        transitionAnim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        GameObject.FindGameObjectWithTag("main_game").SetActive(false);
        SceneManager.LoadScene("Golf Level 4");
    }

    IEnumerator TransitionGolfLevel5()
    {
        transitionAnim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        GameObject.FindGameObjectWithTag("main_game").SetActive(false);
        SceneManager.LoadScene("Golf Level 5");
    }

    IEnumerator TransitionGolfLevel6()
    {
        transitionAnim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        GameObject.FindGameObjectWithTag("main_game").SetActive(false);
        SceneManager.LoadScene("Golf Level 6");
    }

    public void YesClick()
    {
        //if randomObject tag equals to "good"
        if (randomObject.tag == "good")
        {
            int index = 0;

            miniGameScenario.SetActive(false);

            DestroyObject();

            switch (DM.miniGameGolfLines[index])
            {
                case "Mini-Game\n \nHello Boss,\nI have a proposition for you but let's see if your swings in golf is as impressive as you think!":
                    {
                        if(mgTimer.IsCooldownActive != true)
                        {
                            if (isGolfGame == true)
                            {
                                Debug.Log(isGolfGame);
                                GolfGameScenario();
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

        isGolfGame = false;
    }

    public void NoClick()
    {
        //if randomObject tag equals to "good"
        if (randomObject.tag == "good")
        {
            int index = 0;

            miniGameScenario.SetActive(false);

            DestroyObject();

            switch (DM.miniGameGolfLines[index])
            {
                case "Mini-Game\n \nHello Boss,\nI have a proposition for you but let's see if your swings in golf is as impressive as you think!":
                    {
                        if (isGolfGame == true)
                        {
                            Debug.Log("No Golf Game");
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

        isGolfGame = false;
    }

    IEnumerator Cooldown(float timer)
    {
        GM.cooldownBrowser.SetActive(true);
        GM.cooldownNoti.SetActive(true);

        yield return new WaitForSeconds(3);

        GM.cooldownNoti.SetActive(false);
        GM.cooldownBrowser.SetActive(false);
    }
}
