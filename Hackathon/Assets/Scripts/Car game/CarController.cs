using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    // Start is called before the first frame update
    public float carSpeed;
    Vector3 carPosition;
    public float maxPos;

    public GameObject win;
    public GameObject lose_scn;

    public Slider carslider;
    public float timer;
    public float currentTime;
    public bool timerrunning = false;
    public bool lose = false;
    public bool Win = false;

    void Start()
    {
        carPosition = transform.position;
        carslider.value = 0;
        StartTimer();
    }

    public void StartTimer()
    {
        currentTime = timer;
        timerrunning = true;
    }
    // Update is called once per frame
    void Update()
    {
        carPosition.x += Input.GetAxis("Horizontal") * carSpeed * Time.deltaTime;
        carPosition.x = Mathf.Clamp(carPosition.x, -maxPos, maxPos);
        transform.position = carPosition;

        if (timerrunning)
        {
            carslider.value = 1f - (timer/25f);
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                if(lose == false)
                {
                    timerrunning = false;
                    Win = true;
                    win.SetActive(true);
                    Debug.Log("Win");
                }
                
            }
           
        }

        if(lose == true && Win ==false)
        {
            lose_scn.SetActive(true);
        }

        
    }
}
