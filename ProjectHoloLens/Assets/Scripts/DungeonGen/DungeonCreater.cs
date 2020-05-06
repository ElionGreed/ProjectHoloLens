using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCreater : MonoBehaviour
{


    public int dungeonWid, dungeonLen;
    public int RoomWid, RoomLen;
    public int maxIter;
    public int corridorWid;

    // Start is called before the first frame update
    void Start()
    {
        CreateDungeon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateDungeon()
    {
        DungeonGen Generator = new DungeonGen(dungeonWid, dungeonLen);
        var listOfRooms = Generator.CalculateRooms(maxIter, RoomWid, RoomLen);
    }
}
