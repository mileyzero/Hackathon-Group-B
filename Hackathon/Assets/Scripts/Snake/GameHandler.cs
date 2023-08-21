using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] Snake snake;
    private Level_Grid levelgrid;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        levelgrid = new Level_Grid(19,19);

        snake.SetUp(levelgrid);
        levelgrid.SetUp(snake);
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
