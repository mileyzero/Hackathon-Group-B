using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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

    public bool isRunning;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        isRunning = false;

        happinessSlider.value = Random.Range(20f, 55f);
        moneySlider.value = Random.Range(30f, 65f);
        popularitySlider.value = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timer);
        Debug.Log(isRunning);

        if(isRunning == false)
        {
            timer -= delayTimer;
            int randomInitialize = Random.Range(0, 2);

            if(timer <= 0)
            {
                isRunning = true;

                if(randomInitialize == 0)
                {
                    InitializeHoliday();
                }

                else if(randomInitialize == 1)
                {
                    InitializeInvest();
                }
            }
        }
    }

    public void InitializeHoliday()
    {

    }

    public void InitializeInvest()
    {

    }
}
