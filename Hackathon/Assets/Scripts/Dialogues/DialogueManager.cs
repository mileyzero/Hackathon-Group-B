using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    #region Text Variables
    //Reference script to holidayManager
    public Holiday holidayManager;

    //Reference script to investManager
    public Investment investManager;

    //Reference script to insuranceManager
    public Insurance insuranceManager;

    //Reference script to accidentManager
    public Accident accidentManager;

    //Reference script to healthManager
    public Health healthManager;

    //Reference script to MGManager
    public MG_Dialogue MGManager;

    //Reference script to nameGen
    public NameGenerator nameGen;

    public TextMeshProUGUI dialogueText;

    //string arrays for employee, investment and insurance dialogue lines
    public string[] employeeLines = new string[5];
    public string[] investmentLines = new string[10];

    public string[] insuranceHealthLines = new string[1];
    public string[] insuranceInvestmentLines = new string[1];
    public string[] insuranceAccidentLines = new string[1];

    public string[] healthLines = new string[5];
    public string[] accidentLines = new string[5];

    public string[] miniGameLines = new string[1];
    public string[] miniGameCarLines = new string[1];
    public string[] miniGameGolfLines = new string[1];

    //isDialogue bool to check if dialogue is active
    public bool isDialogue;

    //dialoguePlayed bool to check if dialogue is playing
    public bool dialoguePlayed;

    //GameObjects for scenario
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject nameBox;
    public GameObject dialogueBox;

#endregion

    // Start is called before the first frame update
    private void Start()
    {
        isDialogue = false;
        dialoguePlayed = false;

        employeeLines[0] = "Employee Birthday\n \nHello Boss,\nOne of your employees is going to have their birthday TODAY. Would you like to gift a present?";
        employeeLines[1] = "Employee Promotion\n \nGood Morning Boss,\nOne of your senior employees would like to see you about a promotion. Would you like me to send them in to discuss his possible promotion?";
        employeeLines[2] = "Employee Motivation\n \nHello Boss, \nGood news,Our employees have been working hard lately, as a token of appreciation, would you like to provide them with holiday money this year?";
        employeeLines[3] = "Employee Workspace\n \nGood Morning Boss,\nIn regards for our employees' workspace, would you like to provide them with a better workspace?";
        employeeLines[4] = "Employee New Year Party\n \nHappy New Year Boss!,\nWould you like to host a New Year Party for your employees?";

        insuranceHealthLines[0] = "Health Insurance Oppurtunity\n \nGood Morning Boss!,\nWould you like to buy Health Insurance at a lower rate to keep your Employees from unexpected medical expenses?";
        insuranceAccidentLines[0] = "Accident Insurance Oppurtunity\n \nGood Morning Boss!,\nWould you like to buy Accident Insurance at a lower rate to keep your Employees from unexpected accident expenses?";
        insuranceInvestmentLines[0] = "Investment Insurance Oppurtunity\n \nGood Morning Boss!,\nWould you like to buy an Investment Insurance at a lower rate to keep yourself safe from scams?";

        investmentLines[0] = "Investment Oppurtunity\n \nHi Boss!,\nOur accountants have noticed that we have a surplus in capital. They suggested that you should expand the business and offices. Would you like to follow through?";
        investmentLines[1] = "Investment Oppurtunity\n \nHello!,\nHeard your business has been thriving. I am writing to ask you whether you would like to invest in one of our business project. You will receive a good margin of the profits!";
        investmentLines[2] = "Investment Oppurtunity\n \nHi!,\nWould you like to provide some funds for my start-up business? We will pay you handsomely once things start to pick up.";
        investmentLines[3] = "Investment Oppurtunity\n \nHi!,\nI am a representative of an Energy Company called Operate Energy. Our partnership will help us harness and distribute energy. Would you like to provide us with some finicial assistance?";
        investmentLines[4] = "Investment Oppurtunity\n \nHi Sir!,\nI am developing a game called Among Them, and I could really use some financial support to help me develop this game! Would you like to provide me with some funds to complete this game of mine?";
        investmentLines[5] = "Investment Oppurtunity\n \nHi Mr!,\nI would like to provide an upgrade of ADs to your company! Do you want some traction for your ADs?";
        investmentLines[6] = "Investment Oppurtunity\n \nHey there!,\nWant to be the face of our AWESOME BRAND? We are hiring models to help us advertise - Interested?";
        investmentLines[7] = "Investment Oppurtunity\n \nHello!,\nI am a representative of a clothing brand called Doo Nut, we would be thrilled to offer you a deal with our clothing brand - Are you Interested?";
        investmentLines[8] = "Investment Oppurtunity\n \nHi!,\nI would like to catch everyone's attention and spread your company's name by advertising it on a billboard! Are you interested?";
        investmentLines[9] = "Investment Oppurtunity\n \nHi!,\nI would like to provide services to upgrade your company's office area, are you interested?";

        healthLines[0] = "Medical Incident\n \nHi Boss,\nOne of our employees has reported sick, would you like to help out with his/her medical fees?";
        healthLines[1] = "Medical Incident\n \nGood Morning Boss,\nOne of our departments has recently got in contact with COVID, would you like to help out by paying for the medical fees?";
        healthLines[2] = "Medical Incident\n \nHi Sir,\nI have submitted a medical bill for reimbursement, can I have an approval to proceed with the payment?";
        healthLines[3] = "Medical Incident\n \nHi Boss,\nthis is urgent! I need your help with some financial help as I can't pay for my medical bills. Please help me!";
        healthLines[4] = "Medical Incident\n \nHello Sir,\nWould you please consider covering the medical fees as it is directly related to my work.";

        accidentLines[0] = "Accident Incident\n \nHi Boss,\nBad news, one of our employees has gotten into an accident, would you like to provide them with financial assistance to cover their medical bills?";
        accidentLines[1] = "Accident Incident\n \nGood Morning Boss,\nOne of our department head has recently got into an accident, would you please help out with the medical fees?";
        accidentLines[2] = "Accident Incident\n \nHi Boss,\nthis is one of your employees from one of the departements, my family have been struggling with some financial stuff, can you help us?";
        accidentLines[3] = "Accident Incident\n \nHi Sir,\nI have submitted an accident bill for reimbursement, can I have an approval to proceed with the payment?";
        accidentLines[4] = "Accident Incident\n \nHello Boss,\nI appreciate your understanding in the matter, but I was wondering if the company could possibly cover the accident fees?";

        miniGameLines[0] = "Mini-Game\n \nHello Boss,\nI highly recommend taking a break to try out the new mini game - it's a great opportunity to relax and boost team morale.";

        miniGameCarLines[0] = "Mini-Game\n \nHello Boss,\nthere's a celebration at 7:30pm at a nearby hotel, it would be an honor for you to join us!";

        miniGameGolfLines[0] = "Mini-Game\n \nHello Boss,\nI have a proposition for you but let's see if your swings in golf is as impressive as you think!";

        dialogueBox.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallMiniGameGolf()
    {
        yesButton.SetActive(true);
        noButton.SetActive(true);

        dialogueText.text = GetRandomMiniGolf();
        dialogueBox.SetActive(true);
    }

    public void CallMiniGameCar()
    {
        yesButton.SetActive(true);
        noButton.SetActive(true);

        dialogueText.text = GetRandomMiniCar();
        dialogueBox.SetActive(true);
    }

    public void CallMiniGameDialogue()
    {
        yesButton.SetActive(true);
        noButton.SetActive(true);

        dialogueText.text = GetRandomMiniGameDialogue();
        dialogueBox.SetActive(true);
    }

    public void CallInsuranceDialogue()
    {
        yesButton.SetActive(true);
        noButton.SetActive(true);

        dialogueText.text = GetRandomInsuranceDialogue();
        dialogueBox.SetActive(true);
    }

    public void CallEmployeeDialogue()
    {
        yesButton.SetActive(true);
        noButton.SetActive(true);

        dialogueText.text = GetRandomEmployeeDialogue();
        dialogueBox.SetActive(true);
    }

    public void CallInvestDialogue()
    {
        yesButton.SetActive(true);
        noButton.SetActive(true);

        dialogueText.text = GetRandomInvestDialogue();
        dialogueBox.SetActive(true);
    }

    public void CallHealthDialogue()
    {
        yesButton.SetActive(true);
        noButton.SetActive(true);

        dialogueText.text = GetRandomHealthDialogue();
        dialogueBox.SetActive(true);
    }

    public void CallAccidentDialogue()
    {
        yesButton.SetActive(true);
        noButton.SetActive(true);

        dialogueText.text = GetRandomAccidentDialogue();
        dialogueBox.SetActive(true);
    }

    public void CallHealthInsuranceDialogue()
    {
        yesButton.SetActive(true);
        noButton.SetActive(true);

        dialogueText.text = GetRandomHealthInsurance();
        dialogueBox.SetActive(true);
    }

    public void CallAccidentInsuranceDialogue()
    {
        yesButton.SetActive(true);
        noButton.SetActive(true);

        dialogueText.text = GetRandomAccidentInsurance();
        dialogueBox.SetActive(true);
    }

    string GetRandomMiniGolf()
    {
        //chooses random dialogue in miniGameGolfLines
        //then returns which miniGameGolfLines was chosen from randomDialogue randomizer
        if (isDialogue == false && dialoguePlayed == false)
        {
            int randomMiniGolfDialogue = Random.Range(0, miniGameGolfLines.Length);
            Debug.Log(randomMiniGolfDialogue);
            return miniGameGolfLines[randomMiniGolfDialogue];
        }
        return null;
    }

    string GetRandomMiniCar()
    {
        //chooses random dialogue in miniGameCarLines
        //then returns which miniGameCarLines was chosen from randomDialogue randomizer
        if (isDialogue == false && dialoguePlayed == false)
        {
            int randomMiniCarDialogue = Random.Range(0, miniGameCarLines.Length);
            Debug.Log(randomMiniCarDialogue);
            return miniGameCarLines[randomMiniCarDialogue];
        }
        return null;
    }

    string GetRandomMiniGameDialogue()
    {
        //chooses random dialogue in miniGameLines
        //then returns which miniGameLines was chosen from randomDialogue randomizer
        if (isDialogue == false && dialoguePlayed == false)
        {
            int randomMiniGameDialogue = Random.Range(0, miniGameLines.Length);
            Debug.Log(randomMiniGameDialogue);
            return miniGameLines[randomMiniGameDialogue];
        }
        return null;
    }

    string GetRandomAccidentDialogue()
    {
        //chooses random dialogue in accidentLines
        //then returns which accidentLines was chosen from randomDialogue randomizer
        if (isDialogue == false && dialoguePlayed == false)
        {
            int randomAccidentDialogue = Random.Range(0, accidentLines.Length);
            Debug.Log(randomAccidentDialogue);
            return accidentLines[randomAccidentDialogue];
        }
        return null;
    }

    string GetRandomHealthDialogue()
    {
        //chooses random dialogue in healthLines
        //then returns which healthLines was chosen from randomDialogue randomizer
        if (isDialogue == false && dialoguePlayed == false)
        {
            int randomHealthDialogue = Random.Range(0, healthLines.Length);
            Debug.Log(randomHealthDialogue);
            return healthLines[randomHealthDialogue];
        }
        return null;
    }

    string GetRandomInvestDialogue()
    {
        //chooses random dialogue in investmentLines
        //then returns which investmentLines was chosen from randomDialogue randomizer
        if (isDialogue == false && dialoguePlayed == false)
        {
            int randomInvestDialogue = Random.Range(0, investmentLines.Length);
            Debug.Log(randomInvestDialogue);
            return investmentLines[randomInvestDialogue];
        }
        return null;
    }

    string GetRandomEmployeeDialogue()
    {
        Debug.Log(isDialogue);
        Debug.Log(dialoguePlayed);

        //chooses random dialogue in employeeLines
        //then returns which employeeLines was chosen from randomDialogue randomizer
        if (isDialogue == false && dialoguePlayed == false)
        {
            int randomEmployeeDialogue = Random.Range(0, employeeLines.Length);
            Debug.Log(randomEmployeeDialogue);
            return employeeLines[randomEmployeeDialogue];
        }
        return null;
    }

    string GetRandomAccidentInsurance()
    {
        //chooses random dialogue in insuranceAccidentLines
        //then returns which insuranceAccidentLines was chosen from randomDialogue randomizer
        if (isDialogue == false && dialoguePlayed == false)
        {
            int randomInsuranceDialogue = Random.Range(0, insuranceAccidentLines.Length);
            Debug.Log(randomInsuranceDialogue);
            return insuranceAccidentLines[randomInsuranceDialogue];
        }
        return null;
    }

    string GetRandomHealthInsurance()
    {
        //chooses random dialogue in insuranceHealthLines
        //then returns which insuranceHealthLines was chosen from randomDialogue randomizer
        if (isDialogue == false && dialoguePlayed == false)
        {
            int randomInsuranceDialogue = Random.Range(0, insuranceHealthLines.Length);
            Debug.Log(randomInsuranceDialogue);
            return insuranceHealthLines[randomInsuranceDialogue];
        }
        return null;
    }

    string GetRandomInsuranceDialogue()
    {
        //chooses random dialogue in insuranceInvestmentLines
        //then returns which insuranceInvestmentLines was chosen from randomDialogue randomizer
        if (isDialogue == false && dialoguePlayed == false)
        {
            int randomInsuranceDialogue = Random.Range(0, insuranceInvestmentLines.Length);
            Debug.Log(randomInsuranceDialogue);
            return insuranceInvestmentLines[randomInsuranceDialogue];
        }
        return null;
    }

    public void ButtonClick()
    {
        gameObject.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);

        nameBox.SetActive(false);
        nameGen.nameBox.GetComponent<TextMeshProUGUI>().text = null;

        isDialogue = false;
        dialoguePlayed = false;
    }
}
