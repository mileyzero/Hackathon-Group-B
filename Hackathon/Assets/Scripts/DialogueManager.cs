using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public float typingSpeed = 0.04f;

    public TextMeshProUGUI dialogueText;

    public string[] dialogueLines;

    private int dialogueIndex;

    public GameObject yesButton;
    public GameObject noButton;

    // Start is called before the first frame update
    void Start()
    {
        yesButton.SetActive(false);
        noButton.SetActive(false);

        dialogueText.text = string.Empty;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(dialogueText.text == dialogueLines[dialogueIndex])
            {
                
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[dialogueIndex];
            }
        }
    }

    void StartDialogue()
    {
        dialogueIndex = 0;
        StartCoroutine(DialogueStart());
    }

    IEnumerator DialogueStart()
    {
        //to type characters 1 by 1
        foreach(char c in dialogueLines[dialogueIndex].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);

            yesButton.SetActive(true);
            noButton.SetActive(true);
        }
    }

    public void ButtonClick()
    {
        gameObject.SetActive(false);
        yesButton.SetActive(false);
        noButton.SetActive(false);
    }
}
