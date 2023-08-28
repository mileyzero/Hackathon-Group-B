using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeWritingEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent;

    public GameObject nextBtn;

    public string[] textLines;
    public float textSpeed;

    private int index;

    private void Start()
    {
        nextBtn.SetActive(false);

        textComponent.text = string.Empty;
        StartDialogue();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(textComponent.text == textLines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = textLines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach(char c in textLines[index].ToCharArray())
        {
            nextBtn.SetActive(false);

            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if(index < textLines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            nextBtn.SetActive(true);
        }
    }
}
