using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileType tileType;
    [NonSerialized] public Piece Piece;
}