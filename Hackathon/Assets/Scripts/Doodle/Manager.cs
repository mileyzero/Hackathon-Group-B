using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public GameObject platform;
    public GameObject bounceplatform;
    public GameObject breakplatform;
    public GameObject player;

    public BoxCollider2D capsule;
    public CircleCollider2D circle;

    public int popularityCount;
    public int moneyCount;
    public int happinessCount;


    public TextMeshProUGUI _money;
    public TextMeshProUGUI _popularity;
    public TextMeshProUGUI _happiness;

    public int platformCount;
    public int numberofstats;
    public GameObject finalplatform;
    public GameObject win;
    public GameObject popularity;
    public GameObject money;
    public GameObject happiness;
    Vector3 spawnposition;

    // Start is called before the first frame update
    void Start()
    {
        spawnposition = new Vector3();

        platformCount = Random.Range(platformCount, platformCount+10);
        
        for (int i = 0; i <= platformCount; i++)
        {
            spawnposition.y += Random.Range(2f, 3f);
            spawnposition.x = Random.Range(-7f, 7f);
            if(i == platformCount)
            {
                Instantiate(finalplatform, new Vector3(0, spawnposition.y + 1.2f), Quaternion.identity);
            }
            else
            {
                int randomnumber = Random.Range(0, 9);

                if (randomnumber <= 5)
                {
                    Instantiate(platform, spawnposition, Quaternion.identity);
                }
                else
                {
                    if(randomnumber <= 6)
                    {
                        if (i != platformCount - 2 && i != platformCount - 1)
                        {
                            Instantiate(bounceplatform, spawnposition, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(platform, spawnposition, Quaternion.identity);
                        }
                    }
                    else
                    {

                        if (i != platformCount - 2 && i != platformCount - 1)
                        {
                            Instantiate(breakplatform, spawnposition, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(platform, spawnposition, Quaternion.identity);
                        }
                    }
                }
            }

            SpawnStats();
        }
    }

    private void SpawnStats()
    {
        int randompopularity = Random.Range(-15, 2);
        int randommoney = Random.Range(-13, 4);
        int randomhappiness = Random.Range(-15, 5);
        if (randompopularity > 0)
        {
            Instantiate(popularity, new Vector3(Random.Range(-7f, 7f), spawnposition.y + 0.97f), Quaternion.identity);
        }
        if (randommoney > 0)
        {
            Instantiate(money, new Vector3(Random.Range(-7f, 7f), spawnposition.y + 0.97f), Quaternion.identity);
        }

        if (randomhappiness > 0)
        {
            Instantiate(happiness, new Vector3(Random.Range(-7f, 7f), spawnposition.y + 0.97f), Quaternion.identity);
        }
    }

    public void DisplayStats()
    {
        StartCoroutine(showstats());
    }

    IEnumerator showstats()
    {
        //player.SetActive(false);
        //_money.text = "+ " + moneyCount.ToString();
        //_popularity.text = "+ " + popularityCount.ToString();
        //_happiness.text = "+ " + happinessCount.ToString();

        
        for(int money = 0;  money <= moneyCount; money++)
        {
            if(_money.text == "+" + moneyCount.ToString())
            {
                break;
            }
            else
            {
                _money.text = "+" + money.ToString();
                yield return new WaitForSeconds(0.2f);
            }
            
        }

        for (int popularity = 0; popularity <= popularityCount; popularity++)
        {
            if (_popularity.text == "+" + popularityCount.ToString())
            {
                break;
            }
            else
            {
                _popularity.text = "+" + popularity.ToString();
                yield return new WaitForSeconds(0.2f);
            }

        }

        for (int happy = 0; happy <= happinessCount ; happy++)
        {
            if (_happiness.text == "+" + happinessCount.ToString())
            {
                break;
            }
            else
            {
                _happiness.text = "+" + happy.ToString();
                yield return new WaitForSeconds(0.2f);
            }

        }

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money += moneyCount;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.popularity += popularityCount;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.happiness += happinessCount;

        yield return new WaitForSeconds(2f);

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>()._maingame.gameObject.SetActive(true);

        this.gameObject.SetActive(false);

        SceneManager.LoadScene(0);
    }
}
