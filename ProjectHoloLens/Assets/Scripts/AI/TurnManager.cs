using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    StateMachine stateMachine;
    CompanionAI companionAI;
    [SerializeField]
    Button button;
    [SerializeField]
    Button[] skillButtons;
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
    }
    public void EndTurn()
    {
        foreach(Button button in skillButtons)
        {
            button.interactable = false;
        }
        isCompanionDone = false;
        button.interactable = false;
        StartCoroutine(CheckIfEnemyFinished());
        GameControl.control.turn++;
    }

    // Update is called once per frame

    private void CompanionTurn()
    {
        companionAI = UnitManager.unitManager.companion.GetComponent<CompanionAI>();
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
        foreach (Button button in skillButtons)
        {
            button.interactable = true;
        }
    }


}
