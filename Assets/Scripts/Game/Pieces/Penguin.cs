using System;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : Piece
{
    private void Awake()
    {
        InteractionSpots = new HashSet<(SideType, (int x, int y))>
        {
            (SideType.Ally, (0, 0)),
            (SideType.Ally, (0, 1)),
            (SideType.Enemy, (0, 0)),
            (SideType.Ally, (2, 2))
        };
    }

    public override void InteractFront((int x, int y) coords, Board board)
    {
        throw new NotImplementedException();
    }

    public override void InteractCenter((int x, int y) coords, Board board)
    {
        throw new NotImplementedException();
    }

    public override void InteractBack((int x, int y) coords, Board board)
    {
        throw new NotImplementedException();
    }
}