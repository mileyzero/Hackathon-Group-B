using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class moveTrack : MonoBehaviour
{
    public float speed;
    public float maxspeed;
    public float swapPosition;
    public CarController car;

    // Update is called once per frame
    void Update()
    {

        transform.position += new Vector3(0, speed * Time.deltaTime);//moves the track down overtime
        if(transform.position.y < -swapPosition) //when the track reaches certain positon it will teleport to the top
        {
            transform.position = new Vector3(transform.position.x,10f);
        }
   

        if(speed >= -maxspeed)
        {
            if (car.timer <= 25f)
            {
                speed -= Time.deltaTime;
            }
        }
        
    }
}
