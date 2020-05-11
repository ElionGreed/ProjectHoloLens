using UnityEngine;
using System.Collections;

public class Nodess : IHeapItem<Nodess>
{
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    //total cost (G cost) - distance from start node to this node
    public int totalCostToHere;
    //estimated cost (H cost) - estimated distance from this node to target node (manhattan distance)
    public int estimatedCostToTarget;
    public Nodess parent;
    int heapIndex;

    public Nodess(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }
    
    public int fCost
    {
        get
        {
            return totalCostToHere + estimatedCostToTarget;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    //compare f cost of nodes to 
    public int CompareTo(Nodess nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        //if fcosts of nodes are equal (comapre == 0 -> no difference in total distance), compare the h cost
        if (compare == 0)
        {
            compare = estimatedCostToTarget.CompareTo(nodeToCompare.estimatedCostToTarget);
        }
        return -compare;
    }
}