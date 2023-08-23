using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Collect : MonoBehaviour
{
    public AudioSource collectsound;
    public int moneyCollected;
    public int popularCollected;
    public int happyCollected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "doodleMoney")
        {
            collectsound.Play();
            moneyCollected += 1;
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "doodlePopular")
        {
            collectsound.Play();
            popularCollected += 1;
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "doodleHappy")
        {
            collectsound.Play();
            happyCollected += 1;
            Destroy(collision.gameObject);
        }
    }
}
