using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class CompanionAI : MonoBehaviour
{
    float[] utilityValues = new float[5];
    [SerializeField]
    int companionHealth = 100;
    NavMeshAgent agent;
    GameObject[] enemy;
    float[] enemyHealth;
    OrcScript orcScipt;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        CalculateUtility();
    }
    private void CalculateUtility()
    {
        orcScipt = enemy[0].GetComponent<OrcScript>();
    }
    private void chooseAction()
    {
        int biggestAction = 0;
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
                   // Patrol();
                    break;
                }
            case 1:
                {
                    //  anim.SetTrigger("Move");
                   // MoveToTarget();
                    break;
                }
            case 2:
                {
                    //AttackEnemy();
                    break;
                }
            case 3:
                {
                   // Die();
                    break;
                }
        }




    }

}



