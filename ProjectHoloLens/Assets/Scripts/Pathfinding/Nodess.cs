using UnityEngine;
using System.Collections;

public class Nodess : IHeapItem<Nodess>
{
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    //cost from start to current node (cost-so-far)
    public int gCost;
    //cost from current node to goal (cost-to-go)
    public int hCost;
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
            return gCost + hCost;
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
        //if fcosts of nodes are equal (no difference in total distance), compare the h cost
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}