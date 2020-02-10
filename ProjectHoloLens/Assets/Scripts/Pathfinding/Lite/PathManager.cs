using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{

    Pathfinding pathfinding;
    int n;

    private void Awake()
    {
        pathfinding = GetComponent<Pathfinding>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //pathfinding.StartFindPath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    struct PathRequest
    {
        public Vector3 startPos;
        public Vector3 targetPos;
        public bool isFollowing;
        public Action<Vector3, bool, bool> callback;
 

        public PathRequest(Vector3 _start, Vector3 _target, bool _isFollowing, Action<Vector3, bool, bool> _callback)
        {
            startPos = _start;
            targetPos = _target;
            isFollowing = _isFollowing;
            callback = _callback;
        }

    }
}
