using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.tag == "Enemy") { 
        Destroy(collision.collider.gameObject);
        }
    }

}