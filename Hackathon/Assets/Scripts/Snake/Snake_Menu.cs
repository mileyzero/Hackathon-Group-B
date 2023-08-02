using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake_Menu : MonoBehaviour
{
    public GameObject _snakegame;

    public Animator snakePlayAnim;
    public Animator snakeQuitAnim;

    private void Start()
    {

    }

    public void StartGame()
    {
        StartCoroutine(playSnake(2));
    }

    IEnumerator playSnake(float timer)
    {
        snakePlayAnim.SetTrigger("Play");

        yield return new WaitForSeconds(timer);

        Debug.Log("Click");
        _snakegame.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        StartCoroutine(quitSnake(2));
    }

    IEnumerator quitSnake(float timer)
    {
        snakeQuitAnim.SetTrigger("Quit");

        yield return new WaitForSeconds(timer);

        GameObject.FindGameObjectWithTag("main_game").SetActive(true);
        SceneManager.LoadScene(0);
    }
}