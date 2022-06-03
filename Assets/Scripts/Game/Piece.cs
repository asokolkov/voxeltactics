using System;
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
    public HashSet<(SideType, InteractionType)> InteractionSpots;

    public void Interact(List<Vector2Int> coords, Board board)
    {
        // var piece = board[coords.x, coords.y].Piece;
        // if (piece) piece.health -= damage;
        // if (piece.health <= 0) 
        //     Debug.Log($"{piece.sideType} lost!");
        throw new NotImplementedException();
    }

    public abstract void InteractFront((int x, int y) coords, Board board);
    public abstract void InteractCenter((int x, int y) coords, Board board);
    public abstract void InteractBack((int x, int y) coords, Board board);
}