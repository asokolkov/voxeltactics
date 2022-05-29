using System;
using JetBrains.Annotations;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Tile[] tilesArray;
    
    [NonSerialized] public Tile[,] Tiles;

    private const int BoardSize = 3;
    private const float TileHeight = 1;
    
    private void Awake()
    {
        Tiles = new Tile[BoardSize, BoardSize];
        
        var c = 0;
        for (var i = 0; i < BoardSize; i++)
        for (var j = 0; j < BoardSize; j++)
        {
            Tiles[i, j] = tilesArray[c];
            c++;
        }
    }

    public Vector3 GetPositionFromCoords(Vector2Int coords)
    {
        return Tiles[coords.x, coords.y].transform.position + 
               new Vector3(0, TileHeight, 0);
    }

    public Vector2Int GetTileCoords(Tile tile)
    {
        for (var i = 0; i < BoardSize; i++)
        for (var j = 0; j < BoardSize; j++)
            if (Tiles[i, j] == tile)
                return new Vector2Int(i, j);
        throw new ArgumentOutOfRangeException("Tile not found");
    }

    [CanBeNull]
    public Tile GetTileWithPiece(Piece piece)
    {
        for (var i = 0; i < BoardSize; i++)
        for (var j = 0; j < BoardSize; j++)
            if (Tiles[i, j].Piece == piece)
                return Tiles[i, j];
        return null;
    }
}