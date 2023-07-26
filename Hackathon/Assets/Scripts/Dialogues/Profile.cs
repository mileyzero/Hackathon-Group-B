using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    //Profile Window GameObject
    public GameObject profileWindow;

    public Slider trustSlider;
    public Slider successSlider;

    // Start is called before the first frame update
    void Start()
    {
        profileWindow.SetActive(false);
    }


    public void OnMouseDown()
    {
        profileWindow.SetActive(true);

        Debug.Log("PROFILE CLICK");
    }

    public void CloseWindow()
    {
        profileWindow.SetActive(false);
    }
}
