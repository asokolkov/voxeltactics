using System.Collections.Generic;
using UnityEngine;

public class Perry : Piece
{
    private void Awake()
    {
        InteractionSpots = new HashSet<(SideType, InteractionType)>
        {
            (SideType.Ally, InteractionType.AllColumns)
        };
    }

    public override void InteractFront((int x, int y) coords, Board board)
    {
        throw new System.NotImplementedException();
    }

    public override void InteractCenter((int x, int y) coords, Board board)
    {
        throw new System.NotImplementedException();
    }

    public override void InteractBack((int x, int y) coords, Board board)
    {
        throw new System.NotImplementedException();
    }
}