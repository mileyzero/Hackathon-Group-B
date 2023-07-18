using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Investment : MonoBehaviour
{
    //Reference script to GM
    public GameManager GM;

    //private GameObject for scam and goodObject
    private GameObject scamObject;
    private GameObject goodObject;

    //a List to set how many personas to randomize
    public List<GameObject> spawnObjects;

    //spawnArea for player model
    public GameObject spawnArea;
    public GameObject randomObject;

    //reference GameObject for investmentScenario
    public GameObject investmentScenario;

    //Button for interaction of scenario
    public Button scenarioButton;

    //GameObjects for scenario
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject investmentDialogue;
    public GameObject spawned;
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
    }

    // Update is called once per frame
    void Update()
    {

    }

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

    public int RandomNumber()
    {
        int random = Random.Range(1, 10);
        return random;
    }

    public void SpawnScenario()
    {
        SpawnObject();

        investmentScenario.SetActive(true);
        nameBox.SetActive(true);

        scenarioButton.enabled = false;
        Debug.Log(scenarioButton.enabled);

        StartCoroutine(AnimationPlay(0.5f));
    }

    IEnumerator AnimationPlay(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        yesButton.SetActive(true);
        noButton.SetActive(true);
        investmentDialogue.SetActive(true);
    }

    public void DestroyObject()
    {
        scamObject = GameObject.FindGameObjectWithTag("scam");
        goodObject = GameObject.FindGameObjectWithTag("good");

        Debug.Log("Destroyed");

        if (randomObject.tag == "scam")
        {
            Destroy(scamObject);

            GM.happiness -= Random.Range(1f, 3f);
            GM.money -= Random.Range(1f, 3f);
            GM.popularity -= Random.Range(1f, 3f);

            GM.happinessSlider.value += GM.happiness;
            GM.moneySlider.value += GM.money;
            GM.popularitySlider.value += GM.popularity;
        }
        else if (randomObject.tag == "good")
        {
            Destroy(goodObject);

            GM.happiness += 1f;
            GM.money -= 1f;
            GM.popularity += 1.5f;

            GM.happinessSlider.value += GM.happiness;
            GM.moneySlider.value += GM.money;
            GM.popularitySlider.value += GM.popularity;
        }
    }

    public void YesClick()
    {
        if (randomObject.tag == "scam")
        {
            if (RandomNumber() < 7)
            {
                Debug.Log("YOU GOT SCAMMED");
                DestroyObject();
            }
            else
            {
                Debug.Log("INVESMENT SUCCESSFUL");
                DestroyObject();
            }
        }

        else if (randomObject.tag == "good")
        {
            if (RandomNumber() < 6)
            {
                Debug.Log("INVESTMENT SUCCESSFUL");
                DestroyObject();
            }
            else
            {
                Debug.Log("YOU GOT SCAMMED");
                DestroyObject();
            }
        }

        GM.FunctionUpdates();
    }

    public void NoClick()
    {
        if (randomObject.tag == "scam")
        {
            if (RandomNumber() < 7)
            {
                Debug.Log("YOU GOT SCAMMED");
                DestroyObject();
            }
            else
            {
                Debug.Log("INVESMENT SUCCESSFUL");
                DestroyObject();
            }
        }

        else if (randomObject.tag == "good")
        {
            if (RandomNumber() < 6)
            {
                Debug.Log("INVESTMENT SUCCESSFUL");
                DestroyObject();
            }
            else
            {
                Debug.Log("YOU GOT SCAMMED");
                DestroyObject();
            }
        }

        GM.FunctionUpdates();
    }
}
