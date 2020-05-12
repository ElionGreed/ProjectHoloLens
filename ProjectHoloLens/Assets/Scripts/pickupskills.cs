using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupskills : MonoBehaviour
{


    private Vector3 mouseOffset;
    private float MouseZCord;

    void Start()
    {

    }

    void Update()
    {
    }

    public void OnMouseDown()
    {
        MouseZCord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mouseOffset = gameObject.transform.position - GetMouseWorldPos();
    }



    private Vector3 GetMouseWorldPos()
    {
        Vector3 MousePoint = Input.mousePosition;
        MousePoint.z = MouseZCord;
        return Camera.main.ScreenToWorldPoint(MousePoint);
    }

    public void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mouseOffset;
    }
}
