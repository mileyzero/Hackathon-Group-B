using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Snake : MonoBehaviour
{
    public GameObject snakeGame;

    //direction that the Snake can move in
    private enum Direction
    {
        Left,
        Right,
        Up,
        Down

    }

    private enum State
    {
        Alive,
        Dead
    }

    private State state;
    private Direction gridMoveDirection;
    private Vector2Int gridposition;
    private float gridMoveTimer;
    public float gridMoveTimerMax;
    private Level_Grid level_Grid;
    private int snakeBodySize;

    public GameObject blackscreen;

    public TextMeshProUGUI score;
    private int scoreint;

    private AudioSource audioPlayer;
    public AudioSource grass_player;
    public AudioClip[] grass;
    public AudioClip atefood_sfx;
    public AudioClip defeat_sfx;

    public GameObject background_music;

    private List<SnakeMovePosition> snakeMovePositionList;
    private List<SnakeBodyPart> snakeBodyList;

    public void SetUp(Level_Grid level_Grid)
    {
        this.level_Grid = level_Grid;
    }
    private void Awake()
    {
        blackscreen.SetActive(false);
        audioPlayer = GetComponent<AudioSource>();
        gridposition = new Vector2Int(10, 10);
        gridMoveTimerMax = 0.2f;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = Direction.Right;

        snakeMovePositionList = new List<SnakeMovePosition>();
        snakeBodySize = 0;
        snakeBodyList = new List<SnakeBodyPart>();
        state = State.Alive;
    }

    public void PlayGrass_Sfx()
    {
        int randomaudio = Random.Range(0, grass.Length);
        AudioClip grass_effect = grass[randomaudio];

        grass_player.clip = grass_effect;
        grass_player.Play();
    }

    private void Update()
    {
        score.text = scoreint.ToString();
        switch (state)
        {
            case State.Alive:
                HandleInput();
                HandleGridMovement();
                break;

            case State.Dead:
                Die();
                break;
        }
    }

    public void Die()
    {
        blackscreen.SetActive(true);
        background_music.SetActive(false);
        state = State.Dead;
        StartCoroutine(DelayToMainGame(3.2f));
    }

    public void AteFood_SFX()
    {
        audioPlayer.clip = atefood_sfx; 
        audioPlayer.Play();
    }

    public void Defeat_SFX()
    {
        grass_player.clip = defeat_sfx;
        grass_player.Play();
    }

    IEnumerator DelayToMainGame(float timer)
    {
        yield return new WaitForSeconds(timer);

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>()._maingame.gameObject.SetActive(true);

        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.hasPlayedLevel1 = false;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.hasPlayedLevel2 = false;
        GameObject.FindGameObjectWithTag("store_game").GetComponent<StoreGame>().gameManager.hasPlayedLevel3 = false;

        SceneManager.LoadScene("SampleScene");
        //snakeGame.SetActive(false);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (gridMoveDirection != Direction.Down)
            {
                PlayGrass_Sfx();
                gridMoveDirection = Direction.Up;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (gridMoveDirection != Direction.Up)
            {
                PlayGrass_Sfx();
                gridMoveDirection = Direction.Down;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (gridMoveDirection != Direction.Right)
            {
                PlayGrass_Sfx();
                gridMoveDirection = Direction.Left;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (gridMoveDirection != Direction.Left)
            {
                PlayGrass_Sfx();
                gridMoveDirection = Direction.Right;
            }
        }
    }

    private void HandleGridMovement()
    {
        gridMoveTimer += Time.deltaTime;

        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridMoveTimer -= gridMoveTimerMax;

            SnakeMovePosition previousSnakeMovePosition = null;
            if (snakeMovePositionList.Count > 0)
            {
                previousSnakeMovePosition = snakeMovePositionList[0];
            }
            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, gridposition, gridMoveDirection);
            snakeMovePositionList.Insert(0, snakeMovePosition);

            Vector2Int girdMoveDirectionVector;
            switch (gridMoveDirection)
            {
                default:
                case Direction.Right: girdMoveDirectionVector = new Vector2Int(1, 0); break;
                case Direction.Left: girdMoveDirectionVector = new Vector2Int(-1, 0); break;
                case Direction.Up: girdMoveDirectionVector = new Vector2Int(0, +1); break;
                case Direction.Down: girdMoveDirectionVector = new Vector2Int(0, -1); break;
            }
            
            gridposition += girdMoveDirectionVector;

            bool snakeAteFood = level_Grid.SnakeMoved(gridposition);
            if (snakeAteFood)
            {
                //Snake body size increase by 1
                AteFood_SFX();
                snakeBodySize++;
                scoreint += 1;
                CreateSnakeBody();
            }

            if (snakeMovePositionList.Count >= snakeBodySize + 1)
            {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            }

            UpdateSnakeBodyPart();
            foreach (SnakeBodyPart snakeBodyPart in snakeBodyList)
            {
                Vector2Int snakebodyPartGridPosition = snakeBodyPart.GetGridPosition();
                if(gridposition ==  snakebodyPartGridPosition)
                {
                    Debug.Log("Death");
                    AteFood_SFX();
                    Defeat_SFX();
                    state = State.Dead;
                }
            }

            transform.position = new Vector3(gridposition.x, gridposition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(girdMoveDirectionVector) - 90);
        }
    }

    private void CreateSnakeBody()
    {
        snakeBodyList.Add(new SnakeBodyPart(snakeBodyList.Count));
    }

    private void UpdateSnakeBodyPart()
    {
        for (int i = 0; i < snakeBodyList.Count; i++)
        {
            snakeBodyList[i].SetSnakeMovePosition(snakeMovePositionList[i]);
        }
    }

    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public Vector2Int GetGridPosition()
    {
        return gridposition;
    }

    //this function returns the list of the gridPosition that is taken up by the snake's Head and Body
    public List<Vector2Int> GetFullSnakeGridPositionList()
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridposition };
        foreach (SnakeMovePosition snakeMovePosition in snakeMovePositionList)
        {
            gridPositionList.Add(snakeMovePosition.GetGridPosition());
        }
        return gridPositionList;
    }

    //this class handles the single block of the snake's body
    private class SnakeBodyPart
    {
        private Transform transform;
        private SnakeMovePosition snakeMovePosition;
        public SnakeBodyPart(int bodyindex)
        {
            GameObject snakebody = new GameObject("SnakeBody", typeof(SpriteRenderer));
            snakebody.GetComponent<SpriteRenderer>().sprite = GameAssets.i.snakebody;
            snakebody.GetComponent<SpriteRenderer>().sortingOrder = -bodyindex; // puts the added body part behind previous
            transform = snakebody.transform;
        }

        public void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition)
        {
            this.snakeMovePosition = snakeMovePosition;
            transform.position = new Vector3(snakeMovePosition.GetGridPosition().x, snakeMovePosition.GetGridPosition().y);
            float angle;
            switch (snakeMovePosition.GetDirection())
            {
                default:
                case Direction.Up: //if snake was going up
                    switch (snakeMovePosition.GetPrevious())
                    {
                        default:
                            angle = 0;
                            break;
                        case Direction.Left: //if was going left previously
                            angle = 45;
                            transform.position += new Vector3(0.2f, 0.2f);
                            break;
                        case Direction.Right: //if was going right previously
                            angle = -45;
                            transform.position += new Vector3(-0.2f, 0.2f);
                            break;
                    }
                    break;
                case Direction.Down: //if snake was going down
                    switch (snakeMovePosition.GetPrevious())
                    {
                        default:
                            angle = 180;
                            break;
                        case Direction.Left: //if was going left previously                       
                            angle = -45;
                            transform.position += new Vector3(0.2f, -0.2f);
                            break;
                        case Direction.Right: //if was going right previously                            
                            angle = +45;
                            transform.position += new Vector3(-0.2f, -0.2f);
                            break;
                    }
                    break;
                case Direction.Left: //if the snake is going left
                    switch (snakeMovePosition.GetPrevious())
                    {
                        default:
                            angle = -90;
                            break;
                        case Direction.Down: //if was going down previously                           
                            angle = -45;
                            transform.position += new Vector3(-0.2f, 0.2f);
                            break;
                        case Direction.Up: //if was going up previously
                            angle = +45;
                            transform.position += new Vector3(-0.2f, -0.2f);
                            break;
                    }
                    break;
                case Direction.Right: //if the snake is going to the right      
                    switch (snakeMovePosition.GetPrevious())
                    {
                        default:
                            angle = -90;
                            break;
                        case Direction.Down: //if was going down previously
                            angle = 45;
                            transform.position += new Vector3(0.2f, 0.2f);
                            break;
                        case Direction.Up: //if was going up previously
                            angle = -45;
                            transform.position += new Vector3(0.2f, -0.2f);
                            break;
                    }
                    break;

            }
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
        public Vector2Int GetGridPosition()
        {
            return snakeMovePosition.GetGridPosition();
        }
    }

    //this class handles the snake after moving 1 time
    private class SnakeMovePosition
    {
        private SnakeMovePosition previousSnakeMovePosition;
        private Vector2Int gridPosition;
        private Direction direction;

        public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition, Vector2Int gridPosition, Direction direction)
        {
            this.previousSnakeMovePosition = previousSnakeMovePosition;
            this.gridPosition = gridPosition;
            this.direction = direction;
        }

        public Vector2Int GetGridPosition()
        {
            return gridPosition;
        }

        public Direction GetDirection()
        {
            return direction;
        }

        public Direction GetPrevious()
        {
            if (previousSnakeMovePosition != null)
            {
                return previousSnakeMovePosition.direction;
            }
            else
            { return Direction.Right; }
        }
    }
}
