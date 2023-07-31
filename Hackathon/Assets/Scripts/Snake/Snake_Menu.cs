using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake_Menu : MonoBehaviour
{
    public GameObject _snakegame;

    public void StartGame()
    {
        Debug.Log("Click");
        _snakegame.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
}