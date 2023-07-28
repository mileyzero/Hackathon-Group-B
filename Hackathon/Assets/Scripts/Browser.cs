using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Browser : MonoBehaviour
{
    public GameManager GM;

    public GameObject doNutBrowser;
    public GameObject crossButton;

    public bool accidentInsurance;
    public bool healthInsurance;
    public bool investmentInsurance;

    // Start is called before the first frame update
    void Start()
    {
        doNutBrowser.SetActive(false);
        crossButton.SetActive(false);

        accidentInsurance = false;
        healthInsurance = false;
        investmentInsurance = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        doNutBrowser.SetActive(true);

        Debug.Log("BROWSER OPEN");
    }

    public void CloseWindow()
    {
        doNutBrowser.SetActive(false);

        Debug.Log("BROWSER CLOSE");
    }

    public void BuyAccidentInsurance()
    {
        Debug.Log(accidentInsurance);

        GM.money -= 5f;

        GM.moneySlider.value += GM.money;

        accidentInsurance = true;

        if(accidentInsurance == true)
        {
            enabled = false;
        }
        else
        {
            enabled = true;
        }
    }

    public void BuyHealthInsurance()
    {
        Debug.Log(healthInsurance);

        GM.money -= 5f;

        GM.moneySlider.value += GM.money;

        healthInsurance = true;

        if (healthInsurance == true)
        {
            enabled = false;
        }
        else
        {
            enabled = true;
        }
    }

    public void BuyInvestmentInsurance()
    {
        Debug.Log(investmentInsurance);

        GM.money -= 5f;

        GM.moneySlider.value += GM.money;

        investmentInsurance = true;

        if (investmentInsurance == true)
        {
            enabled = false;
        }
        else
        {
            enabled = true;
        }
    }
}
