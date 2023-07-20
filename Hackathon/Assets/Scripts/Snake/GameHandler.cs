using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField] Snake snake;
    private Level_Grid levelgrid;
    // Start is called before the first frame update
    void Start()
    {
        levelgrid = new Level_Grid(19,19);

        snake.SetUp(levelgrid);
        levelgrid.SetUp(snake);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
