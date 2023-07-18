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

    public float typingSpeed = 0.04f;

    public TextMeshProUGUI dialogueText;

    public string[] employeeLines = new string[10];
    public string[] investmentLines = new string[10];
    public string[] insuranceLines = new string[10];

    private int dialogueIndex;

    private bool isDialogue;

    public GameObject yesButton;
    public GameObject noButton;
    public GameObject nameBox;
    public GameObject dialogueBox;

    #endregion

    // Start is called before the first frame update
    private void Start()
    {

        employeeLines[0] = "Hey Boss! One of your employees is having their birthday TODAY. Would you like to gift a present?";
        employeeLines[1] = "Good Morning Boss! One of your senior employees would like to see you about a promotion. Would you like me to send them in to discuss his possible promotion?";



        dialogueBox.SetActive(true);
        yesButton.SetActive(false);
        noButton.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        if (dialogueText.text != employeeLines[dialogueIndex])
        {
            isDialogue = false;
        }

        if (dialogueText.text == employeeLines[dialogueIndex])
        {
            isDialogue = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(dialogueText.text == employeeLines[dialogueIndex])
            {
                isDialogue = true;
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = employeeLines[dialogueIndex];
            }
        }

    }

    public void CallRandomEmployee()
    {
        GetRandomEmployee();
    }

    public string GetRandomEmployee()
    {
        StartDialogue();

        //chooses random dialogue in employeeLines
        //then returns which employeeLines was chosen from randomDialogue randomizer
        int randomDialogue = Random.Range(0, employeeLines.Length);
        return employeeLines[randomDialogue];
    }

    private void StartDialogue()
    {
        //if isDialogue bool equals to false, StartsCoroutine of DialogueStart
        if (isDialogue == false)
        {
            StartCoroutine(DialogueStart());
        }
    }

    private IEnumerator DialogueStart()
    {
        while(isDialogue == false)
        {
            yield return new WaitForSeconds(typingSpeed);

            dialogueBox.SetActive(true);
            yesButton.SetActive(true);
            noButton.SetActive(true);
        }
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
    }
}
