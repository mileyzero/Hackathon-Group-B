using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    public GameObject losescreen;
    public GameObject doodle_manager;

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
        doodle_manager.GetComponent<AudioSource>().enabled = false;
        yield return new WaitForSeconds(1.5f);

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>()._maingame.gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("cooldown").GetComponent<MiniGameTimer>().StartCooldown();

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.hasPlayedLevel1 = false;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.hasPlayedLevel2 = false;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.hasPlayedLevel3 = false;

        SceneManager.LoadScene(0);
    }
}
