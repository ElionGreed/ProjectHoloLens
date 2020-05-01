using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Dlite : MonoBehaviour
{
    GridLite grid;
    private NodeLite startNode;
    private NodeLite targetNode;
    //when the start node moves, the old start node becomes lastnode, and the new one becomes the new startnode
    private NodeLite lastNode;
    //key modifier, used to account for the changing heuristic values as the start node moves, it is added to new items added to the priority
    private double km;
    //cost?
    private double C1 = 1.0;

    double rhs;
    double g;
    //a big list would rather be a hashset - for better performance

    //list containing nodes that make up shortest path
    List<NodeLite> path = new List<NodeLite>();
    //open list
    Heap<NodeLite> openList;
    //closed list
    HashSet<NodeLite> closedList;

    PathManager manager;

    void Awake()
    {
        //get grid component 
        grid = GetComponent<GridLite>();
        manager = GetComponent<PathManager>();
        
    }

    //find path - first iteration

    bool init(Vector3 startPos, Vector3 targetPos)
    {
        //retrieving coordinates of the start and end nodes on the grid
        startNode = grid.NodeCoordinates(startPos);
        targetNode = grid.NodeCoordinates(targetPos);

        if (startNode.walkable && targetNode.walkable)
        {
            //openList = new Heap<NodeLite>(grid.gridSize);

            //initialise key modifier = 0
            km = 0;
            //targetnode's starting rhs = 0, all other nodes start w/ +ive infinity
            targetNode.rhs = 0.0;
            //targetNode.g = C1; //g = cost?
            //targetNode.g = Mathf.Infinity;
            //add targetnode to the openlist with key<x,y>
            //calcKey(targetNode);
            AddToOpenList(targetNode);

            print(targetNode.g);
            return true;
        }
        else
        {
            print("start or target node not walkable");
            return false;
        }
        //initialise all g and rhs values = infinity, except for target node w/ rhs = 0
        //when g=rhs - node is locally consistent

        //add goal node to queue w/ key<h,rhs>
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos, bool isFollowing)
    {
        StartCoroutine(FindPath(startPos, targetPos, isFollowing));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos, bool isFollowing)
    {
        Stopwatch sw = new Stopwatch();

        if (!isFollowing)
        {
            if(init(startPos, targetPos))
            {
                print("hello");
            }
        }
        yield return null;
    }


    //---------------------openList functions
    void AddToOpenList(NodeLite n)
    {
        openList.Add(n);
    }

    void RemoveFromOpenList()
    {
        openList.RemoveFirst();
    }

    void UpdateOpenList(NodeLite n)
    {
        openList.UpdateItem(n);
    }

    //these functions would be in the node class, right?7
    //double calcRHS(NodeLite n)
    //{

    //}

    //double calcG(NodeLite n)
    //{

    //}

    double calcKey(NodeLite n)
    {
        double key = Math.Min(n.g, n.rhs) + Heuristic(startNode, targetNode) + km + Math.Min(n.g, n.rhs);
        //key first and key second?
        return key;
    }

    void PlanInitialPath(Vector3 startPos, Vector3 targetPos)
    {
        NodeLite startNode = grid.NodeCoordinates(startPos);
        NodeLite targetNode = grid.NodeCoordinates(targetPos);

        //openlist containing nodes to be evaluated
        //List<NodeLite> openList = new List<NodeLite>();

        //HashSet<NodeLite> 

        while (openList.Count > 0)
        {
            for(int i = 0; i<openList.Count; i++)
            {
            
            }
        }

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
        //return D * (dx + dy) + (D2 - 2 * D) * min(dx, dy);
        return dx + dy;
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


