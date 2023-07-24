using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    //public GameObject profileWindow;
    public GameObject thisObject;

    public Sprite profile;

    // Start is called before the first frame update
    void Start()
    {
        //profileWindow.SetActive(false);
        thisObject = this.gameObject;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        //profileWindow.SetActive(true);
        Debug.Log("CLICK");


        Sprite spriteImage = profile;
    }
}
