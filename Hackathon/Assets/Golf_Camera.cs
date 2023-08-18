using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golf_Camera : MonoBehaviour
{
    public Transform target;
    public Vector3 camera_y;

    private void LateUpdate()
    {
            Vector3 newPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = newPosition;
    }
}
