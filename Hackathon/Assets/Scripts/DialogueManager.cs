using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    #region Text Variables
    public Holiday holidayManager;
    public Investment investManager;

    public NameGenerator nameGen;

    public TextMeshProUGUI dialogueText;

    public string[] employeeLines = new string[5];
    public string[] investmentLines = new string[5];
    public string[] insuranceLines = new string[5];

    //Insurance negate dialogue
    public string[] healthEmployeeLines = new string[5];

    public bool isDialogue;
    public bool dialoguePlayed;

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

        insuranceLines[0] = "BOSS! I received a call regarding one of your warehouse! An accident occurred, which caused a fire, and it is spreading rapidly. They are currently doing what they can to keep as many things as safe as possible.";

        investmentLines[0] = "Hi Boss! Our accountants have noticed that we have a surplus in capital. They suggested that you expand the business and offices. Would you like to follow through?";
        investmentLines[1] = "Hey Pal! Heard your business has been thriving. I'm writing to ask you whether you would like to invest in one business project. You will receive a good margin of the profits!";

        dialogueBox.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
            return employeeLines[randomInvestDialogue];
        }
        return null;
    }

    string GetRandomEmployeeDialogue()
    {
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

    public void ButtonClick()
    {
        gameObject.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);

        holidayManager.scenarioButton.enabled = false;

        nameBox.SetActive(false);
        nameGen.nameBox.GetComponent<TextMeshProUGUI>().text = null;

        isDialogue = false;
        dialoguePlayed = false;
    }
}
