using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Control : MonoBehaviour
{
    public static event Action ButtonPressed = delegate { };

    public TextMeshProUGUI result;

    public Animator animator;

    public Row[] rows;

    private string prizeValue;

    private bool resultsChecked = false;

    public Button quitBtn;

    public GameObject arrow;

    // Update is called once per frame
    void Update()
    {
        if (!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped)
        {
            prizeValue = null;
            result.enabled = false;
            resultsChecked = false;
        }

        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultsChecked)
        {
            CheckResults();
            result.enabled = true;
            result.text = prizeValue;
        }
    }

    
    private void OnMouseDown()
    {
        if(GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money >= 10)
        {
            if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
            {
                GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money -= 10;

                quitBtn.enabled = false;
                arrow.SetActive(false);
                animator.SetTrigger("Pressed");
                ButtonPressed();
            }
        }
        else
        {
            Debug.Log("Not Enough Money!");
        }

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.moneySlider.value = GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money;
    }


    private void CheckResults()
    {
        if (rows[0].stoppedslot == "Happy" && rows[1].stoppedslot == "Happy" && rows[2].stoppedslot == "Happy")
        {
            prizeValue = "+ Employee Happiness";
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.happiness += 30;
            Debug.Log(GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.happiness);
        }

        else if (rows[0].stoppedslot == "Popularity" && rows[1].stoppedslot == "Popularity" && rows[2].stoppedslot == "Popularity")
        {
            prizeValue = "+ Popularity";
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.popularity += 30;
            Debug.Log(GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.popularity);
        }

        else if (rows[0].stoppedslot == "Money" && rows[1].stoppedslot == "Money" && rows[2].stoppedslot == "Money")
        {
            prizeValue = "+ Money";
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money += 30;
            Debug.Log(GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money);
        }

        else if (rows[0].stoppedslot == "7" && rows[1].stoppedslot == "7" && rows[2].stoppedslot == "7")
        {
            prizeValue = "+ All Resources";
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money += 30;
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.popularity += 30;
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.happiness += 30;

            Debug.Log(GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money);
            Debug.Log(GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.popularity);
            Debug.Log(GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.happiness);
        }

        resultsChecked = true;
        arrow.SetActive(true);
        quitBtn.enabled = true;
    }

    public void QuitGame()
    {
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>()._maingame.gameObject.SetActive(true);

        SceneManager.LoadScene(0);
    }
}
