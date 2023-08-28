using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Collect : MonoBehaviour
{
    public AudioSource collectsound;
    public int moneyCollected;
    public int popularCollected;
    public int happyCollected;

    //This script handles the collision between resources and the player
    private void OnTriggerEnter2D(Collider2D collision) //a value will to added to the related variable when the player collides with the different resources
    {
        if (collision.gameObject.tag == "doodleMoney") //collision with money
        {
            collectsound.Play();
            moneyCollected += 1;
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "doodlePopular") //collision with popularity
        {
            collectsound.Play();
            popularCollected += 1;
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "doodleHappy") //collision with happiness
        {
            collectsound.Play();
            happyCollected += 1;
            Destroy(collision.gameObject);
        }
    }
}
