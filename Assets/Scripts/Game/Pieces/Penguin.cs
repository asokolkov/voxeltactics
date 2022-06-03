using System;
using System.Collections.Generic;
using UnityEngine;

public class Penguin : Piece
{
    private void Awake()
    {
        InteractionSpots = new HashSet<(SideType, InteractionType)>
        {
            (SideType.Ally, InteractionType.Left)
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