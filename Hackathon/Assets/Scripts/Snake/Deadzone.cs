using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadzone: MonoBehaviour
{
    public Snake snake;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "snake")
        {
            Debug.Log("Die");
            snake.Die();   
        }
    }
}
