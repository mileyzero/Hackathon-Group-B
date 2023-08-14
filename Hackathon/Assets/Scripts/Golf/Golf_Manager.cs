using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Golf_Manager : MonoBehaviour
{
    [SerializeField] GameObject Golf_title;
    [SerializeField] GameObject golf_game;
    [SerializeField] Golf_Ball golf;
    // Start is called before the first frame update
    void Start()
    {
       golf_game.SetActive(false);
       Golf_title.SetActive(true);
       StartCoroutine(EnableGame());
    }

    public void Win()
    {
        StartCoroutine(WinCorotine());
    }

    IEnumerator EnableGame()
    {
        yield return new WaitForSeconds(2f);
        golf_game.SetActive(true);
        Golf_title.SetActive(false);
    }

    IEnumerator WinCorotine()
    {
        yield return new WaitForSeconds(3f);

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>()._maingame.gameObject.SetActive(true);
        SceneManager.LoadScene(0);

    }
}
