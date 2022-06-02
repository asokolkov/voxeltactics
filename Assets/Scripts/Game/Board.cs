using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Tile[] tiles;

    private Cell[,] grid;
    
    public Cell this[int x, int y] => grid[x, y];
    
    private const int BoardSize = 3;

    private void Awake()
    {
        FormGrid();
    }

    private void FormGrid()
    {
        grid = new Cell[BoardSize, BoardSize];
        var i = 0;
        for (var y = 0; y < BoardSize; y++)
        for (var x = 0; x < BoardSize; x++)
            grid[x, y] = new Cell(x, y, tiles[i++]);
    }

    public IEnumerable<Cell> GetValues()
    {
        for (var x = 0; x < BoardSize; x++)
        for (var y = 0; y < BoardSize; y++)
            yield return grid[x, y];
    }
}