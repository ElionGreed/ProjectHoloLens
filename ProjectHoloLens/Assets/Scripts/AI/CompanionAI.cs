using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]

public class CompanionAI : MonoBehaviour
{
    [SerializeField]
    float[] utilityValues = new float[5];
    int designantedEnemy; //which enemy unit to perform action on
    public bool hasActionFinished; //checks if 
    bool isMoving;
    bool hasAttacked;
    Animator anim;
    [SerializeField]
    GameObject player;
    [SerializeField]
    Vector3 runAwaySpot;
    //float[] enemyHealth;
    int biggestAction;
    Unit unit;
    CompanionUnit companion;
    Vector3 vel;
    Vector3 _prevPosition;
    EnemyUnit enemyUnit;
    // Start is called before the first frame update
    void Start()
    {
        unit = gameObject.GetComponent<Unit>();
        
        companion = gameObject.GetComponent<CompanionUnit>();
        anim = GetComponent<Animator>();

    }

    public void CalculateUtility() //calculate which enemy to attack
    {
        hasActionFinished = false;

        if (UnitManager.unitManager.enemyUnits.Count > 0)
        {
            CalculateEnemyPriority();
        }

        utilityValues[0] = 0;  // Death Value
        if (companion.myHealth < 1)
        {
            utilityValues[0] = 1000;
        }
        utilityValues[1] = 100/companion.myHealth;  // Run Away Value
        utilityValues[2] = 0;
        if (UnitManager.unitManager.enemyUnits.Count != 0)
        {
            utilityValues[2] = 100 / (UnitManager.unitManager.enemyUnits.Count*4); // Approach Enemy Value
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
        hasAttacked = false;
        anim.SetFloat("HP", companion.myHealth);
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
                    ApproachEnenmy();
                    break;
                }
            case 3:
                {
                   Follow();
                    break;
                }

        }




    }

    private void CalculateEnemyPriority()
    {
        float temp;
        enemyUnit = UnitManager.unitManager.enemyUnits[0].GetComponent<EnemyUnit>();
        temp = (enemyUnit.myHealth) + (Vector3.Distance(transform.position, enemyUnit.transform.position)) * 5;
        for (int i = 0; i < UnitManager.unitManager.enemyUnits.Count; i++)
        {
          
            enemyUnit = UnitManager.unitManager.enemyUnits[i].GetComponent<EnemyUnit>();
            if (temp > (enemyUnit.myHealth) + (Vector3.Distance(transform.position, enemyUnit.transform.position)) * 5)
            {
                temp = (enemyUnit.myHealth) + (Vector3.Distance(transform.position, enemyUnit.transform.position)) * 5;
                designantedEnemy = i;     
            }

        }
    }
    private void Downed()
    {
        if(UnitManager.unitManager.enemyUnits.Count == 0)
        {
            companion.myHealth = companion.maxHealth/2;
        }
  
    }
    private void RunAway()
    {
    
        runAwaySpot = UnitManager.unitManager.enemyUnits[designantedEnemy].transform.position - new Vector3(5, 0, 5);
        PathRequestManager.RequestPath(gameObject.transform.position, runAwaySpot, unit.OnPathFound);
        StartCoroutine(Wait());

    }

  
    private void ApproachEnenmy()
    {
        PathRequestManager.RequestPath(gameObject.transform.position, UnitManager.unitManager.enemyUnits[designantedEnemy].transform.position, unit.OnPathFound);
        StartCoroutine(Wait());

    }
    private void Follow()
    {
        player = UnitManager.unitManager.playerCharacter;
        PathRequestManager.RequestPath(gameObject.transform.position, player.transform.position, unit.OnPathFound);
        StartCoroutine(Wait());
    }

    private void AttackEnemy()
    {
        if (hasAttacked == false)
        {
            anim.SetTrigger("Attack");
            enemyUnit.TakeDamage(companion.myDamage);
            hasActionFinished = true;
            hasAttacked = true;   //to avoid double attack
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.08f);
        StartCoroutine(FaceMovementDirection());
    }

    private IEnumerator FaceMovementDirection()
    {
  
        while (transform.hasChanged)
        {          
            vel = (transform.position - _prevPosition) / Time.fixedDeltaTime;
            _prevPosition = transform.position;
            Debug.Log(vel);
            transform.hasChanged = false;
            if (vel != Vector3.zero)
            {
                isMoving = true;
                anim.SetBool("isMoving", true);
                transform.rotation = Quaternion.LookRotation(vel, Vector3.up);
                transform.hasChanged = true;
            }
            else
            {           
                isMoving = false;
                anim.SetBool("isMoving", false);
            }
            if (UnitManager.unitManager.enemyUnits.Count > 0)
            {
                if (Vector3.Distance(transform.position, enemyUnit.transform.position) < 1)
                {
                    AttackEnemy();
                }
            }
            yield return null;
         
        }
        hasActionFinished = true;
    }

}



