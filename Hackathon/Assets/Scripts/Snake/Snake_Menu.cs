using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake_Menu : MonoBehaviour
{
    public GameObject _snakegame;

    public Animator snekPlayAnim;
    public Animator snekQuitAnim;

    //StartGame plays with coroutine
    public void StartGame()
    {
        StartCoroutine(PlayAnim(1f));
    }

    //QuitGame plays with coroutine
    public void QuitGame()
    {
        StartCoroutine(QuitAnim(1f));
    }

    //QuitAnim sets an animation trigger called 'Quit' which then gets WaitForSeconds with a float timer.
    //After the timer has ended, it finds the main game by its tag and sets it to true and setting the snake game to false
    //Which then loads the main game scene
    IEnumerator QuitAnim(float timer)
    {
        snekQuitAnim.SetTrigger("Quit");

        yield return new WaitForSeconds(timer);

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>()._maingame.gameObject.SetActive(true);
        this.gameObject.SetActive(false);

        SceneManager.LoadScene(0);
    }

    //PlayAnim sets an animation trigger called 'Play' which then gets WaitForSeconds with a float timer.
    //After the timer has ended, snakeGame sets active to true and the snake menu to false
    IEnumerator PlayAnim(float timer)
    {
        snekPlayAnim.SetTrigger("Play");

        yield return new WaitForSeconds(timer);

        Debug.Log("Click");
        _snakegame.SetActive(true);
        this.gameObject.SetActive(false);
    }
}