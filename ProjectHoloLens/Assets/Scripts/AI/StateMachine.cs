using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]

public class StateMachine : MonoBehaviour
{
    //input number of states
    //input state Action
    //input to which other states each state transitions
    //input state transitions conditions 


    NavMeshAgent agent;
    Animator anim;
    [SerializeField]
    GameObject player;
    bool proceedTransition = true;
    bool skipTransition = false;
    float distance;

    public enum Action { Idle, Move, Attack, Dead }
    public Action currState;
    public int stateIndicator;
    [SerializeField]
    //public Action currAction;
    public State[] States;
    public int myHealth = 20;



    // Start is called before the first frame update
    void Start()
    {
        stateIndicator = (int)currState;
        currState = (Action)((int)States[0].perform);
        agent = GetComponent<NavMeshAgent>();
        distance = Vector3.Distance(transform.position, player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
      //  BehaviourLoop();
    }

   public void BehaviourLoop()
    {
        switch (currState)
        {
            case Action.Idle:
                {
                    agent.isStopped = true;
                    CheckTransition();
                    break;
                }
            case Action.Move:
                {
                    MoveToTarget();
                    CheckTransition();
                    break;
                }
            case Action.Attack:
                {
                    AttackTarget();
                    break;
                }
            case Action.Dead:
                {
                    // Die();
                    break;
                }
        }

    }

    private void MoveToTarget()
    {
        agent.isStopped = false;
        anim.SetTrigger("Move");
        agent.SetDestination(player.transform.position);
    }

    private void AttackTarget()
    {
        agent.isStopped = true;
        anim.SetTrigger("Attack");
    }



    void CheckTransition()
    {
        for (int n = 0; n < States[stateIndicator].transitions.Length; n++)
        {

            if (States[stateIndicator].transitions[n].checkMyHealth == true)
            {
                if (myHealth == 0)
                {
                    proceedTransition = true;
                }
                else
                {
                    skipTransition = true;
                }
            }
            if (States[stateIndicator].transitions[n].checkDistance == true)
            {
                if (distance < 15)
                {
                    proceedTransition = true;
                }
                else
                {
                    skipTransition = true;
                }
            }
            if (States[stateIndicator].transitions[n].checkCloseDistance == true)
            {
                if (distance < 2)
                {
                    proceedTransition = true;
                }
                else
                {
                    skipTransition = true;
                }
            }
            if (proceedTransition == true && skipTransition == false)
            {

                stateIndicator = (int)States[stateIndicator].transitions[n].transitTo;
                currState = (Action)(stateIndicator);
                n = States[stateIndicator].transitions.Length;
            }
        }
    }
}


[System.Serializable]
public class State
{
    public enum Action { Idle, Move, Attack, Dead }
    public Action perform;
    public Transit[] transitions;
}


[System.Serializable]
public class Transit
{
    public enum Action { Idle, Move, Attack, Dead }
    public Action transitTo;
    public bool checkMyHealth;
    public bool checkCloseDistance;
    public bool checkDistance;

}
