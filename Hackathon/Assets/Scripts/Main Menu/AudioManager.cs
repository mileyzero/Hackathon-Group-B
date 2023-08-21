using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource mainAudioSource;

    public AudioClip mainMenuAudio;

    public AudioClip doodleJumpAudio;
    public AudioClip golfAudio;
    public AudioClip carAudio;
    public AudioClip casinoAudio;
    public AudioClip snakeAudio;

    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get the AudioManager instance and play the music.
        AudioManager audioManager = FindObjectOfType<AudioManager>();

        if (audioManager != null)
        {
            audioManager.PlayMusic(mainMenuAudio);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void PlayMusic(AudioClip musicClip)
    {
        mainAudioSource.clip = musicClip;
        mainAudioSource.Play();
    }

    public void StopMusic()
    {
        mainAudioSource.Stop();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Handle music for different scenes here.
        AudioClip musicClipToPlay = GetMusicClipForScene(scene.name);
        PlayMusic(musicClipToPlay);
    }

    private AudioClip GetMusicClipForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "Main Menu":
                return mainMenuAudio;

            case "Car":
                return carAudio;

            case "DoodleJump":
                return doodleJumpAudio;

            case "Golf":
                return golfAudio;

            case "SlotMachine":
                return casinoAudio;

            case "Snake":
                return snakeAudio;

            default:
                return mainMenuAudio;
        }
    }

}
