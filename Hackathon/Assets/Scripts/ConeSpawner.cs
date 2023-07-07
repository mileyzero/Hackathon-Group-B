using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeSpawner : MonoBehaviour
{
    public GameObject cone;
    public float maxPos;
    public float delaytimer = 1f;
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

        if (timer <= 0) 
        {
            Vector3 conePos = new Vector3(Random.Range(-maxPos, maxPos), transform.position.y, transform.position.z);
            Instantiate(cone, conePos, transform.rotation);
            timer = delaytimer;
        }    
    }

    
}
