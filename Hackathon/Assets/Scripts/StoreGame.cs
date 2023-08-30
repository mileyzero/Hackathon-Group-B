using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreGame : MonoBehaviour
{
    public GameObject _maingame;
    public GameManager gameManager;
    public Browser accidentinsurance;
    public MiniGameTimer mgTimer;

    public AudioClip level1;
    public AudioClip level2;
    public AudioClip level3;

    public bool hasPlayedLevel1;
    public bool hasPlayedLevel2;
    public bool hasPlayedLevel3;

    public float money;
    public float popularity;
    public float happiness;

    public float moneyslider;
    public float popularityslider;
    public float happinessslider;

    public AudioSource levelAudioChange;

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


        if (!hasPlayedLevel1)
        {
            if (popularity < 30)
            {
                levelAudioChange.clip = level1;
                levelAudioChange.Play();

                hasPlayedLevel1 = true;
                hasPlayedLevel2 = false;
                hasPlayedLevel3 = false;
            }
        }

        if (!hasPlayedLevel2)
        {
            if (popularity > 30 && popularity < 60)
            {
                levelAudioChange.clip = level2;
                levelAudioChange.Play();

                hasPlayedLevel1 = false;
                hasPlayedLevel2 = true;
                hasPlayedLevel3 = false;
            }
        }

        if (!hasPlayedLevel3)
        {
            if (popularity > 60 && popularity < 90)
            {
                levelAudioChange.clip = level3;
                levelAudioChange.Play();

                hasPlayedLevel1 = false;
                hasPlayedLevel2 = false;
                hasPlayedLevel3 = true;
            }
        }
    }
}
