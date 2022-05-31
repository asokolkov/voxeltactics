using System.Collections.Generic;
using UnityEngine;

public class Perry : Piece
{
    private void Awake()
    {
        interactionCoords = new List<Vector2Int>
        {
            new(0, 2), new(0, 2), new(0, 2)
        };
    }
}