using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    public Transform start;
    public Transform target;
    float speed = 5;
    Vector3[] path;
    int targetIndex;
    public int numOfMoves = 3;
    private int remainingMoves;
    //public GameObject objToMove;

    void Start()
    {
        //PathRequestManager.RequestPath(start.transform.position, target.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("MoveUnit");
            StartCoroutine(MoveUnit(numOfMoves));
        }
    }

    IEnumerator MoveUnit(int numOfMoves)
    {
        Vector3 currentNode = path[0];

        while (true)
        {
            if (transform.position == currentNode)
            {
                targetIndex++;
                if (targetIndex >=path.Length || targetIndex == numOfMoves)
                {
                    yield break;
                }
                currentNode = path[targetIndex];
            }

            int remainingMoves = path.Length - numOfMoves;
            transform.position = Vector3.MoveTowards(transform.position, currentNode, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}