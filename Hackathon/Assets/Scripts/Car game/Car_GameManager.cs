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
        pauseMenu.SetActive(false);
        resumeImage.SetActive(false);
        pauseIcon.SetActive(false);
    }
    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
        pauseIcon.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {      
        StartCoroutine(ResumeTime());
    }

    IEnumerator ResumeTime()
    {
        pauseIcon.SetActive(false);
        resumeImage.SetActive(true);
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.07f);
        resumeImage.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;

    }
}
