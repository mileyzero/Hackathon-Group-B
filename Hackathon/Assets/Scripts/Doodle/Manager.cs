using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Manager : MonoBehaviour
{
    public GameObject platform;
    public GameObject bounceplatform;
    public GameObject breakplatform;
    public GameObject player;
    public GameObject pauseMenu;
    public GameObject pauseDonut;

    public GameObject pauseBtn;

    public BoxCollider2D capsule;
    public CircleCollider2D circle;

    public int popularityCount;
    public int moneyCount;
    public int happinessCount;

    public AudioSource audioPlayer;
    public AudioSource background_music;
    public AudioClip[] bounce_sfx;
    public AudioClip extraJump_sfx;
    public AudioClip platformbreak_sfx;
    public AudioClip collect_sfx;
    public AudioClip win_sfx;

    public TextMeshProUGUI _money;
    public TextMeshProUGUI _popularity;
    public TextMeshProUGUI _happiness;

    public TextMeshProUGUI _moneypause;
    public TextMeshProUGUI _popularitypause;
    public TextMeshProUGUI _happinesspause;

    public int platformCount;
    public int numberofstats;
    public GameObject finalplatform;
    public GameObject win;
    public GameObject popularity;
    public GameObject money;
    public GameObject happiness;
    Vector3 spawnposition;

    public Animator transitionAnim;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        audioPlayer = gameObject.GetComponent<AudioSource>();
        spawnposition = new Vector3();

        //Randomly spawns the different type of platforms
        platformCount = Random.Range(platformCount, platformCount+10);
        
        for (int i = 0; i <= platformCount; i++)
        {
            spawnposition.y += Random.Range(2f, 3f);//Randoms the y positon that the platform will spawn
            spawnposition.x = Random.Range(-7f, 7f);//Randoms the x position that the platform will spawn
            if(i == platformCount)
            {
                Instantiate(finalplatform, new Vector3(0, spawnposition.y + 1.2f), Quaternion.identity);
            }
            else
            {
                int randomnumber = Random.Range(0, 9);

                if (randomnumber <= 5)
                {
                    Instantiate(platform, spawnposition, Quaternion.identity);
                }
                else
                {
                    if(randomnumber <= 6)
                    {
                        if (i != platformCount - 2 && i != platformCount - 1)
                        {
                            Instantiate(bounceplatform, spawnposition, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(platform, spawnposition, Quaternion.identity);
                        }
                    }
                    else
                    {

                        if (i != platformCount - 2 && i != platformCount - 1)
                        {
                            Instantiate(breakplatform, spawnposition, Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(platform, spawnposition, Quaternion.identity);
                        }
                    }
                }
            }

            SpawnStats();
        }
    }

    public void PlayWin_Sfx()
    {
        background_music.loop = false;
        background_music.volume = 1;
        background_music.clip = win_sfx; 
        background_music.Play();
    }

    public void PlayeBounce() //bounce sfx
    {
        int randomaudio = Random.Range(0, bounce_sfx.Length);
        AudioClip swing = bounce_sfx[randomaudio];

        audioPlayer.clip = swing;
        audioPlayer.Play();
    }

    public void PlayBreakPlatform() //break sfx
    {
        audioPlayer.clip = platformbreak_sfx;
        audioPlayer.Play();
    }

    public void PlayCollect() //collect sfx
    {
        audioPlayer.clip = collect_sfx;
        audioPlayer.Play();
    }

    public void PlayExtraJump() //extra jump sfx
    {
        audioPlayer.clip = extraJump_sfx;
        audioPlayer.Play();
    }

    private void SpawnStats() //Randomly spawns different resouces
    {
        int randompopularity = Random.Range(-15, 2);
        int randommoney = Random.Range(-13, 4);
        int randomhappiness = Random.Range(-15, 5);
        if (randompopularity > 0)
        {
            Instantiate(popularity, new Vector3(Random.Range(-7f, 7f), spawnposition.y + 0.97f), Quaternion.identity);
        }
        if (randommoney > 0)
        {
            Instantiate(money, new Vector3(Random.Range(-7f, 7f), spawnposition.y + 0.97f), Quaternion.identity);
        }

        if (randomhappiness > 0)
        {
            Instantiate(happiness, new Vector3(Random.Range(-7f, 7f), spawnposition.y + 0.97f), Quaternion.identity);
        }
    }

    public void DisplayStats()
    {
        PlayWin_Sfx();
        StartCoroutine(showstats());
    }

    public void PauseMenu() //when player pauses the game
    {
        pauseMenu.SetActive(true);

        _moneypause.text = moneyCount.ToString();
        _popularitypause.text = popularityCount.ToString();
        _happinesspause.text = happinessCount.ToString();
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static; //changed player's rigidbody to static to stop it from moving
        //Time.timeScale = 0;
        pauseDonut.GetComponent<Animator>().speed = 1;
    }

    public void Resume() //when the player resumes the game
    {
        pauseMenu.SetActive(false);
        player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; //changed back to dynamic so player object can move
        Time.timeScale = 1;
    }

    IEnumerator showstats()
    {
        StoreGame.doodleWinAchCount += 07;
        pauseBtn.SetActive(false);

        //animation to change the number after you win   
        for (int money = 0;  money <= moneyCount; money++)
        {
            if(_money.text == "+" + moneyCount.ToString())
            {
                break;
            }
            else
            {
                _money.text = "+" + money.ToString();
                yield return new WaitForSeconds(0.2f);
            }
            
        }

        for (int popularity = 0; popularity <= popularityCount; popularity++)
        {
            if (_popularity.text == "+" + popularityCount.ToString())
            {
                break;
            }
            else
            {
                _popularity.text = "+" + popularity.ToString();
                yield return new WaitForSeconds(0.2f);
            }

        }

        for (int happy = 0; happy <= happinessCount ; happy++)
        {
            if (_happiness.text == "+" + happinessCount.ToString())
            {
                break;
            }
            else
            {
                _happiness.text = "+" + happy.ToString();
                yield return new WaitForSeconds(0.2f);
            }

        }

        transitionAnim.SetTrigger("Start");

        //increase the stats of the resources in the main game
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().money += moneyCount + 4;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().popularity += popularityCount + 4;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().happiness += happinessCount + 4;

        yield return new WaitForSeconds(2f);

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>()._maingame.gameObject.SetActive(true);

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().hasPlayedLevel1 = false;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().hasPlayedLevel2 = false;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().hasPlayedLevel3 = false;

        this.gameObject.SetActive(false);

        SceneManager.LoadScene("SampleScene");
    }
}
