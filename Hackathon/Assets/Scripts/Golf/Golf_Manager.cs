using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Golf_Manager : MonoBehaviour
{
    [SerializeField] GameObject Golf_title;
    [SerializeField] GameObject golf_game;
    [SerializeField] Golf_Ball golf;

    public GameObject pauseMenu;
    public GameObject pauseBtn;

    public AudioSource audioPlayer;
    public AudioSource background_music;
    public AudioClip[] swing_audioClips;
    public AudioClip inhole;
    public AudioClip lightap;
    public AudioClip collect_sfx;
    public AudioClip win_sfx;

    public TextMeshProUGUI money;
    public TextMeshProUGUI happy;
    public TextMeshProUGUI popular;

    public int moneycollected;
    public int happycollected;
    public int popularitycollected;

    public Animator transitionAnim;

    public GameObject[] resources; //store different type of resources
    public List<GameObject> spawns = new List<GameObject>(); //store all the spawn points

    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().levelAudioChange.Stop();

       golf_game.SetActive(false);
       Golf_title.SetActive(true);
       pauseMenu.SetActive(false);
       StartCoroutine(EnableGame());
       audioPlayer = gameObject.GetComponent<AudioSource>();
    }

    public void PlayeSwing() //sound effect for when player swing golf
    {
        int randomaudio = Random.Range(0, swing_audioClips.Length);
        AudioClip swing = swing_audioClips[randomaudio];

        audioPlayer.clip = swing;
        audioPlayer.Play();
    }

    public void PlayeInHole() //sound effect for ball in hole
    {
        audioPlayer.clip = inhole;
        audioPlayer.Play();
    }

    public void PlayeLightTap() //sound effect for light tapping golf ball
    {
        audioPlayer.clip = lightap;
        audioPlayer.Play();
    }

    public void PlayCollect() //sound effect for collecting resources
    {
        audioPlayer.clip = collect_sfx;
        audioPlayer.Play();
    }

    public void PauseMenu() //function for when player clicks pause button
    {
        pauseMenu.SetActive(true); //enable pause screen
        Time.timeScale = 0; //stop time
    }

    public void Resume() //function for when player clicks resume
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1; //resume time
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

    IEnumerator EnableGame() //when game starts
    {
        yield return new WaitForSeconds(2f);
        golf_game.SetActive(true); //enable golf course
        Golf_title.SetActive(false); 
        GameObject[] spawnpoints = GameObject.FindGameObjectsWithTag("golf_spawnpoints"); //find the spawn points in the scene and store all the spawn points into the array
        spawns.AddRange(spawnpoints);
        SpawnRandom_Resources();

    }

    IEnumerator WinCorotine() //coroutine for when ball goes into hole
    {
        StoreGame.golfAchCount += 03;
        pauseBtn.SetActive(false);

        //the if statements are used to figure out which level the player has completed to make the spawning of the golf level more sequence based
        if (SceneManager.GetActiveScene().name == "Golf Level 1")
        {
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().golfwon_Level1 = true;
        }
        else if (SceneManager.GetActiveScene().name == "Golf Level 2")
        {
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().golfwon_Level2 = true;
        }
        else if (SceneManager.GetActiveScene().name == "Golf Level 3")
        {
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().golfwon_Level3 = true;
        }
        else if (SceneManager.GetActiveScene().name == "Golf Level 4")
        {
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().golfwon_Level4 = true;
        }
        else if (SceneManager.GetActiveScene().name == "Golf Level 5")
        {
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().golfwon_Level5 = true;
        }
        else if (SceneManager.GetActiveScene().name == "Golf Level 6")
        {
            GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().golfwon_Level6 = true;
        }

        background_music.loop = false;
        background_music.volume = 1;
        background_music.clip = win_sfx;
        background_music.Play();
        yield return new WaitForSeconds(2f);
        transitionAnim.SetTrigger("Start");

        yield return new WaitForSeconds(2f);

        //increase the stats based on the amount the player colleted in the level
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money += moneycollected + 6;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.popularity += popularitycollected + 6;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.happiness += happycollected + 6;

        //enable the main game
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>()._maingame.gameObject.SetActive(true);

        //update the slider based on the resouces collected
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.moneySlider.value = GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.money;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.popularitySlider.value = GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.popularity;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.happinessSlider.value = GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.happiness;

        GameObject.FindGameObjectWithTag("cooldown").GetComponent<MiniGameTimer>().StartCooldown();

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().hasPlayedLevel1 = false;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().hasPlayedLevel2 = false;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().hasPlayedLevel3 = false;

        SceneManager.LoadScene("SampleScene");
    }

    public void SpawnRandom_Resources() //fuction to spawn random resouces into the spawn points in the scene
    {
        for(int i = 0; i <spawns.Count; i++) //loops through the resouces and spawnpoints array
        {
            int number; 
            GameObject spawned = Instantiate(resources[number = Random.Range(0, resources.Length)], new Vector3(spawns[i].transform.position.x, spawns[i].transform.position.y), Quaternion.identity); //randoms the resouces and spawns it into the spawn points
            spawned.transform.SetParent(spawns[i].transform);
        }
    }
}
