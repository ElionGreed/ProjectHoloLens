using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupskills : MonoBehaviour
{

    public GameObject oogaItem;
    public GameObject oogaParent;
    public GameObject Booga;

    // Start is called before the first frame update
    void Start()
    {
        oogaItem.GetComponent<Rigidbody>().useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        oogaItem.GetComponent<Rigidbody>().useGravity = false;
        oogaItem.GetComponent<Rigidbody>().isKinematic = true;
        oogaItem.transform.position = Booga.transform.position;
        oogaItem.transform.rotation = Booga.transform.rotation;
        oogaItem.transform.parent = oogaParent.transform;
    }

    public void OnMouseUp()
    {
        oogaItem.GetComponent<Rigidbody>().useGravity = true;
        oogaItem.GetComponent<Rigidbody>().isKinematic = false;
        oogaItem.transform.parent = null;
        oogaItem.transform.position = Booga.transform.position;
    }
}
