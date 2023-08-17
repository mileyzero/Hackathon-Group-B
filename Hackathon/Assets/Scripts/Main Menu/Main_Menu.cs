using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Main_Menu : MonoBehaviour
{
    public GameObject playBtn;
    public GameObject settingBtn;
    public GameObject quitBtn;

    public GameObject volumeSlider;

    public GameObject backBtn;
    public GameObject fullscreenBtn;

    public Slider volumeSliderSet;

    public AudioMixer mainAudio;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.SetActive(false);
        fullscreenBtn.SetActive(false);

        backBtn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackBtnPressed()
    {
        SceneManager.LoadScene("Main Menu");

        playBtn.SetActive(true);
        settingBtn.SetActive(true);
        quitBtn.SetActive(true);

        volumeSlider.SetActive(false);
        backBtn.SetActive(false);
        fullscreenBtn.SetActive(false);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SettingsBtnPressed()
    {
        playBtn.SetActive(false);
        settingBtn.SetActive(false);
        quitBtn.SetActive(false);

        volumeSlider.SetActive(true);
        backBtn.SetActive(true);
        fullscreenBtn.SetActive(true);
    }

    public void SetVolume(float volume)
    {
        mainAudio.SetFloat("volume", volume);
    }

    public void PlayBtnPressed()
    {
        SceneManager.LoadScene("Loading Scene");
    }

    public void QuitBtnGame()
    {
        Application.Quit();
    }
}
