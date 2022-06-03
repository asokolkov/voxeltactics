using System;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Tile[] tiles;
    
    private Cell[,] _grid;
    
    public Cell this[int x, int y] => _grid[x, y];

    private const int BoardSize = 3;

    private void Awake()
    {
        FormGrid();
    }

    private void FormGrid()
    {
        _grid = new Cell[BoardSize, BoardSize];
        var i = 0;
        for (var y = 0; y < BoardSize; y++)
        for (var x = 0; x < BoardSize; x++)
            _grid[x, y] = new Cell(new Vector2Int(x, y), tiles[i++]);
    }

    public IEnumerable<Cell> GetValues()
    {
        for (var x = 0; x < BoardSize; x++)
        for (var y = 0; y < BoardSize; y++)
            yield return _grid[x, y];
    }
}