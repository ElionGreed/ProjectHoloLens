
using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerate
{
    private int maxIter;
    private int roomLen;
    private int roomWid;

    public RoomGenerate(int maxIter, int roomLen, int roomWid)
    {
        this.maxIter = maxIter;
        this.roomLen = roomLen;
        this.roomWid = roomWid;
    }

    internal List<RoomNodes> GenerateRoomsInGivenSpaces(List<Node> roomSpaces)
    {
        throw new NotImplementedException();
    }
}