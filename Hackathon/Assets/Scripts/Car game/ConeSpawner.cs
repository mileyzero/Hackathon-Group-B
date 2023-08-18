using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeSpawner : MonoBehaviour
{
    public GameObject cone;
    public GameObject truck;
    public GameObject[] resources;
    public float maxPos;
    public float delaytimer = 1f;
    public float delaystats = 2.8f;
    public float randomnumber;
    public Vector3 conePos;
    float timer;
    float statstimer;
    // Start is called before the first frame update
    void Start()
    {
        timer = delaytimer;
        statstimer = delaystats;
    }

    // Update is called once per frame
    void Update()
    {
        
        timer -= Time.deltaTime;
        statstimer -= Time.deltaTime;

        
        //if timer is still running it will radomly spawn between the car obstacle and the cone obstacle on the random location on the road
        if (timer <= 0) 
        {
            int randomnumber = Random.Range(0, 7);
            conePos = new Vector3(Random.Range(-maxPos, maxPos), transform.position.y, transform.position.z);
            Debug.Log(conePos);
            if(randomnumber <=5) //cone has a higher probability of spawning than truck
            {
                Instantiate(cone, conePos, transform.rotation);
            }
            else
            {
                Instantiate(truck, conePos, transform.rotation);
            }          
            timer = delaytimer;
        }

        SpawnStats();
    }

    public void SpawnStats()
    { 
        //if timer is still running it will radomly spawn between the car obstacle and the cone obstacle on the random location on the road
        if (statstimer <= 0)
        {
            Vector3 spawnstats = new Vector3(Random.Range(-maxPos, maxPos), transform.position.y, transform.position.z);
            Debug.Log(spawnstats);
            Instantiate(resources[Random.Range(0, resources.Length)], spawnstats, transform.rotation);
            statstimer = delaystats;
        }
    }

    
}
