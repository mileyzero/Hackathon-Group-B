using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_GameManager : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject pauseIcon;
    public GameObject resumeImage;

    private void Start()
    {
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().levelAudioChange.Stop();

        pauseMenu.SetActive(false);
        resumeImage.SetActive(false);
        pauseIcon.SetActive(false);
    }
    public void PauseMenu() //function for the pause screen
    {
        pauseMenu.SetActive(true); 
        pauseIcon.SetActive(true);
        Time.timeScale = 0; //time scale to 0 to stop time ingame
    }

    public void Resume() //function to resume game
    {      
        StartCoroutine(ResumeTime());
    }

    IEnumerator ResumeTime()
    {
        pauseIcon.SetActive(false);
        resumeImage.SetActive(true);
        Time.timeScale = 0.1f; //time scale value is 0.1 to slow down the game
        yield return new WaitForSeconds(0.07f);
        resumeImage.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1; //then set back to normal game speed

    }
}
