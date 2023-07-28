using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Profile : MonoBehaviour
{
    //Reference script to investmentManager
    public Investment investmentManager;

    //Reference script to nameGenerator
    public NameGenerator nameGenerator;

    //Profile Window GameObject
    public GameObject profileWindow;
    public GameObject profileSpawnObj;
    public GameObject profileName;

    //Sliders for trust and success
    public Slider trustSlider;
    public Slider successSlider;

    //int values for trust and success
    public int trustValue;
    public int successValue;

    //Reference to button component
    public Button profileButton;

    //GameObject profileSpawned for profile to appear on box
    private GameObject profileSpawned;

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

        Vector3 profilePosition = profileSpawnObj.transform.position;

        profileSpawned = Instantiate(investmentManager.randomObject, profilePosition, Quaternion.identity);
        profileSpawned.transform.SetParent(profileSpawnObj.transform, false);
        profileSpawned.transform.position = profileSpawnObj.transform.position;

        profileName.GetComponent<TextMeshProUGUI>().text = nameGenerator.completeName;

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
