using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionUnit : CommonUnit
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        UnitManager.unitManager.companion = gameObject;
    }

    // Update is called once per frame
    
}
