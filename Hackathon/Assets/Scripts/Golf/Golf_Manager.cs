using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Golf_Manager : MonoBehaviour
{
    [SerializeField] GameObject Golf_title;
    [SerializeField] GameObject golf_game;
    [SerializeField] Golf_Ball golf;

    public GameObject pauseMenu;

    public TextMeshProUGUI money;
    public TextMeshProUGUI happy;
    public TextMeshProUGUI popular;

    public int moneycollected;
    public int happycollected;
    public int popularitycollected;

    public GameObject[] resources;
    public List<GameObject> spawns = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
       golf_game.SetActive(false);
       Golf_title.SetActive(true);
       pauseMenu.SetActive(false);
       StartCoroutine(EnableGame());

    }
    public void PauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private void Update()
    {
        money.text = moneycollected.ToString();
        happy.text = happycollected.ToString();
        popular.text = popularitycollected.ToString();
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
        GameObject[] spawnpoints = GameObject.FindGameObjectsWithTag("golf_spawnpoints");
        spawns.AddRange(spawnpoints);
        SpawnRandom_Resources();

    }

    IEnumerator WinCorotine()
    {
        yield return new WaitForSeconds(3f);

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money += moneycollected * 3;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.popularity += popularitycollected * 3;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.happiness += happycollected * 3;

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>()._maingame.gameObject.SetActive(true);

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.moneySlider.value = GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.popularitySlider.value = GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.popularity;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.happinessSlider.value = GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.happiness;

        GameObject.FindGameObjectWithTag("cooldown").GetComponent<MiniGameTimer>().StartCooldown();

        SceneManager.LoadScene(0);
    }

    public void SpawnRandom_Resources()
    {
        for(int i = 0; i <spawns.Count; i++)
        {
            int number; 
            GameObject spawned = Instantiate(resources[number = Random.Range(0, resources.Length)], new Vector3(spawns[i].transform.position.x, spawns[i].transform.position.y), Quaternion.identity);
            spawned.transform.SetParent(spawns[i].transform);
        }
    }
}
