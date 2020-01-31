using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLite : MonoBehaviour
{
    public bool displayGridGizmos;
    //public Transform player;
    public LayerMask unwalkableMask;
    //area of grid in world coordinates
    public Vector2 gridWorldSize;
    //store radius to find out size of node
    public float nodeRadius;
    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;

    //2d array of nodes
    NodeLite[,] grid;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;

        //number of nodes that can fit into the grid, rounded so as not to end up with a decimal (incomplete node)
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    private void CreateGrid()
    {
        //new 2d array of nodes with size of gridsizex, gridsizey
        grid = new NodeLite[gridSizeX, gridSizeY];

        //transform.position = (0,0,0), vector3.right = (1,0,0), vector3.back= (0,0,-1)
        Vector3 worldBottomRight = transform.position + Vector3.right * gridWorldSize.x / 2 - Vector3.back * gridWorldSize.y / 2;

        //loop through all points in grid
        for (int x=0; x<gridSizeX; x++)
        {
            for (int y=0; y<gridSizeY; y++)
            {
                //go in increments of node diameter till edge of world is reached, for both axes
                Vector3 worldPoint = worldBottomRight + Vector3.left * (x * nodeDiameter + nodeRadius) + Vector3.back * (y * nodeDiameter + nodeRadius);

                //collision check
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask)); //checksphere return true if collision occured, so if it returns false, walkable is true
                //populating grid
                grid[x, y] = new NodeLite(walkable, worldPoint,x,y);

                //initialising rhs and g values of nodes - except target node which will be rewritten in Dlite.cs
                grid[x, y].InitNodesValues(double.PositiveInfinity, double.PositiveInfinity);

            }
        }
    }

    public int gridSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }

    public int sizeX
    {
        get
        {
            return gridSizeX;
        }
    }

    public int sizeY
    {
        get
        {
            return gridSizeY;
        }
    }

    //to find node the player is in: convert worldposition into grid coordinate
    public NodeLite NodeCoordinates(Vector3 worldPosition)
    {
        //convert world position into perfecntage based on how far along the grid it is,
        //taking into account the node radius and the position of the gameobject this code(grid) is attached to
        float percentX = (worldPosition.x - transform.position.x) / gridWorldSize.x + 0.5f - (nodeRadius / gridWorldSize.x);
        float percentY = (worldPosition.z - transform.position.z) / gridWorldSize.y + 0.5f - (nodeRadius / gridWorldSize.y);
        
        //clamping in case worldposition is outside of grid
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        //"-1" used to stay inside the array
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null)
        {
            // Node playerNode = NodeFromWorldPoint(player.position);
            foreach (NodeLite n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                /*if (playerNode == n)
                {
                    Gizmos.color = Color.cyan;
                }*/

                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }
}
