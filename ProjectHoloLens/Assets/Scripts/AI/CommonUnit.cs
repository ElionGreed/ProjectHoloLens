using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonUnit : MonoBehaviour
{
    [SerializeField]
    protected int myHealth, mySpeed, myDamage;
    private int maxHealth = 100;
    public Image HPBar;

    private void Awake()
    {
        myHealth = maxHealth;
    }
    private void Start()
    {

    }

    public void TakeDamage(int amount)
    {
        myHealth -= amount;
        HPBar.fillAmount = myHealth / maxHealth;
        print("taking dmg");

        if (myHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        UnitManager.unitManager.enemyUnits.Remove(gameObject);
        UnitManager.unitManager.playerUnits.Remove(gameObject);
        Destroy(gameObject);
    }
}
