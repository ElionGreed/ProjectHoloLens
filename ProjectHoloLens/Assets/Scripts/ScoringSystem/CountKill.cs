using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountKill : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GameControl.control.gameStarted = true;
        GameControl.control.kills = 0;
        GameControl.control.score = 0;
        GameControl.control.turn = 0;

    }
	
	// Update is called once per frame

}
