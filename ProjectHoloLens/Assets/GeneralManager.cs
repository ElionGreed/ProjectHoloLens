﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    public GameObject unitTest;
    public Transform start, end;
    Unit unit;
    void Start()
    {
        unit = unitTest.GetComponent<Unit>();

        PathRequestManager.RequestPath(start.position, end.position, unit.OnPathFound);
    }
}
