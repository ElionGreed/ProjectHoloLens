using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : CommonUnit
{

    PlayerUnit playerHealth;
    [SerializeField]
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        UnitManager.unitManager.enemyUnits.Add(gameObject);
        playerHealth = player.GetComponent<PlayerUnit>();
    }

    public void DealDamage()
    {
        playerHealth.TakeDamage(myDamage);
    }

}
