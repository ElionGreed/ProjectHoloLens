using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class CompanionAI : MonoBehaviour
{
    [SerializeField]
    float[] utilityValues = new float[5];
    [SerializeField]
    float companionHealth = 100;
    float damage = 10;
    float distance;
    NavMeshAgent agent;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject runAwaySpot;
    float[] enemyHealth;
    int biggestAction;
    OrcScript orcScipt;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        distance = Vector3.Distance(transform.position, player.transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
       // CalculateUtility();
    }
    private void CalculateUtility()
    {
      

        utilityValues[0] = 0;  // Death Value
        if (companionHealth < 1)
        {
            utilityValues[0] = 1000;
        }
        utilityValues[1] = 100/companionHealth;  // Run Away Value
        utilityValues[2] = 0;
        if (UnitManager.unitManager.enemyUnits.Count != 0)
        {
            utilityValues[2] = 100 / (UnitManager.unitManager.enemyUnits.Count*4); // Attack Value
        }
        utilityValues[3] = 0;// Follow Value
        if (UnitManager.unitManager.enemyUnits.Count == 0)
        {
            utilityValues[0] = 0; // if there are no enemies respawn and follow player
            utilityValues[3] = 999;
        }
        chooseAction();
    }
    private void chooseAction()
    {
        biggestAction = 3;
        for (int n = 0; n < 4; n++)
        {
            if (utilityValues[n + 1] > utilityValues[n])
            {
                biggestAction = n + 1;
            }

        }

        switch (biggestAction)
        {
            case 0:
                {
                    Downed();
                    break;
                }
            case 1:
                {
                   RunAway();
                    break;
                }
            case 2:
                {
                    AttackEnemy();
                    break;
                }
            case 3:
                {
                   Follow();
                    break;
                }
        }




    }
    private void Downed()
    {
        agent.isStopped = true;
    }
    private void RunAway()
    {
        agent.isStopped = false;
        agent.SetDestination(runAwaySpot.transform.position);
    }

    private void AttackEnemy()
    {
        agent.isStopped = false;
        agent.SetDestination(UnitManager.unitManager.enemyUnits[0].transform.position);
    }
    private void Follow()
    {
        agent.isStopped = false;
        agent.SetDestination(player.transform.position);
    }
}



