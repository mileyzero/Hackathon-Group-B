using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CarController : MonoBehaviour
{
    // Start is called before the first frame update
    public float carSpeed;
    Vector3 carPosition;
    public float maxPos;
    void Start()
    {
        carPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        carPosition.x += Input.GetAxis("Horizontal") * carSpeed * Time.deltaTime;
        carPosition.x = Mathf.Clamp(carPosition.x, -maxPos, maxPos);
        transform.position = carPosition;
    }
}
