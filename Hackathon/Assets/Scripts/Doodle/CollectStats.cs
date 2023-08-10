using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectStats : MonoBehaviour
{
    public GameObject manager;

    public void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("doodlemanager");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.gameObject.tag == "doodleMoney")
        {
            manager.GetComponent<Manager>().moneyCount += 1;
        }

        else if (this.gameObject.tag == "doodleHappy")
        {
            manager.GetComponent<Manager>().happinessCount += 1;
        }

        else if (this.gameObject.tag == "doodlePopular")
        {
            manager.GetComponent<Manager>().popularityCount += 1;
        }

        Destroy(this.gameObject);
    }
}
