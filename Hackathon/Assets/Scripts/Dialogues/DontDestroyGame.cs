using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyGame : MonoBehaviour
{
    public static DontDestroyGame instance;

    public Animator transitionAnim;

    public float transitionTime = 1f;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void RestartCurrentScene()
    {
        StartCoroutine(TransitionNextLevel());
    }

    IEnumerator TransitionNextLevel()
    {
        transitionAnim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        // Clean up objects marked with DontDestroyOnLoad
        GameObject[] persistentObjects = GameObject.FindGameObjectsWithTag("Persistent");
        foreach (GameObject obj in persistentObjects)
        {
            Destroy(obj);
        }

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void DestroyGame()
    {
        Destroy(gameObject);
    }
}
