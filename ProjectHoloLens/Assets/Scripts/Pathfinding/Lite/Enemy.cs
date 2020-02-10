using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    Stopwatch sw;
    bool isFinished;
    bool isFollowing;
    Vector3 nextMove;
    float speed = 20;

    private void Start()
    {
        isFinished = false;
        isFollowing = false;
        PathManager.RequestPath(transform.position, target.position, isFollowing, OnPathFound);
    }

    private void Awake()
    {
        sw = new Stopwatch();
        sw.Start();
    }

    public void OnPathFound(Vector3 _nextMove, bool pathSuccessful, bool _isFinished)
    {
        isFinished = _isFinished;

        if (pathSuccessful)
        {
            nextMove = _nextMove;
            //adding pivot offset
            nextMove.y += 0.84f;
            StopCoroutine("MoveAlongPath");
            StartCoroutine("MoveAlongPath");

        }

    }
    IEnumerator MoveAlongPath()
    {
        print("hey");
        Vector3 currentWaypoint = nextMove;
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                if (!isFinished)
                {
                    isFollowing = true;
                    PathManager.RequestPath(transform.position, target.position, isFollowing, OnPathFound);
                }

                yield break;
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;

        }

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
