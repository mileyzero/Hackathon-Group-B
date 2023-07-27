using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    //Reference script to investmentManager
    public Investment investmentManager;

    //Profile Window GameObject
    public GameObject profileWindow;

    public List<GameObject> spawnableObjects;

    //Sliders for trust and success
    public Slider trustSlider;
    public Slider successSlider;

    public int trustValue;
    public int successValue;

    //Reference to button component
    public Button profileButton;

    // Start is called before the first frame update
    void Start()
    {
        //Set profileWindow to false on Start
        profileWindow.SetActive(false);

        //profileButton to get the button's component
        profileButton = GetComponent<Button>();

        trustValue = 0;
        successValue = 0;
    }

    void Update()
    {

    }

    public void OnMouseDown()
    {
        profileWindow.SetActive(true);

        if (investmentManager.randomObject.tag == "good")
        {
            if(trustValue == 0 || successValue == 0)
            {
                trustSlider.value = trustValue = Random.Range(40, 70);
                successSlider.value = successValue = Random.Range(40, 70);

                Debug.Log(trustSlider.value);
                Debug.Log(successSlider.value);
            }
        }

        if (investmentManager.randomObject.tag == "scam")
        {
            if (trustValue == 0 || successValue == 0)
            {
                trustSlider.value = trustValue = Random.Range(10, 40);
                successSlider.value = successValue = Random.Range(20, 40);

                Debug.Log(trustSlider.value);
                Debug.Log(successSlider.value);
            }
        }

        Debug.Log("PROFILE CLICK");
    }

    public void CloseWindow()
    {
        profileWindow.SetActive(false);
    }
}
