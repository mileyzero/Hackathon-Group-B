using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class moveTrack : MonoBehaviour
{
    public float speed;
    public CarController car;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime);
        if(transform.position.y < -10)
        {
            transform.position = new Vector3(transform.position.x,10f);
        }
   

        if(speed >= -15f)
        {
            if (car.timer <= 25f)
            {
                speed -= Time.deltaTime;
            }
        }
        
    }
}
