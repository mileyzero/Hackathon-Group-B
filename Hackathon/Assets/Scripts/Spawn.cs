using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    public List<GameObject> spawnobjects;
    public GameObject spawnArea;
    public GameObject randomobject;
    public GameObject scamobject;
    public GameObject goodobject;
    public string scam = "scam";
    public string good = "good";

    public GameObject scenario;
    public GameObject button;
    public GameObject spawned;
    // Start is called before the first frame update
    void Start()
    {
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObject()
    {
        int randomrange = Random.Range(0, spawnobjects.Count);
        Vector3 spawnPosition = spawnArea.transform.position;
        randomobject = spawnobjects[randomrange];
        spawned = Instantiate(randomobject, spawnPosition, Quaternion.identity);
        spawned.transform.SetParent(spawnArea.transform, false);
        spawned.transform.position = spawnArea.transform.position;
        
    }

    public int RandomNumber()
    {
        int random = Random.Range(1,10);
        return random;
    }

    public void Destroyobj()
    {
        scamobject = GameObject.FindGameObjectWithTag("scam");
        goodobject = GameObject.FindGameObjectWithTag("good");
        if (randomobject.tag == "scam")
        {
            Destroy(scamobject);
        }
        else if(randomobject.tag == "good")
        {
            Destroy(goodobject);
        }
 
    }

    public void SpawnScenario()
    {
        SpawnObject();
        scenario.SetActive(true);
        button.SetActive(false);
    }

    public void YesClick()
    {
        if(randomobject.tag == "scam")
        {
            if(RandomNumber() <9)
            {
                Debug.Log("YOU GOT SCAMMED");
                Destroyobj();
            }
            else
            {
                Debug.Log("INVESMENT SUCCESSFUL");
                Destroyobj();
            }
        }

        else if (randomobject.tag == "good")
        {
            if (RandomNumber() < 9)
            {
                Debug.Log("INVESTMENT SUCCESSFUL");
                Destroyobj();
            }
            else
            {
                Debug.Log("YOU GOT SCAMMED");
                Destroyobj();
            }
        }
    }
}
