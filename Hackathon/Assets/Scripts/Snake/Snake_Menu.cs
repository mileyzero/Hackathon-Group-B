using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake_Menu : MonoBehaviour
{
    public GameObject _snakegame;

    public Animator snekPlayAnim;
    public Animator snekQuitAnim;

    public void StartGame()
    {
        StartCoroutine(PlayAnim(1f));
    }

    public void QuitGame()
    {
        StartCoroutine(QuitAnim(1f));
    }

    IEnumerator QuitAnim(float timer)
    {

        snekQuitAnim.SetTrigger("Quit");

        yield return new WaitForSeconds(timer);

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>()._maingame.gameObject.SetActive(true);
        this.gameObject.SetActive(false);

        SceneManager.LoadScene(0);
    }

    IEnumerator PlayAnim(float timer)
    {
        snekPlayAnim.SetTrigger("Play");

        yield return new WaitForSeconds(timer);

        Debug.Log("Click");
        _snakegame.SetActive(true);
        this.gameObject.SetActive(false);
    }
}