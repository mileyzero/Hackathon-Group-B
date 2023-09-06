using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadzone: MonoBehaviour
{
    public Snake snake;
    public void OnCollisionEnter2D(Collision2D collision) //if the snake hit the fence/deathzone then the snake will die
    {
        if (collision.gameObject.tag == "snake")
        {
            Debug.Log("Die");
            snake.Defeat_SFX();
            snake.Die();
        }
    }
}
