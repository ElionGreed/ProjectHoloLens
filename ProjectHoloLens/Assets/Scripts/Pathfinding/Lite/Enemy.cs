using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    Stopwatch sw;


    private void Awake()
    {
        sw = new Stopwatch();
        sw.Start();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            sw.Stop();
            print("reached player");
        }
    }
}
