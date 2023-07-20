using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Snake : MonoBehaviour
{
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

    private List<SnakeMovePosition> snakeMovePositionList;
    private List<SnakeBodyPart> snakeBodyList;

    public void SetUp(Level_Grid level_Grid)
    {
        this.level_Grid = level_Grid;
    }
    private void Awake()
    {
        gridposition = new Vector2Int(10, 10);
        gridMoveTimerMax = 0.3f;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = Direction.Right;

        snakeMovePositionList = new List<SnakeMovePosition>();
        snakeBodySize = 0;
        snakeBodyList = new List<SnakeBodyPart>();
        state = State.Alive;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Alive:
                HandleInput();
                HandleGridMovement();
                break;

            case State.Dead:
                break;
        }


    }

    public void Die()
    {
        state = State.Dead;
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (gridMoveDirection != Direction.Down)
            {
                gridMoveDirection = Direction.Up;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (gridMoveDirection != Direction.Up)
            {
                gridMoveDirection = Direction.Down;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (gridMoveDirection != Direction.Right)
            {
                gridMoveDirection = Direction.Left;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (gridMoveDirection != Direction.Left)
            {
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
                snakeBodySize++;
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