using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {
    public static GameControl control;
    public int kills;
    public int turn;
    public float score;
    public float[] scoreTable = new float[10];
    public bool gameStarted;

    private void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }else if(control != this)
        {
            Destroy(gameObject);
        }
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerScore.dat");

        PlayerData data = new PlayerData();
        data.scoreTable = scoreTable;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerScore.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerScore.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            scoreTable = data.scoreTable;
        }
       
    }


    [Serializable]
    class PlayerData
    {
       public float[] scoreTable = new float[10];
    }
}
