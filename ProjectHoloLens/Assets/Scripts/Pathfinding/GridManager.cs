using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public bool displayGridGizmos;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public Nodess[,] grid;

    float nodeDiameter;
    int gridWidth, gridHeight;

    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        gridWidth = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridHeight = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    public int MaxSize
    {
        get
        {
            return gridWidth * gridHeight;
        }
    }

    void CreateGrid()
    {
        grid = new Nodess[gridWidth, gridHeight];
        //start creating from origin (bottom left corner)
        Vector3 gridOrigin = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 worldPoint = gridOrigin + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new Nodess(walkable, worldPoint, x, y);
            }
        }
    }

    //get neighbours of a node
    public List<Nodess> GetNeighbours(Nodess node)
    {
        List<Nodess> neighbours = new List<Nodess>();

        //check positions around given node on x (1, 0, -1)
        for (int x = -1; x <= 1; x++)
        {
            //check positions around given node on y (1, 0, -1)
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                //if x and y position are within the bounds of the grid, add the node to neighbours list
                if (checkX >= 0 && checkX < gridWidth && checkY >= 0 && checkY < gridHeight)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    //get the position of the node in the grid array based on its position in the world
    public Nodess GetNodeFromWorldPos(Vector3 worldPos)
    {
        //how far node is along gridworldslize as %
        //percentage  = (worlspos + 1/2 grid world size) / world size
        float percentX = (worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPos.z + gridWorldSize.y / 2) / gridWorldSize.y;

        //clamp b/w 0-1 to ensure world no faulty values are entered into grid[,]
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        //(-1 to account for array index starting at 0
        int x = Mathf.RoundToInt((gridWidth - 1) * percentX);
        int y = Mathf.RoundToInt((gridHeight - 1) * percentY);
        return grid[x, y];
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if (grid != null && displayGridGizmos)
        {
            foreach (Nodess n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}