using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameGenerator : MonoBehaviour
{
    public GameObject nameBox;

    public string completeName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NameRandomList()
    {
        string[] first = { "Kullervo", "Feliciano", "Daichi", "Clement", "Orfeas", "Lorna", "Shad", "Ava", "Ina", "Alphonse", "Anthony", "Maxine", "Willie" };
        string[] last = { "Vladan", "Sigismund", "Deror", "Ragna", "Alene", "Good", "Mills", "Payne", "Webster", "Young", "Meza", "Diaz", "Odom" };

        string completeFirst = first[Random.Range(0, first.Length)];
        string completeLast = last[Random.Range(0, last.Length)];

        completeName = completeFirst + " " + completeLast;
        nameBox.GetComponent<TextMeshProUGUI>().text = completeName;
    }
}
