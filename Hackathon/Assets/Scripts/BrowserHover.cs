using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BrowserHover : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        // Attach the Animator component of the button's parent GameObject
        animator = GetComponentInParent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse entered the button's area");
        // Perform actions, such as changing appearance or starting animations

        // Trigger the animation by setting the Boolean parameter
        animator.SetBool("IsHighlighted", true);
    }

    public void OnPointerExit()
    {
        // Reset the Boolean parameter to its initial state
        animator.SetBool("IsHighlighted", false);
    }
}
