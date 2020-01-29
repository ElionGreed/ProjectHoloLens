using UnityEngine;
using System.Collections;

public class NodeLite : IHeapItem<NodeLite>
{
    //state of node
    public bool walkable;
    //the position of the node in the world
    public Vector3 worldPosition;

    public int gridX;
    public int gridY;
    //    public double rhs;
    //    public double cost;
    //    public int gCost;
    //    public int hCost;
    //    int heapIndex; //***

    //constructor 
    public NodeLite(bool _walkable, Vector3 _worldPosition, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPosition;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int HeapIndex { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public int CompareTo(NodeLite other)
    {
        throw new System.NotImplementedException();
    }

    //    public int FCost
    //    {
    //        get
    //        {
    //            return gCost + hCost;
    //        }
    //    }

    //    public int HeapIndex
    //    {
    //        get
    //        {
    //            return HeapIndex;
    //        }
    //    }

    //    public int CompareTo(Node nodeToCompare)
    //    {
    //        //compare is the f cost of the node being compared to
    //        int compare = FCost.CompareTo(nodeToCompare.FCost);

    //        //if compare is 0, meaning that f cost is 0
    //        if (compare == 0)
    //        {
    //            //set compare to the h cost of the node being comapred to
    //            compare = hCost.CompareTo(nodeToCompare.hCost);
    //        }
    //        //reutn the negated value of compare
    //        return -compare;
    //    }

}
