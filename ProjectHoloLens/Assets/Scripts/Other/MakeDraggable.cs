using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDraggable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        target.AddComponent<NearInteractionGrabbable>();

        // Add ability to drag by reparenting to pointer object on pointer down
        var pointerHandler = target.AddComponent<PointerHandler>();
        pointerHandler.OnPointerDown.AddListener((e) =>
        {
            if (e.Pointer is SpherePointer)
            {
                target.transform.parent = ((SpherePointer)(e.Pointer)).transform;
            }
        });
        pointerHandler.OnPointerUp.AddListener((e) =>
        {
            if (e.Pointer is SpherePointer)
            {
                target.transform.parent = null;
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
