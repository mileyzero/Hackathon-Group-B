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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}