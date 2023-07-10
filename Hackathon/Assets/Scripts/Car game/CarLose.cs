using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLose : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "car")
        {
            Debug.Log("Lose");
        }
    }
}
