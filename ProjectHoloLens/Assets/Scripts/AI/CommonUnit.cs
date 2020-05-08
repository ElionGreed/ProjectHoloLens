using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonUnit : MonoBehaviour
{

    [SerializeField]
    protected int myHealth;
    [SerializeField]
    protected int mySpeed;
    [SerializeField]
    protected int myDamage;

    // Start is called before the first frame update
    void Start()
    {
       
    }

 
 
    public void TakeDamage(int amount)
    {
        myHealth -= amount;     
    }

   

    void Die()
    {
        if (myHealth <= 0)
        {
            UnitManager.unitManager.enemyUnits.Remove(gameObject);
            UnitManager.unitManager.playerUnits.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
