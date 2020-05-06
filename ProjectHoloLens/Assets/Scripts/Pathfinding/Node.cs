﻿using UnityEngine;
using System;

public class Node : IComparable
{
    //total cost (G cost) - distance from start node to this node
    public float totalCostToHere;
    //estimated cost (H cost) - estimated distance from this node to target node (manhattan distance)
    public float estimatedCostToTarget;
    public bool unwalkable; //obstacle
    public Node parent;
    public Vector3 worldPosition;

    public Node()
    {
        this.estimatedCostToTarget = 0.0f;
        this.totalCostToHere = 0.0f;
        this.unwalkable = false;
        this.parent = null;
    }
    
    public Node(Vector3 pos)
    {
        this.estimatedCostToTarget = 0.0f;
        this.totalCostToHere = 1.0f;
        this.unwalkable = false;
        this.parent = null;
        this.worldPosition = pos;
    }

    public void MarkAsObstacle()
    {
        this.unwalkable = true;
    }

    //public float TotalCost
    //{
    //    get
    //    {
    //        return estimatedCostToTarget + totalCostToHere;
    //    }
    //}

    //compare costs to sort nodes in queue (sort function will look for compareto().
    public int CompareTo(object obj)
    {
        Node node = (Node)obj;
        
        //if this node should be lower in queue (lower cost) than obj, return -1
        if (this.estimatedCostToTarget < node.estimatedCostToTarget)
            return - 1;

        if (this.estimatedCostToTarget > node.estimatedCostToTarget)
            return 1;

        return 0;
    }
}