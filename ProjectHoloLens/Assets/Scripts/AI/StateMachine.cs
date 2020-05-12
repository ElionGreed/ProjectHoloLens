using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]

public class StateMachine : MonoBehaviour
{

    //PlayerHealth playerHealth;
    private enum States { Idle, Move, Attack, }
    [SerializeField]
    private States currState;
    public bool hasActionFinished;
    bool isMoving;
    bool hasAttacked;
  //  float distance;
    Animator anim; 
    PlayerUnit playerUnit;
    EnemyUnit myUnit;
    CompanionUnit companionUnit;
    Unit unit;
    GameObject player;
    GameObject companion;
    GameObject target;


    Vector3 vel;
    Vector3 _prevPosition;

    // Use this for initialization
    void Start()
    {
        myUnit = GetComponent<EnemyUnit>();
        anim = GetComponent<Animator>();
        unit = gameObject.GetComponent<Unit>();
        currState = States.Move;
        StartCoroutine(WaitForLoad());
    }

    private IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(0.1f);
        Initialize();
    }
    private void Initialize()
    {
        player = UnitManager.unitManager.playerCharacter;
        companion = UnitManager.unitManager.companion;
        playerUnit = player.GetComponent<PlayerUnit>();
        companionUnit = companion.GetComponent<CompanionUnit>();
    }

    public void BehaviourLoop()
    {
        hasActionFinished = false;
        ChooseTarget();
        switch (currState)
        {

            case States.Idle:
                {
                    Idle();
                    break;
                }
            case States.Move:
                {
                    MoveToTarget();
                    break;
                }
            case States.Attack:
                {
                    AttackTarget();
                    break;
                }
   
        }

    }

    void ChooseTarget()
    {
        if ((Vector3.Distance(transform.position, player.transform.position)) < (Vector3.Distance(transform.position, companion.transform.position))) 
        { 
            target = player;
        }
        else
        {
            target = companion;
        }
 
    }

    float CalculateDistance()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance;
    }

    private IEnumerator FaceMovementDirection()
    {

        while (transform.hasChanged)
        {      
            vel = (transform.position - _prevPosition) / Time.deltaTime;
            _prevPosition = transform.position;
            transform.hasChanged = false;
            if (vel != Vector3.zero)
            {
                isMoving = true;
                anim.SetBool("isMoving", true);
                transform.rotation = Quaternion.LookRotation(vel, Vector3.up);
            }
            else
            {
                isMoving = false;
                anim.SetBool("isMoving", false);
            }
            if (CalculateDistance() < 1)
            {
                currState = States.Attack;
            }
            else if (CalculateDistance() > 1)
            {
                currState = States.Move;
            }        
            yield return null;
        }
        hasActionFinished = true;
    }


    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.08f);
        StartCoroutine(FaceMovementDirection());
    }

    private void MoveToTarget()
    {
        PathRequestManager.RequestPath(transform.position, target.transform.position, unit.OnPathFound);
        StartCoroutine(Wait());
    }
    private void Idle()
    {
        if(myUnit.myHealth < 1)
        {
            currState = States.Idle;
        }  
       else if (CalculateDistance() > 1)
        {
            currState = States.Move;
        }
        else if (CalculateDistance() < 1)
        {
            currState = States.Attack;
        }
    }
    private void AttackTarget()
    {
        if (myUnit.myHealth < 1)
        {
            currState = States.Idle;
        }
        else if (CalculateDistance() > 1)
        {
            currState = States.Move;
        }
        else if (CalculateDistance() < 1)
        {
            currState = States.Attack;
            if (target == player)
            {
                playerUnit.TakeDamage(myUnit.myDamage);
            }
            else if (target == companion)
            {
                companionUnit.TakeDamage(myUnit.myDamage);
            }
        }
        hasActionFinished = true;
        anim.SetTrigger("Attack");

    }
}
