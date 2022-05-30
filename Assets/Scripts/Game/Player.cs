using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(PieceCreator))]
public class Player : MonoBehaviour
{
    private PieceCreator pieceCreator;
    
    public Board board;
    [NonSerialized] public Piece SelectedPiece;
    [NonSerialized] public List<Piece> Pieces;
    
    private const float Raise = 0.2f;

    private void Awake()
    {
        pieceCreator = GetComponent<PieceCreator>();
        Pieces = new List<Piece>();
    }

    public void SelectPiece(Piece piece)
    {
        SelectedPiece = piece;
        piece.transform.position += new Vector3(0, Raise, 0);
    }

    public void DeselectPiece(bool drop = true)
    {
        if (drop) SelectedPiece.transform.position -= new Vector3(0, Raise, 0);
        SelectedPiece = null;
    }

    public void InitializePiece(Vector2Int coords, string title)
    {
        var piece = pieceCreator.CreatePiece(title).GetComponent<Piece>();
        Pieces.Add(piece);
        Move(piece, coords);
    }
    
    public void Move(Piece piece, Vector2Int coords)
    {
        piece.Coords = coords;
        piece.transform.position = board.GetPiecePosition(coords);
    }
}