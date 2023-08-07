using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTimer : MonoBehaviour
{
    public float MGTimer = 40f;

    // Start is called before the first frame update
    void Start()
    {
        MGTimer = 40f;
}

    // Update is called once per frame
    void Update()
    {
        MGTimer -= Time.deltaTime;
    }

    public void Timer()
    {
        MGTimer = 40f;

        if(MGTimer > 0)
        {
            Debug.Log(MGTimer);
            MGTimer -= Time.deltaTime;
        }
    }
}
