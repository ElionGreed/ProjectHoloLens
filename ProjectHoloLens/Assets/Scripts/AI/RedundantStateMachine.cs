using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]

public class RedundantStateMachine : MonoBehaviour
{
    //input number of states
    //input state Action
    //input to which other states each state transitions
    //input state transitions conditions 


    Animator anim;
    [SerializeField]
    GameObject player;
    bool proceedTransition = true;
    bool skipTransition = false;
    bool isMoving;
    float distance;
    Vector3 vel;
    Vector3 _prevPosition;

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
        distance = Vector3.Distance(transform.position, player.transform.position);
    }

  

   public void BehaviourLoop()
    {
        switch (currState)
        {
            case Action.Idle:
                {
                    isMoving = true;
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
                  //  AttackTarget();
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
     //   agent.isStopped = false;
        anim.SetTrigger("Move");
     //   agent.SetDestination(player.transform.position);
    }

    private void AttackEnemy()
    {
        anim.SetTrigger("Attack");
     //   player.TakeDamage(player.myDamage);
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


    private IEnumerator FaceMovementDirection()
    {
        while (transform.hasChanged)
        {
            vel = (transform.position - _prevPosition) / Time.deltaTime;
            _prevPosition = transform.position;
            transform.hasChanged = false;
            Debug.Log(vel);
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

            if (Vector3.Distance(transform.position, player.transform.position) < 1)
            {
                
            }
            yield return null;

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
