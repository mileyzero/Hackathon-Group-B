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
    public Animator transitionAnim;

    public Row[] rows;

    private string prizeValue;

    private bool resultsChecked = false;

    public float transitionTime = 1f;

    public Button quitBtn;

    public GameObject arrow;
    public GameObject coinSpawner;
    public GameObject casinoCoin;
    public GameObject[] spawnedCoins;

    public AudioSource audioplayer;

    public AudioSource spin_audioplayer;

    public AudioClip spin_sfx;

    public AudioClip win;

    public AudioClip buttonpress_sfx;

    private void Start()
    {
        SpawnCoin();
    }

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
            spin_audioplayer.Stop();
            CheckResults();
            result.enabled = true;
            result.text = prizeValue;
        }
    }

    private void OnMouseDown()
    {
        if(GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money > 10)
        {
            
            if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
            {
                DestroyCoin();
                audioplayer.clip = buttonpress_sfx;
                spin_audioplayer.clip = spin_sfx;
                audioplayer.Play();
                spin_audioplayer.Play();
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

    public void SpawnCoin()
    {
        GameObject instantiated;
        Vector2 prev_vec = coinSpawner.transform.position;
        int moneyBalance = Mathf.CeilToInt((GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money - 10) / 10);

        spawnedCoins = new GameObject[moneyBalance];
        Debug.Log("before for loop" + moneyBalance);
            for (int i = 1; i <= moneyBalance; i++)
            {
                Debug.Log(moneyBalance);
                Debug.Log("work plz");

                instantiated = Instantiate(casinoCoin, new Vector2(coinSpawner.transform.position.x + UnityEngine.Random.Range(-0.06f,0.06f), prev_vec.y + 0.15f), Quaternion.identity);
                instantiated.GetComponent<Renderer>().sortingOrder = i;

                prev_vec = instantiated.transform.position;
                spawnedCoins[i-1] = instantiated;
            }
    }

    public void DestroyCoin()
    {
        if(spawnedCoins.Length > 0)
        {
            Destroy(spawnedCoins[spawnedCoins.Length - 1]);
            spawnedCoins[spawnedCoins.Length - 1] = null;
            Array.Resize(ref spawnedCoins, spawnedCoins.Length - 1);
        }
    }

    private void CheckResults()
    {
        if (rows[0].stoppedslot == "Happy" && rows[1].stoppedslot == "Happy" && rows[2].stoppedslot == "Happy")
        {
            audioplayer.clip = win;
            audioplayer.Play();
            prizeValue = "+ Employee Happiness";
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.happiness += 30;
            Debug.Log(GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.happiness);
        }

        else if (rows[0].stoppedslot == "Popularity" && rows[1].stoppedslot == "Popularity" && rows[2].stoppedslot == "Popularity")
        {
            audioplayer.clip = win;
            audioplayer.Play();
            prizeValue = "+ Popularity";
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.popularity += 30;
            Debug.Log(GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.popularity);
        }

        else if (rows[0].stoppedslot == "Money" && rows[1].stoppedslot == "Money" && rows[2].stoppedslot == "Money")
        {
            audioplayer.clip = win;
            audioplayer.Play();
            prizeValue = "+ Money";
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money += 30;
            SpawnCoin();
            Debug.Log(GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money);
        }

        else if (rows[0].stoppedslot == "7" && rows[1].stoppedslot == "7" && rows[2].stoppedslot == "7")
        {
            audioplayer.clip = win;
            audioplayer.Play();
            prizeValue = "+ All Resources";
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money += 30;
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.popularity += 30;
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.happiness += 30;
            SpawnCoin();
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
        StartCoroutine(TransitionQuitLevel());
    }

    IEnumerator TransitionQuitLevel()
    {
        transitionAnim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>()._maingame.gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.moneySlider.value = GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money;

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.hasPlayedLevel1 = false;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.hasPlayedLevel2 = false;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.hasPlayedLevel3 = false;

        SceneManager.LoadScene("SampleScene");
    }
}
