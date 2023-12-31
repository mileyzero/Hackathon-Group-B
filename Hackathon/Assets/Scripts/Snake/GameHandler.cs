using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] Snake snake;
    private Level_Grid levelgrid;
    public GameObject pauseMenu;

    public GameObject guide;
    // Start is called before the first frame update
    void Start()
    {
        levelgrid = new Level_Grid(19,19); 
        guide.SetActive(true); //turn on the tutorial
        snake.SetUp(levelgrid); //setting up the grid for the snake game
        levelgrid.SetUp(snake);

        Time.timeScale = 0f;
    }

    public void Start_Click() //when player clicks the start button
    {
        guide.SetActive(false);
        Time.timeScale = 1f;
    }
    public void PauseMenu() //when player clicks the pause button
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume() //when player click resume
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
