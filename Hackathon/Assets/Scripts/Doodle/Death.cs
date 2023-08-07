using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public MiniGameTimer mgTimer;

    public GameObject losescreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "doodle")
        {
            losescreen.SetActive(true);
            Debug.Log("Die");

            StartCoroutine(TransitionToMainGame(1.5f));
        }

        if(collision.tag == "platform")
        {
            Destroy(collision.gameObject);
        }
    }

    IEnumerator TransitionToMainGame(float timer)
    {
        yield return new WaitForSeconds(1.5f);

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>()._maingame.gameObject.SetActive(true);

        mgTimer.Timer();
    }
}
