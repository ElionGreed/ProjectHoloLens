using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : CommonUnit
{

    // PlayerUnit playerHealth;
    [SerializeField]
    //  GameObject player;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        UnitManager.unitManager.enemyUnits.Add(gameObject);
      //  playerHealth = Unit.player.GetComponent<PlayerUnit>();

    }


}
