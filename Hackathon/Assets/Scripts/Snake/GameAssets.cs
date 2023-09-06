using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    //used to store all the game assets
    public static GameAssets i;

    private void Awake()
    {
        i = this;
    }
    public Sprite snakeHeadSprite;
    public Sprite foodsprite;
    public Sprite snakebody;
}
