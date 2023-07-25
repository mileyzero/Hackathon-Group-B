using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Control : MonoBehaviour
{
    public static event Action ButtonPressed = delegate { };

    public TextMeshProUGUI result;

    public Animator animator;

    public Row[] rows;

    private string prizeValue;

    private bool resultsChecked = false;

    public GameObject arrow;

    // Update is called once per frame
    void Update()
    {
        if (!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped)
        {
            prizeValue = null;
            result.enabled = false;
            resultsChecked = false;
        }

        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultsChecked)
        {
            CheckResults();
            result.enabled = true;
            result.text = prizeValue;
        }
    }

    
    private void OnMouseDown()
    {
        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped)
        {
            arrow.SetActive(false);
            animator.SetTrigger("Pressed");
            ButtonPressed();
        }
    }
    

    private void CheckResults()
    {
        if (rows[0].stoppedslot == "Happy" && rows[1].stoppedslot == "Happy" && rows[2].stoppedslot == "Happy")
        {
            prizeValue = "+ Employee Happiness";
        }

        else if (rows[0].stoppedslot == "Popularity" && rows[1].stoppedslot == "Popularity" && rows[2].stoppedslot == "Popularity")
        {
            prizeValue = "+ Popularity";
        }

        else if (rows[0].stoppedslot == "Money" && rows[1].stoppedslot == "Money" && rows[2].stoppedslot == "Money")
        {
            prizeValue = "+ Money";
        }

        else if (rows[0].stoppedslot == "7" && rows[1].stoppedslot == "7" && rows[2].stoppedslot == "7")
        {
            prizeValue = "+ All Resources";
        }

        resultsChecked = true;
        arrow.SetActive(true);
    }
}
