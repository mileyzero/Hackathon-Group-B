using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Holiday holidayManager;
    public Investment investManager;
    public NameGenerator nameManager;

    public GameObject investmentNotiIcon;
    public GameObject employeeNotiIcon;

    public GameObject investmentButton;
    public GameObject employeeButton;

    public Slider happinessSlider;
    public Slider moneySlider;
    public Slider popularitySlider;

    public float maxHappiness = 10f;
    public float maxMoney = 10f;
    public float maxPopularity = 10f;

    public float happiness;
    public float money;
    public float popularity;

    public float originalTimer = 3f;
    public float delayTimer = 3f;

    public float timer;

    public int randomInitialize;

    public bool isRunning;

    // Start is called before the first frame update
    void Start()
    {
        timer = delayTimer;

        isRunning = false;

        happinessSlider.value = Random.Range(20f, 55f);
        moneySlider.value = Random.Range(30f, 65f);
        popularitySlider.value = 20f;

        investmentNotiIcon.SetActive(false);
        employeeNotiIcon.SetActive(false);

        investmentButton.SetActive(false);
        employeeButton.SetActive(false);

        holidayManager.scenarioButton.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isRunning);

        if(isRunning == false)
        {
            randomInitialize = Random.Range(0, 2);
            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                isRunning = true;

                if(randomInitialize == 0)
                {
                    investManager.scenarioButton.enabled = true;

                    investmentNotiIcon.SetActive(true);
                    investmentButton.SetActive(true);
                }

                else if(randomInitialize == 1)
                {
                    holidayManager.scenarioButton.enabled = true;

                    employeeButton.SetActive(true);
                    employeeNotiIcon.SetActive(true);
                }

                Debug.Log(randomInitialize);
            }
        }
    }

    public void FunctionUpdates()
    {
        timer = originalTimer;
        isRunning = false;
    }

    public void InitializeHoliday()
    {
        Debug.Log("Holiday");
        holidayManager.GetComponent<Holiday>().SpawnScenario();
        nameManager.GetComponent<NameGenerator>().NameRandomList();
    }

    public void InitializeInvest()
    {
        Debug.Log("Investment");
        investManager.GetComponent<Investment>().SpawnScenario();
        nameManager.GetComponent<NameGenerator>().NameRandomList();
    }
}
