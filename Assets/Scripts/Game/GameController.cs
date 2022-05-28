using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PieceCreator))]
public class GameController : MonoBehaviour
{
    private PieceCreator pieceCreator;
    public Board allyBoard;
    public Board enemyBoard;


    private void Awake()
    {
        SetDependencies();
    }

    private void SetDependencies()
    {
        pieceCreator = GetComponent<PieceCreator>();
    }

    private void Start()
    {
        InitializePiece(new Vector2Int(0, 0), enemyBoard, "Penguin");
    }
    
    private void InitializePiece(Vector2Int squareCoords, Board board, string title)
    {
        var piece = pieceCreator.CreatePiece(title).GetComponent<Piece>();
        piece.SetData(squareCoords, board);
    }
}
