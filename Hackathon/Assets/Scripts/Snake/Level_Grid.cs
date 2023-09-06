using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Grid
{
    private Vector2Int foodGridposition;
    private GameObject foodGameObject;
    private int width;
    private int height;
    private Snake snake;

    public Level_Grid(int width, int height) //used to set the width and height of the gird
    {
        this.width = width;
        this.height = height;

        
    }

    public void SetUp(Snake snake)
    {
        this.snake = snake;

        SpawnFood();
    }

    private void SpawnFood() //this function is used to spawn the food at the random positon within the gird
    {
        do
        {
            foodGridposition = new Vector2Int(Random.Range(1, width), Random.Range(1, height));
        } while (snake.GetFullSnakeGridPositionList().IndexOf(foodGridposition) != -1);
        

        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodsprite; //put in the food sprite
        foodGameObject.transform.position = new Vector3(foodGridposition.x, foodGridposition.y);
    }

    public bool SnakeMoved(Vector2Int snakegridposition) //when the snake positon and food positon is the same then snake ate the food
    {
        if(snakegridposition == foodGridposition)
        {
            Object.Destroy(foodGameObject); //destroy food
            //increase snake's speed
            if(snake.gridMoveTimerMax >= 0.06f)
            {
                snake.gridMoveTimerMax -= 0.01f;
            }
            else
            {
                snake.gridMoveTimerMax = 0.05f;
            }
            SpawnFood();
            return true;
        }
        else
        {
            return false;
        }
    }


}
