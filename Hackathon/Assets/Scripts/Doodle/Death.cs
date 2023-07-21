using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{

    public GameObject losescreen;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "doodle")
        {
            losescreen.SetActive(true);
            Debug.Log("Die");
        }

        if(collision.tag == "platform")
        {
            Destroy(collision.gameObject);
        }
    }
}
