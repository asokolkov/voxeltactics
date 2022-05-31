using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public string title;
    public string Damage { get; set; }
    public string Health { get; set; }
    public Vector2Int Coords { get; set; }
    public PieceStatus pieceStatus;
}