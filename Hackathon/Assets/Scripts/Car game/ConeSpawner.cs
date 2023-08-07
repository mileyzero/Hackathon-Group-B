using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeSpawner : MonoBehaviour
{
    public GameObject cone;
    public GameObject truck;
    public float maxPos;
    public float delaytimer = 1f;
    public float randomnumber;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = delaytimer;
    }

    // Update is called once per frame
    void Update()
    {
        
        timer -= Time.deltaTime;
        
        //if timer is still running it will radomly spawn between the car obstacle and the cone obstacle on the random location on the road
        if (timer <= 0) 
        {
            int randomnumber = Random.Range(0, 7);
            Vector3 conePos = new Vector3(Random.Range(-maxPos, maxPos), transform.position.y, transform.position.z);
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
    }

    
}
