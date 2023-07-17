using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Holiday : MonoBehaviour
{
    public GameManager GM;

    private GameObject scamObject;
    private GameObject goodObject;

    public List<GameObject> spawnObjects;
    public GameObject spawnArea;
    public GameObject randomObject;

    public GameObject holidayScenario;

    public Button scenarioButton;

    public GameObject yesButton;
    public GameObject noButton;
    public GameObject holidayDialogue;
    public GameObject spawned;
    public GameObject nameBox;

    // Start is called before the first frame update
    void Start()
    {
        GM.employeeNotiIcon.SetActive(false);
        GM.employeeButton.SetActive(false);

        enabled = false;

        holidayScenario.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        holidayDialogue.SetActive(false);
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

        holidayScenario.SetActive(true);
        nameBox.SetActive(true);

        scenarioButton.enabled = false;

        StartCoroutine(AnimationPlay(0.5f));
    }

    IEnumerator AnimationPlay(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        yesButton.SetActive(true);
        noButton.SetActive(true);
        holidayDialogue.SetActive(true);
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
