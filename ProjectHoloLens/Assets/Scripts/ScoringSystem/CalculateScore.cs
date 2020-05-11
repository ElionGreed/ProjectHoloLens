using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateScore : MonoBehaviour {
    [SerializeField]
    public TextMesh[] TextM;
    float score;
    int kills;
    int turn;
    // Use this for initialization
    void Start () { 
    kills = GameControl.control.kills;
    turn = GameControl.control.turn;     
    score = ((kills * 10) - turn);
    TextM[0].text =("Kills: " + kills.ToString());
    TextM[1].text =("Time: " + turn.ToString());
    TextM[2].text = ("Score: " + score.ToString());
    GameControl.control.scoreTable[9] = score;
    }
	
	// Update is called once per frame
	
}
