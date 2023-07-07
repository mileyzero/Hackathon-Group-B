using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTrack : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, -5 * Time.deltaTime);

        if(transform.position.y < -10)
        {
            transform.position = new Vector3(transform.position.x,10f);
        }
    }
}
