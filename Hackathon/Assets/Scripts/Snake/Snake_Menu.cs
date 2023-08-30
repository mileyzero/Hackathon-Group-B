using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake_Menu : MonoBehaviour
{
    public float transitionTime = 2f;

    public GameObject _snakegame;

    public Animator snekPlayAnim;
    public Animator snekQuitAnim;

    public Animator transitionAnim;

    //StartGame plays with coroutine
    public void StartGame()
    {
        StartCoroutine(TransitionNextLevel());
    }

    //QuitGame plays with coroutine
    public void QuitGame()
    {
        StartCoroutine(TransitionQuitLevel());
    }

    IEnumerator TransitionNextLevel()
    {
        snekPlayAnim.SetTrigger("Play");
        transitionAnim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        Debug.Log("Click");
        _snakegame.SetActive(true);
        this.gameObject.SetActive(false);
    }

    IEnumerator TransitionQuitLevel()
    {
        snekQuitAnim.SetTrigger("Quit");
        transitionAnim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>()._maingame.gameObject.SetActive(true);
        this.gameObject.SetActive(false);

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().hasPlayedLevel1 = false;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().hasPlayedLevel2 = false;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().hasPlayedLevel3 = false;

        SceneManager.LoadScene("SampleScene");
    }
}