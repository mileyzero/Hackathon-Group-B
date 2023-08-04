using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyGame : MonoBehaviour
{
    public static DontDestroyGame instance;


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
