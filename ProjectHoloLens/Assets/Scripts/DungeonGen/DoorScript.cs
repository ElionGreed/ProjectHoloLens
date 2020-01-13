using UnityEngine;

public class DoorScript : MonoBehaviour
{
 
    void OnDrawGizmos()
    {
        Ray Ray = new Ray(transform.position, transform.rotation * Vector3.forward);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Ray);
    }

}
