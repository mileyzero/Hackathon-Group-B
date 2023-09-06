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
        if(collision.tag == "doodle") //if the player collides with the death zone then player will die
        {
            losescreen.SetActive(true);
            Debug.Log("Die");

            StartCoroutine(TransitionToMainGame(1.5f));
        }

        if(collision.tag == "platform") //if the platform collides with the death zone then destory the platforms
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

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().hasPlayedLevel1 = false;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().hasPlayedLevel2 = false;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().hasPlayedLevel3 = false;

        SceneManager.LoadScene("SampleScene");
    }
}
