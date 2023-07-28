using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject platform;
    public GameObject bounceplatform;
    public GameObject breakplatform;

    public BoxCollider2D capsule;
    public CircleCollider2D circle;

    public int popularityCount;
    public int moneyCount;
    public int happinessCount;

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

        int randompopularity = Random.Range(-16, 2);
        int randommoney = Random.Range(-13, 2);
        int randomhappiness = Random.Range(-8, 2);
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
}
