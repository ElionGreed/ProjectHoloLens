using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BasicInstantaie : MonoBehaviour
{
    public GameObject rock;

    // Start is called before the first frame update
    void Update()
    {
            Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(rock, new Vector3(cursorPos.x, cursorPos.y, cursorPos.z), Quaternion.identity);
    }
}
