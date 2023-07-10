using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeMove : MonoBehaviour
{
    public float speed = 5f;
    public GameObject road;
    // Start is called before the first frame update
    void Start()
    {
        road = GameObject.FindGameObjectWithTag("road");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate (new Vector3(0,-1,0)*(-road.GetComponent<moveTrack>().speed)*Time.deltaTime);
    }
}
