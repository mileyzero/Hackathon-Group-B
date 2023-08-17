using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreGame : MonoBehaviour
{
    public GameObject _maingame;
    public GameManager gameManager;
    public Browser accidentinsurance;
    public MiniGameTimer mgTimer;

    public float money;
    public float popularity;
    public float happiness;

    public float moneyslider;
    public float popularityslider;
    public float happinessslider;

    private void Update()
    {
        money = gameManager.money;
        popularity = gameManager.popularity;
        happiness = gameManager.happiness;

        gameManager.moneySlider.value = money;
        gameManager.popularitySlider.value = popularity;
        gameManager.happinessSlider.value = happiness;

        moneyslider = gameManager.moneySlider.value;
        popularityslider = gameManager.popularitySlider.value;
        happinessslider = gameManager.happinessSlider.value;

    }
}
