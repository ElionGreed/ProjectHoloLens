using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    public Vector3 targetPosition;
    Vector3 lookAtTarget;
    Quaternion playerRotation;

    float rotationSpeed = 5;
    float movementSpeed = 5;

    GameObject pointer;

    //instantiate arrow at node player selected or instantiate different colour tile at centre of node selected

    // Update is called once per frame
    void Update()
    {
          if (Input.GetMouseButtonDown(0))
          {
              SetTargetPosition();
          }
        Move();
    }

    private void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            targetPosition = hit.point;
            lookAtTarget = new Vector3(targetPosition.x - transform.position.x, transform.position.y, targetPosition.z - transform.position.z);
            playerRotation = Quaternion.LookRotation(lookAtTarget);
            //this.transform.Translate(targetPosition.x, 0, targetPosition.z);

            //while (this.transform.position!=targetPosition)
            //{
            //    Move();
            //}
        }
    }

    private void Move()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, rotationSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
    }
}
