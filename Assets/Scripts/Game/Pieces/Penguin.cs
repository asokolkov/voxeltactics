using System;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : Piece
{
    private void Awake()
    {
        interactionCoords = new[] { InteractionCoords.AllyBack };
    }
}