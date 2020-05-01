using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    PathRequest currentPathRequest;
    Dlite Dlite;
    int n;

    private void Awake()
    {
        Dlite = GetComponent<Dlite>();
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

    public static void RequestPath(Vector3 start, Vector3 target, bool isFollowing, Action<Vector3, bool, bool> callback)
    {
        PathRequest newRequest = new PathRequest(start, target, isFollowing, callback);
    }

    //check this
    void TryProcessingNext()
    {
        Dlite.StartFindPath(currentPathRequest.startPos, currentPathRequest.targetPos, currentPathRequest.isFollowing);
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
