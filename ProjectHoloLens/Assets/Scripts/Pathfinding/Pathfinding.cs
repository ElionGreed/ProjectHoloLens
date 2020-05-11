using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Pathfinding : MonoBehaviour
{
    PathRequestManager requestManager;
    GridManager grid;

    void Awake()
    {
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<GridManager>();
    }

    //find manhattan distance
    int GetDistance(Nodess nodeA, Nodess nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector3[] pathInV3 = new Vector3[0];

        bool pathSuccess = false;


        Nodess startNode = grid.NodeFromWorldPoint(startPos);
        Nodess targetNode = grid.NodeFromWorldPoint(targetPos);

        //ensure both nodes are walkable
        if (startNode.walkable && targetNode.walkable)
        {
            //initialise open and closed lists
            Heap<Nodess> openSet = new Heap<Nodess>(grid.MaxSize);
            HashSet<Nodess> closedSet = new HashSet<Nodess>();
            //add start node to open list
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                //remove nodes from open list and add them to closed list until path is found
                Nodess currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                //if we're at the targetnode, path is found
                if (currentNode == targetNode)
                {
                    pathSuccess = true;
                    break;
                }

                //if not at target yet
                //find neighbours of current node
                foreach (Nodess neighbour in grid.GetNeighbours(currentNode))
                {
                    //ignore if neighbour is unwalkable or is already in the closed list
                    if (!neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    //cost from current node to neighbour = current node's g cost + est manhattan distance
                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                    //if the cost to neighbour is less than the neighbour's g cost, and neighbour isn't in open list
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        //set default properties of the neighbouring node 
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        //add neighbour to open list
                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
        }

        yield return null;

        if (pathSuccess)
        {
            //waypoints = RetracePath(startNode, targetNode);
            pathInV3 = RetracePath(startNode, targetNode);
        }

        //requestManager.FinishedProcessingPath(waypoints, pathSuccess);
        requestManager.FinishedProcessingPath(pathInV3, pathSuccess);
    }

    //retrace/reverse the nodes to find the path in the right order
    Vector3[] RetracePath(Nodess startNode, Nodess endNode)
    {
        //list for path (in nodes form)
        List<Nodess> path = new List<Nodess>();
        Nodess currentNode = endNode;


        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        //the result of path is now complete and in Nodess type
        //it needs to be in vector3:

        Vector3[] pathInV3 = pathToV3(path);
        Array.Reverse(pathInV3);
        return pathInV3;

        //List<Vector3> vPath = new List<Vector3>();
    }

    Vector3[] pathToV3(List<Nodess> path)
    {
        //path in nodess
        List<Vector3> pathV3 = new List<Vector3>();
        for (int i = 0; i < path.Count; i++)
        {
            pathV3.Add(path[i].worldPosition);
        }

        return pathV3.ToArray();
    }
}