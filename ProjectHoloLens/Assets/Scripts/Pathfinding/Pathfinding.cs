using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Pathfinding : MonoBehaviour
{
    public static PriorityQueue openList;
    public static HashSet<Nodess> closedList;

    private static float HeuristicCost(Nodess currentNode, Nodess targetNode)
    {
        Vector3 HeuCost = currentNode.worldPosition - targetNode.worldPosition;
        return HeuCost.magnitude;
    }

    public static ArrayList FindPath(Nodess start, Nodess goal)
    {
        //initialise open and closed lists
        //place start node in open list
        //process rest of open list
        openList = new PriorityQueue();
        openList.Push(start);
        start.totalCostToHere = 0.0f;
        start.estimatedCostToTarget = HeuristicCost(start, goal);

        closedList = new HashSet<Nodess>();
        Nodess node = null;

        while (openList.Length != 0)
        {
            node = openList.First(); //node with lowest F
            //ensure we're not at target node already
            if (node.worldPosition == goal.worldPosition)
            {
                return TracePath(node);
            }

            ArrayList neighbours = new ArrayList();
            GridManager.instance.GetNeighbours(node, neighbours);

            for (int i = 0; i < neighbours.Count; i++)
            {
                Nodess neighbouringNode = (Nodess)neighbours[i];

                //if not already in closed list
                if (!closedList.Contains(neighbouringNode))
                {
                    //path cost from node to its neighbours
                    float cost = HeuristicCost(node, neighbouringNode);

                    //updating the default cost values & adding parent
                    float totalCost = node.totalCostToHere + cost;
                    float neighbourNodeEstCost = HeuristicCost(neighbouringNode, goal);

                    neighbouringNode.totalCostToHere = totalCost;
                    neighbouringNode.parent = node; //set as successor
                    neighbouringNode.estimatedCostToTarget = totalCost + neighbourNodeEstCost;

                    if (!openList.Contains(neighbouringNode))
                    {
                        //add to open list if not there already
                        openList.Push(neighbouringNode);
                    }
                }
                closedList.Add(node);
                openList.Remove(node);
            }
            if (node.worldPosition != goal.worldPosition)
            {
                Debug.Log("Goal not found");
                return null;
            }
        }
        return TracePath(node);
    }

    //adding nodes to path list (in correct order)
    private static ArrayList TracePath(Nodess node)
    {
        ArrayList path = new ArrayList();
        while (node != null)
        {
            path.Add(node);
            node = node.parent;
        }
        path.Reverse();
        return path;
    }
}