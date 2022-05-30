using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Tile[] tiles;

    private const int BoardSize = 3;
    private const float TileHeight = 1;
    
    private void Awake()
    {
        NumerateTiles();
    }

    private void NumerateTiles()
    {
        var row = 0;
        var col = 0;
        foreach (var tile in tiles)
        {
            tile.Coords = new Vector2Int(row, col);
            row++;
            if (row >= BoardSize - 1)
            {
                col++;
                row = 0;
            }
        }
    }

    public Tile GetTile(Vector2Int coords)
    {
        var foundTiles = tiles
            .Select(x => x)
            .Where(x => x.Coords == coords)
            .ToList();
        if (foundTiles.Count != 1) throw new ArgumentException("Tile not found"); 
        return foundTiles.First();
    }

    public Vector3 OccupyTileOn(Vector2Int coords)
    {
        var tile = GetTile(coords);
        tile.occupied = true;
        return tile.transform.position + new Vector3(0, TileHeight, 0);
    }

    public void ReleaseTileOn(Vector2Int coords)
    {
        var tile = GetTile(coords);
        tile.occupied = false;
    }

    public Vector3 Occupy(Tile tile)
    {
        tile.occupied = true;
        return tile.transform.position + new Vector3(0, TileHeight, 0);
    }
}