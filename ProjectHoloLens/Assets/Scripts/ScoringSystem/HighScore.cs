using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour {
    public TextMesh TextM;
    public float[] scoreTable = new float[10];
    // Use this for initialization
    void Start () {
		if(GameControl.control.gameStarted == false)
        {
            GameControl.control.Load();
        }
        scoreTable = GameControl.control.scoreTable;
        Sort();
       for (int i = 0; i < 9; i++)
       {
            TextM.text += ((i+1).ToString()+ "." + scoreTable[i].ToString() + "\r\n");
       }
        GameControl.control.scoreTable = scoreTable;
        GameControl.control.Save();
    }

    // Update is called once per frame
    public void Sort()
    {
        float temp;
        int i, j;
        for (i = 0; i < 9; i++)
        {

            for (j = 0; j < 9 - i; j++)
                if (scoreTable[j] < scoreTable[j + 1])
                {
                    temp = scoreTable[j];
                    scoreTable[j] = scoreTable[j + 1];
                    scoreTable[j + 1] = temp;
                }
     }
    }

}
