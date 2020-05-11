﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    StateMachine stateMachine;
    CompanionAI companionAI;
    [SerializeField]
    Button button;
    bool isEnemyDone;
    bool isCompanionDone;

    private void Start()
    {
        StartCoroutine(WaitForLoad());
    }

    private IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(0.2f);
        Initialize();
    }

    private void Initialize()
    {
         companionAI = UnitManager.unitManager.companion.GetComponent<CompanionAI>();
    }
    public void EndTurn()
    {
        isEnemyDone = false;
        isCompanionDone = false;
        button.interactable = false;
        StartCoroutine(CheckIfEnemyFinished());
        GameControl.control.turn++;
    }

    // Update is called once per frame
    private void EnemyTurns()
    {
        for (int i = 0; i < UnitManager.unitManager.enemyUnits.Count; i++) {
            stateMachine = UnitManager.unitManager.enemyUnits[i].GetComponent<StateMachine>();
            stateMachine.BehaviourLoop();
        }
        StartCoroutine(CheckIfEnemyFinished());
    }
    private void CompanionTurn()
    {
        companionAI.CalculateUtility();
        StartCoroutine(CheckIfCompanionFinished());
    }


    private IEnumerator CheckIfEnemyFinished()
    {
        int i = 0;
        while (i < UnitManager.unitManager.enemyUnits.Count)
        {
            stateMachine = UnitManager.unitManager.enemyUnits[i].GetComponent<StateMachine>();
            stateMachine.BehaviourLoop();
            while (stateMachine.hasActionFinished == false)
            {
                yield return null;
            }
            i++;
            yield return null;
        }  
        CompanionTurn();
    }

    private IEnumerator CheckIfCompanionFinished()
    {
        while (isCompanionDone == false)
        {
            if(companionAI.hasActionFinished == true)
            {
                isCompanionDone = true;
            }
            yield return null;
        }
        button.interactable = true;
    }


}
