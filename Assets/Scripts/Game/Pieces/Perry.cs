using System.Collections.Generic;
using UnityEngine;

public class Perry : Piece
{
    private void Awake()
    {
        InteractionSpots = new HashSet<(SideType, (int x, int y))>
        {
            (SideType.Ally, (1, 1)),
            (SideType.Enemy, (0, 1)),
        };
    }
}