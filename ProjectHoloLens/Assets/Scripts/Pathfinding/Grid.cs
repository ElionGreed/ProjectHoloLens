using UnityEngine;
using System.Collections;
using System;

public class Grid : MonoBehaviour
{
    private static Grid s_Instance = null;

    public static Grid instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(Grid)) as Grid;
                if (s_Instance == null)
                {
                    Debug.Log("No Grid found.");
                }
            }
            return s_Instance;
        }
    }

    public int numOfRows;
    public int numOfColumns;
    public float gridCellSize;
    public bool displayGridGizmos = true;
    public bool displayObstaclesGizmos = true;

    private Vector3 origin = new Vector3();
    private GameObject[] ObstaclesList;
    //all nodes in grid
    public Nodes[,] nodes { get; set; }
    public Vector3 Origin { get { return origin; } }

    void Awake()
    {
        //add obstacles to obstacle arraylist
        ObstaclesList = GameObject.FindGameObjectsWithTag("Unwalkable");
        if (ObstaclesList.Length < 5)
        {
            Debug.Log("Have you given all your obstacles the Unwalkable tag?");
            CalculateObstacles();
        }
    }

    //setting up the 2d array for nodes
    private void CalculateObstacles()
    {
        //2d array of nodes
        nodes = new Nodes[numOfColumns, numOfRows];
        int index = 0;

        for (int i = 0; i < numOfColumns; i++)
        {
            for (int j = 0; j < numOfRows; j++)
            {
                //create nodes (default properties)
                Vector3 cellPos = GetGridCellCentre(index);
                Nodes node = new Nodes(cellPos);
                nodes[i, j] = node;
                index++; //unique index for each cell
            }
        }

        if (ObstaclesList != null && ObstaclesList.Length > 0)
        {
            foreach (GameObject node in ObstaclesList)
            {
                //information about which cells the obstacles are occupying
                int indexCell = GetGridIndex(node.transform.position);
                int col = GetColumn(indexCell);
                int row = GetRow(indexCell);
                nodes[row, col].MarkAsObstacle(); //makes unwalkable = true
            }
        }
    }

    //world coordinated of grid cell based on cell's index
    public Vector3 GetGridCellCentre(int index)
    {
        Vector3 cellPos = GetGridCellPosition(index);
        cellPos.x += (gridCellSize / 2.0f); //(/2 to get the centre)
        cellPos.z += (gridCellSize / 2.0f);
        return cellPos;
    }

    private Vector3 GetGridCellPosition(int index)
    {
        int row = GetRow(index);
        int col = GetColumn(index);
        float xPosInGrid = col * gridCellSize;
        float zPosInGrid = row * gridCellSize;

        return origin + new Vector3(xPosInGrid, 0.0f, zPosInGrid); //positon of cell relative to origin (0)
    }

    //find grid cell index from the grid position
    public int GetGridIndex(Vector3 position)
    {
        if (!IsInBounds(position))
        {
            return -1;
        }
        position -= origin;
        int col = (int)(position.x / gridCellSize);
        int row = (int)(position.z / gridCellSize);

        return (row * numOfColumns + col);
    }

    //if position is within the grid
    public bool IsInBounds(Vector3 position)
    {
        float width = numOfColumns * gridCellSize;
        float height = numOfRows * gridCellSize;
        return (position.x >= origin.x
            && position.x <= origin.x + width
            && position.x <= origin.z + height
            && position.z >= origin.z); //if fits within the x and z margins true will be returned
    }

    //row cell with given index is in
    private int GetRow(int index)
    {
        int row = index / numOfColumns; //formula (for 0-based index) row=index/width, col=index%width 
        return row;
    }

    //col cell with given index is in
    private int GetColumn(int index)
    {
        int col = index % numOfColumns;
        return col;
    }

    //will be used by Pathfinding class to find neighbours of a given node
    public void GetNeighbours(Nodes node, ArrayList neighbours)
    {
        Vector3 neighbourPos = node.worldPosition; //world position of node 
        int neighbourIndex = GetGridIndex(neighbourPos); //index of that node

        int row = GetRow(neighbourIndex); //row node is in
        int column = GetColumn(neighbourIndex); //column node is in

        //going through nodes surround the node on each side and adding them to neighbours
        //bottom 
        int leftNodeRow = row - 1;
        int leftNodeCol = column;
        AssignNeighbour(leftNodeRow, leftNodeCol, neighbours);

        //top
        leftNodeRow = row + 1;
        leftNodeCol = column;
        AssignNeighbour(leftNodeRow, leftNodeCol, neighbours);

        //right 
        leftNodeRow = row;
        leftNodeCol = column + 1;
        AssignNeighbour(leftNodeRow, leftNodeCol, neighbours);

        //left 
        leftNodeRow = row;
        leftNodeCol = column - 1;
        AssignNeighbour(leftNodeRow, leftNodeCol, neighbours);

    }

    private void AssignNeighbour(int row, int col, ArrayList neighbours)
    {
        //if valid and within the region
        if (row != -1 &&
            col != -1 &&
            row < numOfRows &&
            col < numOfColumns)
        {
            Nodes nodeToAdd = nodes[row, col];
            //ensuring node to add is not an obstacle
            if (!nodeToAdd.unwalkable)
            {
                neighbours.Add(nodeToAdd);
            }
        }
    }


    void OnDrawGizmos()
{
    if (displayGridGizmos)
    {
        DrawGridGizmos(transform.position, numOfRows, numOfColumns, gridCellSize, Color.blue);
    }
    Gizmos.DrawSphere(transform.position, 0.5f);
    if (displayObstaclesGizmos)
    {
        Vector3 cellSize = new Vector3(gridCellSize, 1.0f, gridCellSize);

        if (ObstaclesList != null && ObstaclesList.Length > 0)
        {
            foreach (GameObject ob in ObstaclesList)
            {
                Gizmos.DrawCube(
                    GetGridCellCentre(GetGridIndex(ob.transform.position)),
                    cellSize);
            }
        }
    }

}

private void DrawGridGizmos(Vector3 origin, int numOfRows, int numOfColumns, float CellSize, Color color)
{
    float width = (numOfColumns * CellSize);
    float height = (numOfRows * CellSize);
    Gizmos.color = color;
    //horizontal grid lines
    for (int i = 0; i < numOfRows; i++)
    {
        Vector3 startPos = origin + i * CellSize * new Vector3(0.0f, 0.0f, 1.0f);
        Vector3 endPos = startPos + width * new Vector3(1.0f, 0.0f, 0.0f);
        Gizmos.DrawLine(startPos, endPos);
    }
    //vertical grid lines
    for (int i = 0; i < numOfColumns; i++)
    {
        Vector3 startPos = origin + 1 * CellSize * new Vector3(1.0f, 0.0f, 0.0f);
        Vector3 endPos = startPos + height * new Vector3(0.0f, 0.0f, 1.0f);
        Gizmos.DrawLine(startPos, endPos);
    }
}
}