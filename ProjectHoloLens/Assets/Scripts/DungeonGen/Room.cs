using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public float width;
    public float Height;

    public int x;
    public int y;

    // Start is called before the first frame update
    void Start()
    {
        if(RoomController.instance == null)
        {
            return;


        }


        RoomController.instance.RegisterRoom(this);
    }

    public Vector3 GetMiddleRoom()
    {
        return new Vector3(x * width, y * Height);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, Height, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
