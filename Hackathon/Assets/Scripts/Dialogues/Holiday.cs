using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Holiday : MonoBehaviour
{
    public GameManager GM;

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
        int randomRange = Random.Range(0, spawnObjects.Count);
        Vector3 spawnPosition = spawnArea.transform.position;
        randomObject = spawnObjects[randomRange];
        spawned = Instantiate(randomObject, spawnPosition, Quaternion.identity);
        spawned.transform.SetParent(spawnArea.transform, false);
        spawned.transform.position = spawnArea.transform.position;
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

    public void YesClick()
    {
        GM.happiness += Random.Range(1f, 3f);
        GM.money -= Random.Range(1f, 3f);
        GM.popularity += Random.Range(1f, 3f);

        GM.happinessSlider.value += GM.happiness;
        GM.moneySlider.value += GM.money;
        GM.popularitySlider.value += GM.popularity; 
    }

    public void NoClick()
    {
        GM.happiness -= Random.Range(1f, 3f);
        GM.money -= Random.Range(1f, 3f);
        GM.popularity -= Random.Range(1f, 3f);

        GM.happinessSlider.value += GM.happiness;
        GM.moneySlider.value += GM.money;
        GM.popularitySlider.value += GM.popularity;
    }
}
