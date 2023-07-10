using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCone : MonoBehaviour
{
    public GameObject cone;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "cone")
        {
            Destroy(collision.gameObject);
        }
    }
}
