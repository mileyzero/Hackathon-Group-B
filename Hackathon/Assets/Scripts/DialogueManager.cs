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

    //Reference script to nameGen
    public NameGenerator nameGen;

    public TextMeshProUGUI dialogueText;

    //string arrays for employee, investment and insurance dialogue lines
    public string[] employeeLines = new string[7];
    public string[] investmentLines = new string[10];
    public string[] insuranceLines = new string[2];

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

        employeeLines[0] = "Hey Boss! One of your employees is having their birthday TODAY. Would you like to gift a present?";
        employeeLines[1] = "Good Morning Boss! One of your senior employees would like to see you about a promotion. Would you like me to send them in to discuss his possible promotion?";
        employeeLines[2] = "Hi Boss, One of our employees has reported sick, would you like to help out by paying for his/her medical fees?";
        employeeLines[3] = "Hi Boss! Good news, one of our employees has been working hard lately, as a token of appreciation, would you like to provide them with holiday money this year?";
        employeeLines[4] = "Hi Boss, Bad news, one of our employees has gotten into an accident, would you like to provide them with financial assistance to cover their medical bills?";
        employeeLines[5] = "Good Morning Boss! In regards for our employees' workspace, would you like to provide them financial assistance to upgrade?";
        employeeLines[6] = "Happy New Year Boss! would you like to host a New Year Party for your employees?";

        insuranceLines[0] = "BOSS! I received a call regarding one of your warehouse! An accident occurred, which caused a fire. They said not many of the items are damaged, do you want to repair the warehouse?";
        insuranceLines[1] = "Hi Boss! I received a message from employee claiming to be under our company, said that his/her car has broken down and is in need of money, do you want to provide him/her any financial assistance?";

        investmentLines[0] = "Hi Boss! Our accountants have noticed that we have a surplus in capital. They suggested that you expand the business and offices. Would you like to follow through?";
        investmentLines[1] = "Hey Pal! Heard your business has been thriving. I'm writing to ask you whether you would like to invest in one business project. You will receive a good margin of the profits!";
        investmentLines[2] = "Hi. Would you like to provide some funds for my start-up business? We will pay you handsomely once things start to pick up.";
        investmentLines[3] = "Hi, I am a representative of an Energy Company called Operate Energy. Our partnership will help us harness and distribute energy. Would you like to invest?";
        investmentLines[4] = "Hey there! I'm the developer of Among Us, and I could really use some financial support to help me develop this game! Would you help me?";
        investmentLines[5] = "Hi! I would like to provide an upgrade of ads to your company! Do you want some traction for your ads?";
        investmentLines[6] = "Hey there! Want to be the face of our awesome brand? We're hiring models to help us advertise - interested?";
        investmentLines[7] = "Hi! I am a representative of a clothing brand called Doo Nut, we would be thrilled to offer you a deal with our clothing brand - are you interested?";
        investmentLines[8] = "Hi, I would like to catch everyone's attention and spread your company's name by advertising it on a billboard! Are you interested?";
        investmentLines[9] = "Hi, I would like to provide services to upgrade your company's office area, are you interested?";


        dialogueBox.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    string GetRandomInvestDialogue()
    {
        //chooses random dialogue in employeeLines
        //then returns which employeeLines was chosen from randomDialogue randomizer
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

    string GetRandomInsuranceDialogue()
    {
        //chooses random dialogue in employeeLines
        //then returns which employeeLines was chosen from randomDialogue randomizer
        if (isDialogue == false && dialoguePlayed == false)
        {
            int randomInsuranceDialogue = Random.Range(0, insuranceLines.Length);
            Debug.Log(randomInsuranceDialogue);
            return insuranceLines[randomInsuranceDialogue];
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
