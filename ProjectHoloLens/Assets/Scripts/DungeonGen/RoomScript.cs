using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{

    public ADoorways[] doorways;
    public MeshCollider meshCollider;

    public Bounds RoomBounds
    {
        get { return meshCollider.bounds; }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
