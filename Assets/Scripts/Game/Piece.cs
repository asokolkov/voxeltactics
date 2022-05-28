using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public string title;
    public string Damage { get; set; }
    public string Health { get; set; }
    
    public Board Board { get; set; }
    public Vector2Int Position { get; set; }
    public bool active;
    
    public List<Vector2Int> allyInteractionPositions;
    public List<Vector2Int> enemyInteractionPositions;

    public abstract List<Vector2Int> SelectInteractionPositions();

    private void Awake()
    {
        allyInteractionPositions = new List<Vector2Int>();
        enemyInteractionPositions = new List<Vector2Int>();
        active = true;
    }

    public void SetData(Vector2Int coords, Board board)
    {
        Position = coords;
        Board = board;
        transform.position = board.CalculatePosition(coords);
    }
}