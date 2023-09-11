using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Control : MonoBehaviour
{
    public static event Action ButtonPressed = delegate { }; //initialize the ButtonPressed event

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
        if (GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money < 10)
        {
            StoreGame.gambleAchCount += 02;
        }

        if (!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped) //if any of the rows are not stopped then won't display the result
        {
            prizeValue = null;
            result.enabled = false;
            resultsChecked = false;
        }

        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultsChecked) //if all the rows are stopped and the results are not checked then check the results and display result
        {
            spin_audioplayer.Stop(); //stop spin audio sfx
            CheckResults();
            result.enabled = true;
            result.text = prizeValue;
        }
    }

    private void OnMouseDown() //when player clicks the button to spin the machine
    {
        if(GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money > 10) //if player has more than 10 money then they can spin
        {
            if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped) //if all the rows are stopped
            {
                DestroyCoin(); //remove one of the coin
                audioplayer.clip = buttonpress_sfx; //sound sfx for button
                spin_audioplayer.clip = spin_sfx; //sound sfx for spin
                audioplayer.Play();
                spin_audioplayer.Play();
                GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money -= 10; //remove money from player's resource

                quitBtn.enabled = false;
                arrow.SetActive(false);
                animator.SetTrigger("Pressed");
                ButtonPressed(); //calls button pressed function
            }
        }
        else
        {
            Debug.Log("Not Enough Money!");
        }

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.moneySlider.value = GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money;
    }

    public void SpawnCoin() //function to spawn the coins based on the amount of money the player has
    {
        GameObject instantiated;
        Vector2 prev_vec = coinSpawner.transform.position;
        int moneyBalance = Mathf.CeilToInt((GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money - 10) / 10); //divide the money the player has by 10 and round down to get the amoun of money player has in single digit

        spawnedCoins = new GameObject[moneyBalance];
        Debug.Log("before for loop" + moneyBalance);
            for (int i = 1; i <= moneyBalance; i++)
            {
                Debug.Log(moneyBalance);
                Debug.Log("work plz");
                //spawn the coin at the coinspawn location and the y postion of the spawn will be 0.15 above the previous coin
                instantiated = Instantiate(casinoCoin, new Vector2(coinSpawner.transform.position.x + UnityEngine.Random.Range(-0.06f,0.06f), prev_vec.y + 0.15f), Quaternion.identity); 
                instantiated.GetComponent<Renderer>().sortingOrder = i; //sorting order of the coin will be same as the i of the for loop 

                prev_vec = instantiated.transform.position; //store the position of the coin that is just spawned
                spawnedCoins[i-1] = instantiated; //add the coins into the array. -1 because the array index starts from 0
            }
    }

    public void DestroyCoin() //function to remove 1 coin and update and resize the array after removing the coin
    {
        if(spawnedCoins.Length > 0)
        {
            Destroy(spawnedCoins[spawnedCoins.Length - 1]);
            spawnedCoins[spawnedCoins.Length - 1] = null;
            Array.Resize(ref spawnedCoins, spawnedCoins.Length - 1);
        }
    }

    private void CheckResults() //function to check the result of the slot machine after spinning the machine
    {
        if (rows[0].stoppedslot == "Happy" && rows[1].stoppedslot == "Happy" && rows[2].stoppedslot == "Happy") //if all slots are happiness then increase happiness by 30
        {
            audioplayer.clip = win;
            audioplayer.Play();
            prizeValue = "+ Employee Happiness";
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.happiness += 30;
            Debug.Log(GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.happiness);
        }

        else if (rows[0].stoppedslot == "Popularity" && rows[1].stoppedslot == "Popularity" && rows[2].stoppedslot == "Popularity") //if all slots are popularity then increae popularity by 30
        {
            audioplayer.clip = win;
            audioplayer.Play();
            prizeValue = "+ Popularity";
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.popularity += 30;
            Debug.Log(GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.popularity);
        }

        else if (rows[0].stoppedslot == "Money" && rows[1].stoppedslot == "Money" && rows[2].stoppedslot == "Money") //if all slots are money then increase money by 30
        {
            audioplayer.clip = win;
            audioplayer.Play();
            prizeValue = "+ Money";
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money += 30;
            SpawnCoin();
            Debug.Log(GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money);
        }

        else if (rows[0].stoppedslot == "7" && rows[1].stoppedslot == "7" && rows[2].stoppedslot == "7") //if all slots are 7 then increase all stats by 30
        {
            StoreGame.gambleLucky7AchCount += 06;

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

    public void QuitGame() //function for when player quits the slotmachine
    {
        StartCoroutine(TransitionQuitLevel());
    }

    IEnumerator TransitionQuitLevel()
    {
        transitionAnim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>()._maingame.gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.moneySlider.value = GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money;

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().hasPlayedLevel1 = false;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().hasPlayedLevel2 = false;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().hasPlayedLevel3 = false;

        SceneManager.LoadScene("SampleScene");
    }
}
