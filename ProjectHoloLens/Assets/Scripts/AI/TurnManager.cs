using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    StateMachine stateMachine; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void EnemyTurns()
    {
        for(int i = 0; i < UnitManager.unitManager.enemyUnits.Count; i++) {
            stateMachine = UnitManager.unitManager.enemyUnits[i].GetComponent<StateMachine>();
            stateMachine.BehaviourLoop();
        }
    }
}
