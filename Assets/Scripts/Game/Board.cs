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
        var x = 0;
        var y = 0;
        foreach (var tile in tiles)
        {
            tile.coords = new Vector2Int(y, x);
            if (y >= BoardSize - 1)
            {
                x++;
                y = 0;
            }
            else y++;
            
        }
    }

    public Tile GetTile(Vector2Int coords)
    {
        var foundTiles = tiles
            .Select(x => x)
            .Where(x => x.coords == coords)
            .ToList();
        if (foundTiles.Count != 1) throw new ArgumentException("Tile not found"); 
        return foundTiles.First();
    }

    public Vector3 GetSpotPosition(Tile tile)
    {
        return tile.transform.position + new Vector3(0, TileHeight, 0);
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