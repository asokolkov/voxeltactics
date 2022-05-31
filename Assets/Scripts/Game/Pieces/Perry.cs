using System.Collections.Generic;
using UnityEngine;

public class Perry : Piece
{
    private void Awake()
    {
        interactionCoords = new[] { InteractionCoords.AllyFront };
    }
}