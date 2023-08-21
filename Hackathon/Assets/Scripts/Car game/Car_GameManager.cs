using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_GameManager : MonoBehaviour
{

    public GameObject pauseMenu;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }
    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
