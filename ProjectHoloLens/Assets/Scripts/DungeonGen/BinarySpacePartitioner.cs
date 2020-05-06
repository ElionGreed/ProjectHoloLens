using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BinarySpacePartitioner
{
    RoomNodes rootnod;
    public RoomNodes Rootnod { get => this.rootnod; }

    public BinarySpacePartitioner(int dungeonWid, int dungeonLen)
    {
        this.rootnod = new RoomNodes(new Vector2Int(0, 0), new Vector2Int(dungeonWid, dungeonLen), null, 0);
    }

    public List<RoomNodes> PrepareNodesCollection(int maxIter, int roomWid, int roomLen)
    {
        Queue<RoomNodes> grap = new Queue<RoomNodes>();
        List<RoomNodes> ListReturn = new List<RoomNodes>();
        grap.Equals(this.rootnod);
        ListReturn.Add(this.rootnod);

        int iterations = 0;
        while(iterations < maxIter && grap.Count > 0)
        {
            iterations++;
            RoomNodes CurrentNode = grap.Dequeue();
            if (CurrentNode.Width >= roomWid * 2 || CurrentNode.Length >= roomLen * 2)
            {
                SplitTheSpace(CurrentNode, ListReturn, roomLen, roomWid, grap);
            }
        }
        return ListReturn;
    }

    private void SplitTheSpace(RoomNodes currentNode, List<RoomNodes> ListReturn, int roomLen, int roomWid, Queue<RoomNodes> grap)
    {
        Line line = GetLineDividingSpace(currentNode.BottomLeftCorner,currentNode.TopRightCorner, roomWid, roomLen);

        RoomNodes node1, node2;
        if (line.Orientation == Orientation.Horizontal)
        {
            node1 = new RoomNodes(currentNode.BottomLeftCorner,
                new Vector2Int(currentNode.TopRightCorner.x, line.Coordinates.y),
                currentNode,
                currentNode.LayerIndex + 1);
            node2 = new RoomNodes(new Vector2Int(currentNode.BottomLeftCorner.x, line.Coordinates.y),
                currentNode.TopRightCorner,
                currentNode,
                currentNode.LayerIndex + 1);
        }
        else
        {
            node1 = new RoomNodes(currentNode.BottomLeftCorner,
                new Vector2Int(line.Coordinates.x, currentNode.TopRightCorner.y),
                currentNode,
                currentNode.LayerIndex + 1);
            node2 = new RoomNodes(new Vector2Int(line.Coordinates.x, currentNode.BottomLeftCorner.y),
                currentNode.TopRightCorner,
                currentNode,
                currentNode.LayerIndex + 1);
        }
        AddNewNodeToCollections(ListReturn, grap, node1);
        AddNewNodeToCollections(ListReturn, grap, node2);
    }

    private void AddNewNodeToCollections(List<RoomNodes> ListReturn, Queue<RoomNodes> grap, RoomNodes node)
    {
        ListReturn.Add(node);
        grap.Enqueue(node);
    }

    private Line GetLineDividingSpace(Vector2Int BottomLeftCorner, Vector2Int TopRightCorner, int roomWid, int roomLen)
    {
        Orientation orientation;
        bool lengthStatus = (TopRightCorner.y - BottomLeftCorner.y) >= 2 * roomLen;
        bool widthStatus = (TopRightCorner.x - BottomLeftCorner.x) >= 2 * roomWid;
        if (lengthStatus && widthStatus)
        {
            orientation = (Orientation)(UnityEngine.Random.Range(0, 2));
        }
        else if (widthStatus)
        {
            orientation = Orientation.Vertical;
        }
        else
        {
            orientation = Orientation.Horizontal;
        }
        return new Line(orientation, GetCoordinatesFororientation(orientation, BottomLeftCorner, TopRightCorner, roomWid, roomLen));
    }

    private Vector2Int GetCoordinatesFororientation(Orientation orientation, Vector2Int BottomLeftCorner, Vector2Int TopRightCorner, int roomWid, int roomLen)
    {
        Vector2Int coordinates = Vector2Int.zero;
        if (orientation == Orientation.Horizontal)
        {
            coordinates = new Vector2Int(
                0,
                Random.Range(
                (BottomLeftCorner.y + roomLen),
                (TopRightCorner.y - roomWid)));
        }
        else
        {
            coordinates = new Vector2Int(
                Random.Range(
                (BottomLeftCorner.x + roomLen),
                (TopRightCorner.x - roomWid))
                , 0);
        }
        return coordinates;
    }
}