using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class TrollBehaviour : MonoBehaviour
{

    //PlayerHealth playerHealth;
    private enum States { Patrol, Move, Attack, Dead }
    [SerializeField]
    private States currState;

    NavMeshAgent agent;
    Animator anim;
    GameObject player;
    GameObject[] waypoints;
    [SerializeField]
    float enemyHealth = 6;
    float distance = 0;
    bool attacked = false;
    int damage = 2;
    bool damaged;
    bool dead = false;
    int way = 0;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        currState = States.Patrol;
        player = GameObject.FindGameObjectWithTag("Player");
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
       // playerHealth = player.GetComponent<PlayerHealth>();


    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
        damaged = false;
        if (player != null)
        {
            distance = Vector3.Distance(transform.position, player.transform.position);
        }
        else
        {
            distance = 0;
        }
        switch (currState)
        {
            case States.Patrol:
                {
                    Patrol();
                    break;
                }
            case States.Move:
                {
                    //  anim.SetTrigger("Move");
                    MoveToTarget();
                    break;
                }
            case States.Attack:
                {
                    AttackEnemy();
                    break;
                }
            case States.Dead:
                {
                    Die();
                    break;
                }
        }

    }

    private void MoveToTarget()
    {
        if (distance > 1 + agent.stoppingDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);

        }
        else
        {
            agent.isStopped = true;
            currState = States.Attack;
        }
    }
    private void Patrol()
    {
        if (distance > 30)
        {
            agent.SetDestination(waypoints[way].transform.position);
        }
        if (distance < 30)
        {
            agent.isStopped = true;
            currState = States.Move;
        }
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending && distance > 30)
        {
            way++;
            if (way == 2)
            {
                way = 0;
            }
        }

    }
    private void AttackEnemy()
    {
        if (distance > 1 + agent.stoppingDistance)
        {
            currState = States.Move;
        }
        if (distance > 30 + agent.stoppingDistance)
        {
            currState = States.Patrol;
        }
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("attack1"))
        {
            anim.SetTrigger("Attack"); // make sure to change name
            attacked = true;
        }

        if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("attack1") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && distance < 1 + agent.stoppingDistance)
        {
            if (attacked != false)
            {
               // playerHealth.TakeDamage(damage);
                attacked = false;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        Debug.Log("Took Damage");
        damaged = true;
        enemyHealth -= amount;
        if (enemyHealth < 0.1)
        {
            currState = States.Dead;
        }
    }

    private void Die()
    {
        if (dead != true)
        {
            agent.isStopped = true;
           // GameControl.control.kills += 1;
            anim.SetTrigger("death");
            dead = true;
            Destroy(gameObject, 7f);
        }
    }
}
