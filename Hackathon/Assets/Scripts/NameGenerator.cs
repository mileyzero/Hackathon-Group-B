using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NameGenerator : MonoBehaviour
{
    public GameObject textBox;

    public string completeName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomListName()
    {
        string[] first = new string[] { "Alejandro", "Sapphire", "Jazmin", "Lily", "Aurora", "Bethan", "Lina", "Aminah", "Leslie", "Elysia" };
        string[] last = new string[] { "Oliver", "Williams", "Cardenas", "Holden", "Buchanan", "Thornton", "Winters", "Hancock", "Mcdaniel", "Allison" };

        completeName = first[Random.Range(0, first.Length)] + " " + last[Random.Range(0, last.Length)];
        textBox.GetComponent<TextMeshProUGUI>().text = completeName;
    }
}
