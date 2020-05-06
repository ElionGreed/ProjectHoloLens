using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DungeonGen : MonoBehaviour
{

    private int dungeonWid;
    private int dungeonLen;

    
    List<RoomNodes> allspaceNodes = new List<RoomNodes>();

    public DungeonGen(int dungeonWid, int dungeonLen)
    {
        this.dungeonWid = dungeonWid;
        this.dungeonLen = dungeonLen;
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Node> CalculateRooms(int maxIter, int roomWid, int roomLen)
    {
        BinarySpacePartitioner BSP = new BinarySpacePartitioner(dungeonWid, dungeonLen);
        allspaceNodes = BSP.PrepareNodesCollection(maxIter, roomWid, roomLen);
        List<Node> roomSpaces = StructureHelper.TraverseGraphToExtractLowestLeafes(BSP.Rootnod);

        RoomGenerate roomGenerate = new RoomGenerate(maxIter, roomLen, roomWid);
        List<RoomNodes> roomlist = roomGenerate.GenerateRoomsInGivenSpaces(roomSpaces);
        return new List<Node>(allspaceNodes);
    }
}
