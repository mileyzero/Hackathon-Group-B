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

    public Level_Grid(int width, int height)
    {
        this.width = width;
        this.height = height;

        
    }

    public void SetUp(Snake snake)
    {
        this.snake = snake;

        SpawnFood();
    }

    private void SpawnFood()
    {
        do
        {
            foodGridposition = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        } while (snake.GetGridPosition() == foodGridposition);
        

        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodsprite;
        foodGameObject.transform.position = new Vector3(foodGridposition.x, foodGridposition.y);
    }

    public void SnakeMoved(Vector2Int snakegridposition)
    {
        if(snakegridposition == foodGridposition)
        {
            Object.Destroy(foodGameObject);
            SpawnFood();
            Debug.Log("Snake Ate Food");
        }
    }

   
}
