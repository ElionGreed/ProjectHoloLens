using System;
using System.Collections;
using UnityEngine;

public class PathRequestManager : MonoBehaviour
{
    [SerializeField]
    public Transform startPos, endPos;
    public Nodess startNode { get; set; }
    public Nodess goalNode { get; set; }

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
            //*******DON'T UPDATE IF WE GOING TURN-BASED
            FindPath();
        }
    }

    public void MoveUnit(GameObject unit, int _numOfMoves)
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
        startNode = new Nodess(GridManager.instance.GetGridCellCentre
            (GridManager.instance.GetGridIndex(startPos.position)));
        goalNode = new Nodess(GridManager.instance.GetGridCellCentre
            (GridManager.instance.GetGridIndex(endPos.position)));

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
            foreach(Nodess node in pathArray)
            {
                if (index < pathArray.Count)
                {
                    Nodess nextNode = (Nodess)pathArray[index];
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(node.worldPosition, nextNode.worldPosition);
                    index++;
                }
            }
        }
    }
}