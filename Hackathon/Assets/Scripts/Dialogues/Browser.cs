using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Browser : MonoBehaviour
{
    public GameManager GM;

    public Button accidentBtn;
    public Button healthBtn;
    public Button investmentBtn;

    public GameObject accidentBtnDisabled;
    public GameObject healthBtnDisabled;
    public GameObject investmentBtnDisabled;

    public GameObject doNutBrowser;
    public GameObject crossButton;

    public GameObject accidentGreyed;
    public GameObject healthGreyed;
    public GameObject insuranceGreyed;

    public GameObject accidentActive;
    public GameObject healthActive;
    public GameObject insuranceActive;

    public GameObject accidentInsuranceCfm;
    public GameObject healthInsuranceCfm;
    public GameObject investmentInsuranceCfm;

    public bool accidentInsurance;
    public bool healthInsurance;
    public bool investmentInsurance;


    // Start is called before the first frame update
    void Start()
    {
        accidentGreyed.SetActive(true);
        healthGreyed.SetActive(true);
        insuranceGreyed.SetActive(true);

        accidentBtnDisabled.SetActive(false);
        healthBtnDisabled.SetActive(false);
        investmentBtnDisabled.SetActive(false);

        accidentActive.SetActive(false);
        healthActive.SetActive(false);
        insuranceActive.SetActive(false);

        accidentInsuranceCfm.SetActive(false);
        healthInsuranceCfm.SetActive(false);
        investmentInsuranceCfm.SetActive(false);

        doNutBrowser.SetActive(false);
        crossButton.SetActive(false);

        accidentInsurance = false;
        healthInsurance = false;
        investmentInsurance = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(accidentInsurance == false)
        {
            accidentGreyed.SetActive(true);
            accidentActive.SetActive(false);

            accidentBtn.enabled = true;

            accidentBtnDisabled.SetActive(false);
        }

        if(accidentInsurance == true)
        {
            accidentGreyed.SetActive(false);
            accidentActive.SetActive(true);
        }

        if (healthInsurance == false)
        {
            healthGreyed.SetActive(true);
            healthActive.SetActive(false);

            healthBtn.enabled = true;

            healthBtnDisabled.SetActive(false);
        }
        
        if(healthInsurance == true)
        {
            healthGreyed.SetActive(false);
            healthActive.SetActive(true);
        }

        if(investmentInsurance == false)
        {
            insuranceGreyed.SetActive(true);
            insuranceActive.SetActive(false);

            investmentBtn.enabled = true;

            investmentBtnDisabled.SetActive(false);
        }

        if(investmentInsurance == true)
        {
            insuranceGreyed.SetActive(false);
            insuranceActive.SetActive(true);
        }
    }

    public void OnMouseDown()
    {
        doNutBrowser.SetActive(true);
        crossButton.SetActive(true);

        Debug.Log("BROWSER OPEN");
    }

    public void CloseWindow()
    {
        doNutBrowser.SetActive(false);
        crossButton.SetActive(false);

        Debug.Log("BROWSER CLOSE");
    }

    public void BuyAccidentInsurance()
    {
        GM.currentMoney = GM.money;

        Debug.Log(accidentInsurance);

        accidentInsurance = true;

        if(accidentInsurance == true)
        {
            accidentGreyed.SetActive(false);
            accidentActive.SetActive(true);

            accidentBtnDisabled.SetActive(true);

            GM.money -= 5f;

            StartCoroutine(MoneyTrueToFalse(2));

            accidentInsuranceCfm.SetActive(false);

            GM.StartCoroutine(GM.AnimateMoneySlider());

            Debug.Log(GM.moneySlider.value);
            accidentBtn.enabled = false;
        }
        else
        {
            accidentBtn.enabled = true;

            accidentBtnDisabled.SetActive(false);

            accidentGreyed.SetActive(true);
            accidentActive.SetActive(false);
        }

        GM.moneySlider.value = GM.money;
    }

    public void BuyHealthInsurance()
    {
        GM.currentMoney = GM.money;

        Debug.Log(healthInsurance);

        healthInsurance = true;

        if (healthInsurance == true)
        {
            healthGreyed.SetActive(false);
            healthActive.SetActive(true);

            healthBtnDisabled.SetActive(true);

            GM.money -= 5f;

            StartCoroutine(MoneyTrueToFalse(2));

            GM.StartCoroutine(GM.AnimateMoneySlider());

            healthInsuranceCfm.SetActive(false);

            Debug.Log(GM.moneySlider.value);
            healthBtn.enabled = false;
        }
        else
        {
            healthBtn.enabled = true;

            healthBtnDisabled.SetActive(false);

            healthGreyed.SetActive(true);
            healthActive.SetActive(false);
        }

        GM.moneySlider.value = GM.money;
    }

    public void OpenAccidentConfirmation()
    {
        if(accidentInsurance != true)
        {
            accidentInsuranceCfm.SetActive(true);
        }
    }

    public void OpenHealthConfirmation()
    {
        healthInsuranceCfm.SetActive(true);
    }

    public void OpenInvestmentConfirmation()
    {
        investmentInsuranceCfm.SetActive(true);
    }

    public void CloseInvestmentConfirmation()
    {
        investmentInsuranceCfm.SetActive(false);
    }

    public void CloseAccidentConfirmation()
    {
        accidentInsuranceCfm.SetActive(false);
    }

    public void CloseHealthConfirmation()
    {
        healthInsuranceCfm.SetActive(false);
    }

    public void BuyInvestmentInsurance()
    {
        GM.currentMoney = GM.money;

        Debug.Log(investmentInsurance);

        investmentInsurance = true;

        if (investmentInsurance == true)
        {
            insuranceGreyed.SetActive(false);
            insuranceActive.SetActive(true);

            investmentBtnDisabled.SetActive(true);

            GM.money -= 5f;

            investmentInsuranceCfm.SetActive(false);

            StartCoroutine(MoneyTrueToFalse(2));

            GM.StartCoroutine(GM.AnimateMoneySlider());

            Debug.Log(GM.moneySlider.value);
            investmentBtn.enabled = false;
        }
        else
        {
            investmentBtn.enabled = true;

            investmentBtnDisabled.SetActive(false);

            insuranceGreyed.SetActive(true);
            insuranceActive.SetActive(false);
        }

        GM.moneySlider.value = GM.money;
    }

    public void OpenWebsite()
    {
        Application.OpenURL("https://www.prudential.com.sg/");
    }

    IEnumerator MoneyTrueToFalse(float timer)
    {
        GM.minusMoney.SetActive(true);

        yield return new WaitForSeconds(2);

        GM.minusMoney.SetActive(false);
    }
}
