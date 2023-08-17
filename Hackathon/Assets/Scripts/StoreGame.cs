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

    private void Update()
    {
        money = gameManager.money;
        popularity = gameManager.popularity;
        happiness = gameManager.happiness;
    }
}
