using System;
using System.Collections;
using UnityEngine;

public class PathRequestManager : MonoBehaviour
{
    private Transform startPos, endPos;
    public Nodes startNode { get; set; }
    public Nodes goalNode { get; set; }

    //array found from findpath()
    public ArrayList pathArray;
    public int numOfMoves =5;
    public int speed=20;

    //GameObject objStartCube, objEndCube;
    GameObject startObj, endObj;
    private float elapsedTime = 0.0f;
    public float intervalTime = 1.0f;

    private void Start()
    {
        startObj = GameObject.FindGameObjectWithTag("Start");
        endObj = GameObject.FindGameObjectWithTag("End");

        pathArray = new ArrayList();
        FindPath();
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= intervalTime)
        {
            elapsedTime = 0.0f;
            //*******DON'T UPDATE IF WE GOING TURN_BASED
            //FindPath();
        }
    }

    public void MoveUnit(GameObject unit)
    {
        //Node currentNode = (Node) pathArray[0];

        //for (int i = 0; i < numOfMoves; i++)
        //{
        //    if (startObj.transform.position != endObj.transform.position)
        //    {
        //        currentNode = (Node) pathArray[i];
        //    }
        //    Vector3 currentNodePos = currentNode.worldPosition;
        //    startObj.transform.position = Vector3.MoveTowards(transform.position, currentNodePos, speed);
        //}
    }

    private void FindPath()
    {
        startPos = startObj.transform;
        endPos = endObj.transform;
        startNode = new Nodes(Grid.instance.GetGridCellCentre
            (Grid.instance.GetGridIndex(startPos.position)));
        goalNode = new Nodes(Grid.instance.GetGridCellCentre
            (Grid.instance.GetGridIndex(endPos.position)));

        pathArray = Pathfinding.FindPath(startNode, goalNode);
    }

    private void OnDrawGizmos()
    {
        if (pathArray == null)
        {
            return;
        }

        if (pathArray.Count > 0)
        {
            int index = 1;
            foreach(Nodes node in pathArray)
            {
                if (index < pathArray.Count)
                {
                    Nodes nextNode = (Nodes)pathArray[index];
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(node.worldPosition, nextNode.worldPosition);
                    index++;
                }
            }
        }
    }
}