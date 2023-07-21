using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject platform;

    public int platformCount = 300;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnposition = new Vector3();

        for (int i = 0; i < platformCount; i++)
        {
            spawnposition.y += Random.Range(0.5f, 2f);
            spawnposition.x = Random.Range(-5f, 5f);
            Instantiate(platform, spawnposition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
