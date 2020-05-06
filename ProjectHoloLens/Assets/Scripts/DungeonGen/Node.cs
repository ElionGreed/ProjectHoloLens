using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node
{

    private List<Node> childenNodes;
    public List<Node> ChildenNodes { get => childenNodes; }

    public bool Visted { get; set; }
    public Vector2Int BottomLeftCorner { get; set; }
    public Vector2Int BottomRightCorner { get; set; }
    public Vector2Int TopRightCorner { get; set; }
    public Vector2Int TopLeftCorner { get; set; }

    public int LayerIndex { get; set; }
    public Node Parent { get; set; }

    // Start is called before the first frame update
    public Node(Node parentNode)
    {
        childenNodes = new List<Node>();
        this.Parent = parentNode;
        if(parentNode != null)
        {
            parentNode.AddChild(this);
        }
    }

    public void AddChild(Node node)
    {
        childenNodes.Add(node);

    }

    public void RemoveChild(Node node)
    {
        childenNodes.Remove(node);
    }
}
