﻿using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public string title;
    public int damage;
    public int health;
    public int x;
    public int y;
    public PieceStatus pieceStatus;
    public SideType sideType;
    public HashSet<(SideType, (int x, int y))> InteractionSpots;

    public void Interact((int x, int y) coords, Board board)
    {
        var piece = board[coords.x, coords.y].Piece;
        if (piece)
        {
            piece.health -= damage;
        }
    }
}