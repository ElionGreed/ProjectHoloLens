using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupskills : MonoBehaviour
{

    public Vector3 Spot;
    public bool canbeHeld = true;
    public GameObject Item;
    public GameObject Parent;
    public bool isbeingHeld = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isbeingHeld == true)
        {
            Item.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Item.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            Item.transform.SetParent(Parent.transform);
        }
        else
        {
            Spot = Item.transform.position;
            Item.transform.SetParent(null);
            Item.GetComponent<Rigidbody>().useGravity = true;
            Item.transform.position = Spot;

        }
    }

    public void OnMouseDown()
    {
        isbeingHeld = true;
        Item.GetComponent<Rigidbody>().useGravity = false;
        Item.GetComponent<Rigidbody>().detectCollisions = true;
    }

    public void OnMouseUp()
    {
        isbeingHeld = false;
    }
}
