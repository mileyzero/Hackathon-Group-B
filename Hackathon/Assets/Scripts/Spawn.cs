using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    #region Variables
    public GameManager GM;
    
    [SerializeField]
    public GameObject randomObject;

    private GameObject scamobject;
    private GameObject goodobject;
    private GameObject spawned;

    public List<GameObject> spawnobjects;
    public GameObject spawnArea;

    public string scam = "scam";
    public string good = "good";

    public GameObject scenario;
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject dialogue;
    public GameObject nameBox;

    public Button scenarioButton;

    #endregion

    private void Start()
    {
        scenario.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        dialogue.SetActive(false);
        nameBox.SetActive(false);

        scenarioButton.enabled = false;
    }

    public void SpawnObject()
    {
        Debug.Log("Spawned");
        int randomrange = Random.Range(0, spawnobjects.Count);
        Vector3 spawnPosition = spawnArea.transform.position;
        randomObject = spawnobjects[randomrange];
        spawned = Instantiate(randomObject, spawnPosition, Quaternion.identity);
        spawned.transform.SetParent(spawnArea.transform, false);
        spawned.transform.position = spawnArea.transform.position;
    }

    public int RandomNumber()
    {
        int random = Random.Range(1,10);
        return random;
    }

    public void Destroyobj()
    {
        scamobject = GameObject.FindGameObjectWithTag("scam");
        goodobject = GameObject.FindGameObjectWithTag("good");

        Debug.Log("Destroyed");
        if (randomObject.tag == "scam")
        {
            Destroy(scamobject);

            GM.happiness -= Random.Range(1f, 3f);
            GM.money -= Random.Range(1f, 3f);
            GM.popularity -= Random.Range(1f, 3f);

            GM.happinessSlider.value += GM.happiness;
            GM.moneySlider.value += GM.money;
            GM.popularitySlider.value += GM.popularity;
        }
        else if(randomObject.tag == "good")
        {
            Destroy(goodobject);

            GM.happiness += 1f;
            GM.money -= 1f;
            GM.popularity += 1.5f;

            GM.happinessSlider.value += GM.happiness;
            GM.moneySlider.value += GM.money;
            GM.popularitySlider.value += GM.popularity;
        }
    }

    public void SpawnScenario()
    {
        SpawnObject();

        scenario.SetActive(true);
        nameBox.SetActive(true);

        scenarioButton.enabled = false;

        StartCoroutine(AnimationPlay(0.5f));
    }

    IEnumerator AnimationPlay(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        yesButton.SetActive(true);
        noButton.SetActive(true);
        dialogue.SetActive(true);
    }

    public void YesClick()
    {
        if(randomObject.tag == "scam")
        {
            if(RandomNumber() < 7)
            {
                Debug.Log("YOU GOT SCAMMED");
                Destroyobj();
            }
            else
            {
                Debug.Log("INVESMENT SUCCESSFUL");
                Destroyobj();
            }
        }

        else if (randomObject.tag == "good")
        {
            if (RandomNumber() < 6)
            {
                Debug.Log("INVESTMENT SUCCESSFUL");
                Destroyobj();
            }
            else
            {
                Debug.Log("YOU GOT SCAMMED");
                Destroyobj();
            }
        }
    }

    public void NoClick()
    {
        if (randomObject.tag == "scam")
        {
            if (RandomNumber() < 7)
            {
                Debug.Log("YOU GOT SCAMMED");
                Destroyobj();
            }
            else
            {
                Debug.Log("INVESMENT SUCCESSFUL");
                Destroyobj();
            }
        }

        else if (randomObject.tag == "good")
        {
            if (RandomNumber() < 6)
            {
                Debug.Log("INVESTMENT SUCCESSFUL");
                Destroyobj();
            }
            else
            {
                Debug.Log("YOU GOT SCAMMED");
                Destroyobj();
            }
        }
    }
}
