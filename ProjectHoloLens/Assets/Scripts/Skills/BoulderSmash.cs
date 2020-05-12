using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSmash : MonoBehaviour
{
    private int myDamage = 50;
    EnemyUnit enemyUnit;

    // Start is called before the first frame update

  

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyUnit = other.GetComponent<EnemyUnit>();
            enemyUnit.TakeDamage(myDamage);
        }
        Destroy(gameObject);
    }

}
