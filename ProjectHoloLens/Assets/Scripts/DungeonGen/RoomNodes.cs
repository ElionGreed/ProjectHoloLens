using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNodes : Node
{

    public RoomNodes(Vector2Int BottomLeftCorner, Vector2Int TopRightCorner, Node parentNode, int index) : base(parentNode)
    {
        this.BottomLeftCorner = BottomLeftCorner;
        this.TopRightCorner = TopRightCorner;
        this.BottomRightCorner = new Vector2Int(TopRightCorner.x, BottomLeftCorner.y);
        this.TopLeftCorner = new Vector2Int(BottomLeftCorner.x, TopRightCorner.y);
        this.LayerIndex = index;
    }

    public int Width { get => (int)(TopRightCorner.x - BottomLeftCorner.x); }
    public int Length { get => (int)(TopRightCorner.y - BottomLeftCorner.y); }

}