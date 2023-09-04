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

    public Texture2D cursorPixel;
    public Texture2D pressedCursor;

    public Sprite fullscreenOn;
    public Sprite fullscreenOff;

    public float transitionTime = 1f;

    public bool isFullscreen = false;

    private CursorMode _cursorMode = CursorMode.ForceSoftware;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorPixel, Vector2.zero, _cursorMode);

        fullscreenBtn.SetActive(false);

        backBtn.SetActive(false);
        mainAudio.Play();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(pressedCursor, Vector2.zero, _cursorMode);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursorPixel, Vector2.zero, _cursorMode);
        }
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

        SceneManager.LoadScene("SampleScene");
    }
}
