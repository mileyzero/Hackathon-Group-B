using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLose : MonoBehaviour
{
    public GameObject car;

    public void Awake()
    {
        car = GameObject.FindGameObjectWithTag("car");
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "car")
        {
            car.GetComponent<CarController>().lose = true;
            Debug.Log("Lose");
        }
    }
}
