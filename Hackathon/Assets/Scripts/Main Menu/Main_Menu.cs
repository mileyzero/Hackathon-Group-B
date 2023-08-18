using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class Main_Menu : MonoBehaviour
{
    public string mainSceneName = "Main Menu";

    public GameObject playBtn;
    public GameObject settingBtn;
    public GameObject quitBtn;

    public GameObject backBtn;
    public GameObject fullscreenBtn;
    public GameObject volumeSlider;

    public GameObject loadingBar;
    public GameObject loadingScreen;
    public GameObject loadingText;

    public GameObject clickContinueBtn;

    public Slider volumeSliderSet;
    public Slider sliderLoad;

    public AudioMixer mainAudio;

    public TextMeshProUGUI progressText;

    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.SetActive(false);
        fullscreenBtn.SetActive(false);

        loadingBar.SetActive(false);
        loadingText.SetActive(false);

        clickContinueBtn.SetActive(false);

        backBtn.SetActive(false);
    }

    /*IEnumerator LoadMainSceneAsynchronous()
    {
        yield return null;

        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(mainSceneName);
        asyncOp.allowSceneActivation = false;

        while (!asyncOp.isDone)
        {
            float progress = Mathf.Clamp01(asyncOp.progress / .9f);

            sliderLoad.value = progress;
            progressText.text = progress * 100f + "%";

            if (asyncOp.progress >= 0.9f)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    clickContinueBtn.SetActive(true);
                    asyncOp.allowSceneActivation = true;
                }
            }
        }
    }*/

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
        playBtn.SetActive(false);
        settingBtn.SetActive(false);
        quitBtn.SetActive(false);

        StartCoroutine(LoadMainSceneAsynchronous());
    }

    public void QuitBtnGame()
    {
        Application.Quit();
    }

    public void NextScene()
    {
        SceneManager.LoadScene(0);
    }
}
