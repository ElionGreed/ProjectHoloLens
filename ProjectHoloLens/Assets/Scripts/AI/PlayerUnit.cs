using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerUnit : CommonUnit
{

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        UnitManager.unitManager.playerCharacter = gameObject;
    
    }

    // Update is called once per frame
   
}
