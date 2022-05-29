using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public string title;
    public string Damage { get; set; }
    public string Health { get; set; }
    
    public Board Board { get; set; }
    public Vector2Int Position { get; set; }
    
    public List<Vector2Int> allyInteractionPositions;
    public List<Vector2Int> enemyInteractionPositions;

    public abstract List<Vector2Int> SelectInteractionPositions();

    private void Awake()
    {
        allyInteractionPositions = new List<Vector2Int>();
        enemyInteractionPositions = new List<Vector2Int>();
    }

    public void FillData(Vector2Int coords, Board board)
    {
        Position = coords;
        Board = board;
        MovePieceTo(coords);
    }
    
    public void MovePieceTo(Vector2Int coords)
    {
        var tile = Board.GetTileWithPiece(this);
        if (tile != null) tile.Piece = null;
        transform.position = Board.GetPositionFromCoords(coords);
        Board.Tiles[coords.x, coords.y].Piece = this;
    }
}