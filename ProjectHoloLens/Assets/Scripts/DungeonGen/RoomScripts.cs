using UnityEngine;

public class RoomScripts : MonoBehaviour
{

    public DoorScript[] doorways;
    public MeshCollider meshCollider;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Bounds RoomBounds
    {
        get
        {
            return meshCollider.bounds;
        }
    }

 

}
