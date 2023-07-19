using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Holiday : MonoBehaviour
{
    public GameManager GM;

    private GameObject holidayObject;

    public List<GameObject> spawnObjects;
    public GameObject spawnArea;

    private GameObject randomObject;
    private GameObject spawned;

    public GameObject holidayScenario;

    public Button scenarioButton;

    public GameObject yesButton;
    public GameObject noButton;
    public GameObject holidayDialogue;
    public GameObject nameBox;

    public bool isYes;
    public bool isNo;

    // Start is called before the first frame update
    void Start()
    {
        isYes = false;
        isNo = false;

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

        isYes = true;
        isNo = true;

        scenarioButton.enabled = false;
        Debug.Log(scenarioButton.enabled);

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
        holidayObject = GameObject.FindGameObjectWithTag("holiday");

        if(randomObject.tag == "holiday")
        {
            Destroy(holidayObject);
        }
    }

    public void YesClick()
    {
        if (isYes == true)
        {
            holidayScenario.SetActive(false);

            GM.happiness += Random.Range(1f, 3f);
            GM.money -= Random.Range(1f, 3f);
            GM.popularity += Random.Range(1f, 3f);

            GM.happinessSlider.value += GM.happiness;
            GM.moneySlider.value += GM.money;
            GM.popularitySlider.value += GM.popularity;

            DestroyObject();
        }

        GM.FunctionUpdates();
    }

    public void NoClick()
    {
        if (isNo == true)
        {
            holidayScenario.SetActive(false);

            GM.happiness -= Random.Range(1f, 3f);
            GM.money -= Random.Range(1f, 3f);
            GM.popularity -= Random.Range(1f, 3f);

            GM.happinessSlider.value += GM.happiness;
            GM.moneySlider.value += GM.money;
            GM.popularitySlider.value += GM.popularity;

            DestroyObject();
        }

        GM.FunctionUpdates();
    }
}
