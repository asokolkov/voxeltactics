using UnityEngine;

public class Cell
{
    public CellStatus Status;
    public Tile Tile;
    public Vector3 Position;
    public Vector2Int Coords;
    
    private const float TileHeight = 1;

    public Cell(Vector2Int coords, Tile tile)
    {
        Coords = coords;
        Tile = tile;
        Status = CellStatus.Vacant;
        Position = tile.transform.position + new Vector3(0, TileHeight, 0);
    }
}