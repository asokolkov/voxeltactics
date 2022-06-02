using UnityEngine;

public class Cell
{
    public Piece Piece;
    public CellStatus Status;
    public Tile Tile;
    public Vector3 Position;
    public int X;
    public int Y;
    
    private const float TileHeight = 1;

    public Cell(int x, int y, Tile tile)
    {
        X = x;
        Y = y;
        Tile = tile;
        Status = CellStatus.Vacant;
        Position = tile.transform.position + new Vector3(0, TileHeight, 0);
    }
}