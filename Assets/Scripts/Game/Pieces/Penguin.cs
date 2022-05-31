using System;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : Piece
{
    private void Awake()
    {
        interactionCoords = new List<Vector2Int>
        {
            new(2, 2), new(2, 1), new(2, 0)
        };
    }
}