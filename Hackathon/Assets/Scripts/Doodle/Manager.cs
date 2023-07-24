using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject platform;

    public int platformCount;
    public GameObject finalplatform;
    public GameObject win;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnposition = new Vector3();

        platformCount = Random.Range(10, 20);

        for (int i = 0; i <= platformCount; i++)
        {
            spawnposition.y += Random.Range(2f, 3f);
            spawnposition.x = Random.Range(-5f, 5f);
            if(i == platformCount)
            {
                finalplatform = Instantiate(platform, spawnposition, Quaternion.identity);
            }
            else
            {
                Instantiate(platform, spawnposition, Quaternion.identity);
            }

        }
    }
}
