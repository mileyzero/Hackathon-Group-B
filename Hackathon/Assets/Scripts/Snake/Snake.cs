using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2Int gridMoveDirection;
    private Vector2Int gridposition;
    private float gridMoveTimer;
    public float gridMoveTimerMax;
    private Level_Grid level_Grid;
    private List<SnakeBodyPart> snakeBodyList;

    private int snakeBodySize;
    public List<Vector2Int> snakeMovePositionList;
    
    public void SetUp(Level_Grid level_Grid)
    {
        this.level_Grid = level_Grid;
    }
    private void Awake()
    {
       gridposition = new Vector2Int(10, 10);
       gridMoveTimerMax = 0.4f;
       gridMoveTimer = gridMoveTimerMax;
       gridMoveDirection = new Vector2Int(1, 0);

       snakeMovePositionList= new List<Vector2Int>();
        snakeBodySize = 0;
       snakeBodyList= new List<SnakeBodyPart>();
    }

    private void Update()
    {
        HandleInput();
        HandleGridMovement();

    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (gridMoveDirection.y != -1)
            {
                gridMoveDirection.x = 0;
                gridMoveDirection.y = +1;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (gridMoveDirection.y != +1)
            {
                gridMoveDirection.x = 0;
                gridMoveDirection.y = -1;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (gridMoveDirection.x != +1)
            {
                gridMoveDirection.x = -1;
                gridMoveDirection.y = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gridMoveDirection.x != -1)
            {
                gridMoveDirection.x = +1;
                gridMoveDirection.y = 0;
            }
        }
    }

    private void HandleGridMovement()
    {
        gridMoveTimer += Time.deltaTime;

        if (gridMoveTimer >= gridMoveTimerMax)
        {
            gridMoveTimer -= gridMoveTimerMax;
            snakeMovePositionList.Insert(0, gridposition);
            gridposition += gridMoveDirection;

            bool snakeAteFood = level_Grid.SnakeMoved(gridposition);
            if (snakeAteFood)
            {
                Debug.Log("Snake Body Size Increase");
                snakeBodySize++;
                CreateSnakeBody();
            }

            if (snakeMovePositionList.Count >= snakeBodySize + 1)
            {
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count - 1);
            }
            
            for(int i = 0; i< snakeMovePositionList.Count; i++)
            {
                Vector2Int snakemovePosition = snakeMovePositionList[i];
                
            }
            transform.position = new Vector3(gridposition.x, gridposition.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirection)-90);

            UpdateSnakeBodyPart();

            
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
            Vector3 snakebodyposition = new Vector3(snakeMovePositionList[i].x, snakeMovePositionList[i].y);
            snakeBodyList[i].SetGridPosition(snakeMovePositionList[i]);
        }
    }

    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    public Vector2Int GetGridPosition()
    {
        return gridposition;
    }

    public List<Vector2Int> GetFullSnakeGridPositionList()
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridposition };
        gridPositionList.AddRange(snakeMovePositionList);
        return gridPositionList;
    }

    private class SnakeBodyPart
    {
        private Vector2Int gridPosition;
        private Transform transform;
        public SnakeBodyPart(int bodyindex)
        {
            GameObject snakebody = new GameObject("SnakeBody", typeof(SpriteRenderer));
            snakebody.GetComponent<SpriteRenderer>().sprite = GameAssets.i.snakebody;
            snakebody.GetComponent<SpriteRenderer>().sortingOrder = -bodyindex; // puts the added body part behind previous
            transform = snakebody.transform;
        }

        public void SetGridPosition(Vector2Int gridPosition)
        {
            this.gridPosition = gridPosition;
            transform.position = new Vector3(gridPosition.x, gridPosition.y);
        }
    }
}
