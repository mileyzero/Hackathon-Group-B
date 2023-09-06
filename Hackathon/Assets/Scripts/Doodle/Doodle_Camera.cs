using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doodle_Camera : MonoBehaviour
{
    public Transform target;

    private void LateUpdate() //to make the camera follow the player if the player moves above the camera
    {
        if(target.position.y > transform.position.y)
        {
            Vector3 newPosition = new Vector3(transform.position.x, target.position.y, transform.position.z);
            transform.position = newPosition;
        }
    }
}
