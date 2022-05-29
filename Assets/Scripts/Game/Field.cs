using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class Field : MonoBehaviour
{
    public GameObject allyBoardGO;
    public GameObject enemyBoardGO;
    
    [NonSerialized] public Board AllyBoard;
    [NonSerialized] public Board EnemyBoard;
    
    [NonSerialized] public Piece SelectedPiece;
    
    private const float SquareSize = 1;
    private const int BoardSize = 3;

    private void Awake()
    {
        SetDependencies();
    }

    private void SetDependencies()
    {
        AllyBoard = allyBoardGO.GetComponent<Board>();
        EnemyBoard = enemyBoardGO.GetComponent<Board>();
    }

    public void OnSquareSelected(Vector3 inputPosition, GameObject selectedObject)
    {
        var board = selectedObject.GetComponent<Board>();
        var coords = board.CalculateCoords(inputPosition);
        var piece = board.GetPieceOnSquare(coords);
        if (SelectedPiece)
        {
            if (piece != null && SelectedPiece == piece) 
                DeselectPiece();
            else if (piece != null && SelectedPiece != piece)
                SelectPiece(piece);
            else
                MovePiece(coords, SelectedPiece);
        }
        else if (piece != null) SelectPiece(piece);
    }

    private void MovePiece(Vector2Int coords, Piece piece)
    {
        piece.Board.UpdateBoardOnPieceMove(coords, piece.Position, 
            piece, null);
        SelectedPiece.MovePiece(coords);
        DeselectPiece();
    }
    
    private void SelectPiece(Piece piece)
    {
        SelectedPiece = piece;
        piece.transform.position += new Vector3(0, 0.1f, 0);
    }
    
    private void DeselectPiece()
    {
        SelectedPiece = null;
    }
}