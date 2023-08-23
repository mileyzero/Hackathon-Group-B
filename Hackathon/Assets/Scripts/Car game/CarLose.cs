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
    //if the obstacles collides with car then they lose
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "car")
        {
            car.GetComponent<CarController>().PlayerLose();
            car.GetComponent<CarController>().lose = true;
            car.GetComponent<CapsuleCollider2D>().enabled = false;
            Debug.Log("Lose");

        }

    }
}
