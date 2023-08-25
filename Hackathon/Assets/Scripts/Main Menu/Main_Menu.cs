using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class Main_Menu : MonoBehaviour
{
    public Animator transitionAnim;

    public AudioSource mainAudio;

    public GameObject playBtn;
    public GameObject settingBtn;
    public GameObject quitBtn;

    public GameObject backBtn;
    public GameObject fullscreenBtn;

    public Image toggleImage;

    public Sprite fullscreenOn;
    public Sprite fullscreenOff;

    public float transitionTime = 1f;

    public bool isFullscreen = false;

    // Start is called before the first frame update
    void Start()
    {
        fullscreenBtn.SetActive(false);

        backBtn.SetActive(false);
        mainAudio.Play();
    }

    public void BackBtnPressed()
    {
        playBtn.SetActive(true);
        settingBtn.SetActive(true);
        quitBtn.SetActive(true);

        backBtn.SetActive(false);
        fullscreenBtn.SetActive(false);
    }

    public void SetFullScreen()
    {
        isFullscreen = !isFullscreen;

        if (isFullscreen)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
            Debug.Log(Screen.currentResolution);
        }
        else
        {
            int windowHeight = 720;
            int windowWidth = 1280;

            Screen.SetResolution(windowWidth, windowHeight, false);
        }

        UpdateToggleImage();
    }

    private void UpdateToggleImage()
    {
        if (isFullscreen)
        {
            toggleImage.sprite = fullscreenOn;
        }
        else
        {
            toggleImage.sprite = fullscreenOff;
        }
    }

    public void SettingsBtnPressed()
    {
        playBtn.SetActive(false);
        settingBtn.SetActive(false);
        quitBtn.SetActive(false);

        backBtn.SetActive(true);
        fullscreenBtn.SetActive(true);
    }

    public void PlayBtnPressed()
    {
        playBtn.SetActive(false);
        settingBtn.SetActive(false);
        quitBtn.SetActive(false);
    }

    public void QuitBtnGame()
    {
        Application.Quit();
    }

    public void NextScene()
    {
        StartCoroutine(TransitionNextLevel());
    }

    IEnumerator TransitionNextLevel()
    {
        transitionAnim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(0);
    }
}
