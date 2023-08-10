using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreGame : MonoBehaviour
{
    public GameObject _maingame;
    public GameManager gameManager;
    public Browser accidentinsurance;

    public float money;
    private void Update()
    {
        money = gameManager.money;
    }
}
