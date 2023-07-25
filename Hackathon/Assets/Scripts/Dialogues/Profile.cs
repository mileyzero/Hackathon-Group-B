using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    //Profile Window GameObject
    public GameObject profileWindow;

    //Profile Picture
    public GameObject profileObject;

    //Spawn Area of Profile Picture
    public GameObject spawnArea;

    private GameObject profileSpawn;
    private GameObject profileSpawned;
    private GameObject spawned;

    // Start is called before the first frame update
    void Start()
    {
        profileWindow.SetActive(false);
        profileObject = this.gameObject;
    }


    public void OnMouseDown()
    {
        profileWindow.SetActive(true);

        spawnArea = GameObject.FindGameObjectWithTag("profileSpawn");

        Debug.Log("CLICK");

        SpawnObject();
    }

    public void SpawnObject()
    {
        Debug.Log("Spawned");

        Vector3 spawnPosition = spawnArea.transform.position;
        spawned = Instantiate(profileObject, spawnPosition, Quaternion.identity);
        spawned.transform.SetParent(spawnArea.transform, false);
        spawned.transform.position = spawnArea.transform.position;
    }


    public void CloseWindow()
    {
        profileWindow.SetActive(false);
    }
}
