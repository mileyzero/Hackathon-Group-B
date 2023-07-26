using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Browser : MonoBehaviour
{
    public GameObject doNutBrowser;

    // Start is called before the first frame update
    void Start()
    {
        doNutBrowser.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        doNutBrowser.SetActive(true);

        Debug.Log("BROWSER CLICK");
    }

    public void CloseWindow()
    {
        doNutBrowser.SetActive(false);
    }
}
