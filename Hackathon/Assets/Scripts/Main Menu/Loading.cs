using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Loading : MonoBehaviour
{
    public GameObject loadingScreen;

    public Slider sliderLoad;
    public TextMeshProUGUI progressText;

    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronous(sceneIndex));
    }

    IEnumerator LoadAsynchronous(int sceneIndex)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(0);

        while (!op.isDone)
        {
            float progress = Mathf.Clamp01(op.progress / .9f);

            sliderLoad.value = progress;
            progressText.text = progress * 20f + "%";

            yield return null;
        }
    }
}
