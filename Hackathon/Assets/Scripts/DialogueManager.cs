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

    public string[] dialogueLines;

    private int dialogueIndex;

    private bool isDialogue;

    public GameObject yesButton;
    public GameObject noButton;
    public GameObject nameBox;

    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        yesButton.SetActive(false);
        noButton.SetActive(false);

        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Dialogue Playing");

        if (dialogueText.text != dialogueLines[dialogueIndex])
        {
            isDialogue = false;
        }

        if (dialogueText.text == dialogueLines[dialogueIndex])
        {
            isDialogue = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(dialogueText.text == dialogueLines[dialogueIndex])
            {
                isDialogue = true;
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[dialogueIndex];
            }
        }
    }

    private void StartDialogue()
    {
        dialogueIndex = 0;

        if (isDialogue == false)
        {
            StartCoroutine(DialogueStart());
        }
    }

    private IEnumerator DialogueStart()
    {
        while (isDialogue == false)
        {
            //to type characters 1 by 1
            foreach (char c in dialogueLines[dialogueIndex].ToCharArray())
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(typingSpeed);

                yesButton.SetActive(true);
                noButton.SetActive(true);
            }
        }
    }

    public void ButtonClick()
    {
        gameObject.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);


        nameBox.SetActive(false);
        nameGen.nameBox.GetComponent<TextMeshProUGUI>().text = null;

        isDialogue = false;
    }
}
