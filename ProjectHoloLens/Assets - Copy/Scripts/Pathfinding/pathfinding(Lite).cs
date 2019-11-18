//using System.Collections.Generic;
//using System.Diagnostics;
//using UnityEngine;

//public class Pathfinding : MonoBehaviour
//{
//    public GameObject obstacle;
//    private Node startNode;
//    private Node endNode;
//    private Node lastNode;
//    Vector3 nextMove;

//    //list of **
//    List<Node> updatedNodes = new List<Node>();
//    //list of observed nodes
//    List<Node> updatedObservedNodes = new List<Node>();
//    //list of path nodes
//    List<Node> path = new List<Node>();
//    //private Heap<Node> openSet;
//    public static int visitedCount;
//    public static int updatedCount;

//    private void Awake()
//    {
        
//    }

//    public void StartFindPath(Vector3 startPos, Vector3 targetPos, bool isFollowing)
//    {
//        //StartCoroutine(FindPath(startPos, targetPos, isFollowing));
//    }

//    public void FindPath(Vector3 startPos, Vector3 targetPos, bool isFollowing)
//    {
//        Stopwatch sw = new Stopwatch();
//        sw.Start();

//        bool replanReturn = false;

//        //if not following
//        if (!isFollowing)
//        {
//            if (Init(startPos, targetPos))
//            {
//                //replanReturn = Replan();
//            }
//        }
//        else
//        {
            
//        }
//    }

//    private bool Init(Vector3 startPos, Vector3 targetPos)
//    {
//        //get start node from grid
//        //get target node from grid

//        //if both start and target nodes are walkable (not obstacles)
//        if(startNode.walkable && endNode.walkable)
//        {
//            //openSet = new Heap<Node>(grid.MaxSize);

//            //k_m = 0;
//            //endNode.rhs = 0.0;
//            //endNode.cost = C1;

//            //Pair<double, double> k = calculateKey(targetNode);
//            //endNode.k = k;
//            //listInsert(endNode);
//            //lastNode = startNode;
//            return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//    //public bool Replan()
//    //{
//    //    path.Clear();

//    //    int result = computeShortestPath();
        
//    //    if (result != 0)
//    //    {
//    //        return false;
//    //        Node current = startNode;
//    //        //add current node to the path
//    //        path.Add(current);

//    //    }

//    //}

//    private int computeShortestPath()
//    {
//        //if (openSet.Count == 0)
//        //{
//        //    return 1;
//        //}

//        int numStep = 0;

//        Node u;
    
//          return 0; 
//    }


//    private void Update()
//    {
        
//    }

//    //private double RHS(Node node, double value = double.MinValue)
//    //{
//    //    //if (node.eq(endNode){
//    //    //    return 0;
//    //    //}

//    //}
//}
