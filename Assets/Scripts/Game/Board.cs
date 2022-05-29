using System;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform boardPivot;

    private Piece[,] grid;

    private const float SquareSize = 1;
    private const int BoardSize = 3;

    private void Awake()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new Piece[BoardSize, BoardSize];
    }
    
    public void SetPieceOnBoard(Vector2Int coords, Piece piece)
    {
        if (AreCoordsOnBoard(coords)) grid[coords.x, coords.y] = piece;
    }

    public Vector3 CalculatePosition(Vector2Int coords)
    {
        return boardPivot.position + new Vector3(coords.x * SquareSize, 0, 
            coords.y * SquareSize);
    }
    
    public Vector2Int CalculateCoords(Vector3 position)
    {
        var x = Mathf
            .FloorToInt(transform
                .InverseTransformPoint(position).x / SquareSize) + BoardSize / 2;
        var y = Mathf
            .FloorToInt(transform
                .InverseTransformPoint(position).z / SquareSize) + BoardSize / 2;
        return new Vector2Int(x, y);
    }
    
    public Piece GetPieceOnSquare(Vector2Int coords)
    {
        return AreCoordsOnBoard(coords) ? grid[coords.x, coords.y] : null;
    }
    
    public static bool AreCoordsOnBoard(Vector2Int coords)
    {
        return coords.x >= 0 && coords.y >= 0 && coords.x < BoardSize && coords.y < BoardSize;
    }

    public bool HasPiece(Piece piece)
    {
        for (var i = 0; i < BoardSize; i++)
        for (var j = 0; j < BoardSize; j++)
            if (grid[i, j] == piece) return true;
        return false;
    }

    public void UpdateBoardOnPieceMove(Vector2Int newCoords, Vector2Int oldCoords, 
        Piece newPiece, Piece oldPiece)
    {
        grid[oldCoords.x, oldCoords.y] = oldPiece;
        grid[newCoords.x, newCoords.y] = newPiece;
    }
}