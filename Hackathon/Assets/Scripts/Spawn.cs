using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    #region Variables
    public GameManager GM;

    public List<GameObject> spawnobjects;
    public GameObject spawnArea;
    public GameObject randomObject;
    public GameObject scamobject;
    public GameObject goodobject;

    public string scam = "scam";
    public string good = "good";

    public GameObject scenario;

    public Button scenarioButton;

    public GameObject yesButton;
    public GameObject noButton;
    public GameObject dialogue;
    public GameObject spawned;
    #endregion

    private void Start()
    {
        scenario.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
        dialogue.SetActive(false);
    }

    public void SpawnObject()
    {
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

        if (randomObject.tag == "scam")
        {
            Destroy(scamobject);

            GM.happiness -= 0.5f;
            GM.money -= 1f;
            GM.popularity -= 0.5f;
        }
        else if(randomObject.tag == "good")
        {
            Destroy(goodobject);

            GM.happiness += 1f;
            GM.money -= 1f;
            GM.popularity += 1.5f;
        }
 
    }

    public void SpawnScenario()
    {
        SpawnObject();

        scenario.SetActive(true);
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
