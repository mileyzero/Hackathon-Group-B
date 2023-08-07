using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCone : MonoBehaviour
{
    public GameObject cone;
    //this will destroy the obstacles when they enter the destroyer
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "cone")
        {
            Destroy(collision.gameObject);
        }
    }
}
