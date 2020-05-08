using UnityEngine;
using System.Collections;
using System;

public class GridManager : MonoBehaviour
{
    private static GridManager s_Instance = null;

    public static GridManager instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(GridManager)) as GridManager;
                if (s_Instance == null)
                {
                    Debug.Log("No GridManager found.");
                }
            }
            return s_Instance;
        }
    }

    public int numOfRows; //height of grid
    public int numOfColumns; //width of grid
    public float gridCellSize; //size of cell
    public bool displayGridGizmos = true;
    public bool displayObstaclesGizmos = true;

    private Vector3 origin = new Vector3();
    private GameObject[] ObstaclesList;
    //2d array of nodes in grid
    public Nodess[,] grid { get; set; }
    public Vector3 Origin { get { return origin; } }

    void Awake()
    {
        //add obstacles to obstacle array
        ObstaclesList = GameObject.FindGameObjectsWithTag("Unwalkable");
        print(ObstaclesList.Length + " Obstacles found");
        CalculateObstacles();
    }

    //find obstacles on map
    private void CalculateObstacles()
    {
        //2d array of nodes
        grid = new Nodess[numOfColumns, numOfRows];
        int index = 0;



        //------------------Node creation-------------------
        for (int i = 0; i < numOfColumns; i++)
        {
            for (int j = 0; j < numOfRows; j++)
            {
                //create nodes (default properties)
                Vector3 cellPos = GetGridCellCentre(index);
                Nodess node = new Nodess(cellPos);
                grid[i, j] = node;
                //print(index + " this index is at position " + cellPos);
                index++; //unique index for each cell
            }
        }
        //print("nodes added to nodes list. Length of list is " + grid.Length);


        //--------------managing obstacles--------------------
        if (ObstaclesList != null && ObstaclesList.Length > 0)
        {
            print("Obstacle list length is: " + ObstaclesList.GetLength(0));
            print("grid length is " + grid.Length);

            //loop through obstacles and add them to list 
            foreach (GameObject onbstacle in ObstaclesList)
            {

                //find index using position, and use it to get row and col
                //int cellIndex = GetGridIndex(onbstacle.transform.position); print(cellIndex);
                //int row = GetRow(cellIndex); print(row);
                //int col = GetColumn(cellIndex); print(col);


                //-----------------information about which cells the obstacles are occupying--------------
                int indexCell = GetGridIndex(onbstacle.transform.position);
                print("index of obstacle is " + indexCell); //-1????

                int col = GetColumn(indexCell);
                int row = GetRow(indexCell);

                print("row of cell: " + row);
                print("column of cell: " + col);

                //grid[row, col].MarkAsObstacle(); //makes unwalkable = true
                //print("NOT ADDING OBSTACLES");

                try
                {
                    grid[row, col].MarkAsObstacle(); //makes unwalkable = true
                    print("obstacle marked");
                }
                catch (Exception)
                {
                    Debug.Log("could not add obstacle to list");
                }
            }
        }
    }

    //world coordinates of grid cell based on cell's index
    public Vector3 GetGridCellCentre(int index)
    {
        Vector3 cellPos = GetGridCellPosition(index);
        cellPos.x += (gridCellSize / 2.0f); //(/2 to get the centre)
        cellPos.z += (gridCellSize / 2.0f);
        return cellPos;
    }

    //grid position of node in grid (not worldPos), relative to origin of grid rather than origin of world
    private Vector3 GetGridCellPosition(int index)
    {
        int row = GetRow(index);
        int col = GetColumn(index);
        float xPosInGrid = col * gridCellSize;
        float zPosInGrid = row * gridCellSize;

        //positon of cell relative to origin (0) 
        return Origin + new Vector3(xPosInGrid, 0.0f, zPosInGrid);
    }

    //find grid cell index from the grid position
    public int GetGridIndex(Vector3 gridPosition)
    {
        //if psoition isn't in bounds, return -1
        if (!IsInBounds(gridPosition))
        {
            Debug.Log("position of node is out of bounds");
            return -1;
        }

        //Debug.Log("in bounds yay");
        gridPosition -= Origin;
        int col = (int)(gridPosition.x / gridCellSize);
        int row = (int)(gridPosition.z / gridCellSize);

        return (row * numOfColumns + col);
    }

    //if position is within the grid
    public bool IsInBounds(Vector3 position)
    {
        float width = numOfColumns * gridCellSize;
        float height = numOfRows * gridCellSize;

        return (position.x >= Origin.x
            && position.x <= Origin.x + width
            && position.z <= Origin.x + height
            && position.z >= Origin.z); //if within these bounds return true;
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
    public void GetNeighbours(Nodess node, ArrayList neighbours)
    {
        Vector3 neighbourPos = node.worldPosition;
        int neighbourIndex = GetGridIndex(neighbourPos);
        int row = GetRow(neighbourIndex);
        int column = GetColumn(neighbourIndex);
        // Bottom 
        int leftNodeRow = row - 1;
        int leftNodeColumn = column;
        AssignNeighbour( leftNodeRow, leftNodeColumn, neighbours);
        // Top 
        leftNodeRow = row + 1;
        leftNodeColumn = column;
        AssignNeighbour( leftNodeRow, leftNodeColumn, neighbours);
        // Right 
        leftNodeRow = row; 
        leftNodeColumn = column + 1;
        AssignNeighbour( leftNodeRow, leftNodeColumn, neighbours);
        // Left 
        leftNodeRow = row;
        leftNodeColumn = column - 1;
        AssignNeighbour( leftNodeRow, leftNodeColumn, neighbours);
    }

    private void AssignNeighbour(int row, int col, ArrayList neighbours)
    {
        //if valid and within the region
        if (row != -1 &&
            col != -1 &&
            row < numOfRows &&
            col < numOfColumns)
        {
            try
            {
                Nodess nodeToAdd = grid[row, col];
                //ensuring node to add is not an obstacle
                if (!nodeToAdd.unwalkable)
                {
                    neighbours.Add(nodeToAdd);
                    print("neighbour node added");
                }
            }
            catch
            {
                Debug.Log("Failed to assign neighbour.");
            }
        }
    }


    void OnDrawGizmos()
    {
        if (displayGridGizmos)
        {
            DrawGridGizmos(transform.position, numOfRows, numOfColumns, gridCellSize, Color.blue);
        }
        //sphere at origin of grid
        Gizmos.DrawSphere(transform.position, 0.5f);

        if (displayObstaclesGizmos)
        {

            Vector3 cellSize = new Vector3(gridCellSize, 1.0f, gridCellSize);
            if (ObstaclesList != null && ObstaclesList.Length > 0)
            {
                foreach (GameObject obj in ObstaclesList)
                {
                    Gizmos.DrawCube(GetGridCellCentre
                        (GetGridIndex(obj.transform.position)),
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
            Vector3 startPos = origin + i * CellSize * new Vector3(1.0f, 0.0f, 0.0f);
            Vector3 endPos = startPos + height * new Vector3(0.0f, 0.0f, 1.0f);
            Gizmos.DrawLine(startPos, endPos);
        }
    }
}