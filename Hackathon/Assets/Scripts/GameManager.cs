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

    // Start is called before the first frame update
    void Start()
    {
        happinessSlider.value = Random.Range(0.05f, 0.2f);
        moneySlider.value = Random.Range(0.05f, 0.3f);
        popularitySlider.value = Random.Range(0.05f, 0.15f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
