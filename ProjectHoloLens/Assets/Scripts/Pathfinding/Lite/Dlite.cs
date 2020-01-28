using System;
using System.Collections.Generic;
using UnityEngine;

public class Dlite : MonoBehaviour
{
    GridLite grid;
    
    //a big list would rather be a hashset - for better performance


    private void Awake()
    {
        //get grid component 
        grid = GetComponent<GridLite>();
    }

    //find path - first iteration

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        NodeLite startNode = grid.NodeCoordinates(startPos);
        NodeLite targetNode = grid.NodeCoordinates(targetPos);

        List<NodeLite> openSet = new List<NodeLite>();

    }


    //http://theory.stanford.edu/~amitp/GameProgramming/Heuristics.html
    //On a square grid that allows 8 directions of movement, use Diagonal distance (L∞).
    double Heuristic(NodeLite start, NodeLite end)
    {
        //abs returns the absolute value
        //d = lowest cost between adjacent sqs
        //d2 = cost of moving diagonally

        double dx = Math.Abs(start.gridX - end.gridX);
        double dy = Math.Abs(start.gridY - end.gridY);
        return D * (dx + dy) + (D2 - 2 * D) * min(dx, dy);
    }

    double RealDistance(NodeLite start, NodeLite end)
    {
        double dx = start.gridX - end.gridX;
        double dy = start.gridY - end.gridY;
        //formula for straight line distance between two points:
        return Math.Sqrt(dx * dx + dy * dy);
    }
}
//     //start position
//     private node startNode; 
//     //target position
//     private node targetNode;
//     private node lastNode;
//     //list of nodes to be evaluated
//     private heap<node>openList;
//     //list of already-evaluated nodes
//     private heap<node>closedList;

//     // lastNode = node in open with lowest f
//     //remove current from oepnlist
//     //add current to closed

//     // if (lastNode == targetNode){
//     //     return;
//     // }

//     private void addToOpenList(node node)
//     {
//         openList.add(node);
//     }

// }
// // G //cost so far
// // H //heuristic - cost-to-go
// // F //g(s) + h(s)
// //8-connected graph allows for diagonal movement
// // Loop 

// // steps of incremntal search
// // 1- perform graph search 
// // 2- repeat:
// //     - execute path plan
// //     - recieve graph changes 
// //     - update previous search results

// if current is the tagret node
//     return

// for each neighbour of current node
//     if node is in closed
//         skip to next node
//     if  


//    //reusing previous search results 
// //    store optimal results for shortest path
// //    mainly storing g values 

// // next iteration:
// // find inconsistencies 
// // make them consistent through relaxation (instead of recaluating shortest path)


