using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PieceCreator))]
public class GameController : MonoBehaviour
{
    private PieceCreator pieceCreator;

    public Player Ally;
    public Player Enemy;
    public Player ActivePlayer;

    public GameObject allyBoardGO;
    public GameObject enemyBoardGO;
    
    [NonSerialized] public Board AllyBoard;
    [NonSerialized] public Board EnemyBoard;
    
    [NonSerialized] public Piece SelectedPiece;

    private void Awake()
    {
        SetDependencies();
        Ally = new Player(AllyBoard);
        Enemy = new Player(EnemyBoard);
    }
    
    private void Start()
    {
        ActivePlayer = Ally;
        InitializePiece(new Vector2Int(0, 0), EnemyBoard, "Penguin");
        GetAllPossiblePlayerBoardInteractions(ActivePlayer);
    }
    
    private void SetDependencies()
    {
        pieceCreator = GetComponent<PieceCreator>();
        AllyBoard = allyBoardGO.GetComponent<Board>();
        EnemyBoard = enemyBoardGO.GetComponent<Board>();
    }

    public void OnClick(Vector3 inputPosition, GameObject go)
    {
        if (go.CompareTag("Character")) OnCharacterClick(go);
        else if (go.CompareTag("Board")) OnBoardClick(inputPosition);
    }

    private void OnBoardClick(Vector3 position)
    {
        if (!SelectedPiece) return;
        var coords = SelectedPiece.Board.CalculateCoords(position);
        MovePiece(coords, SelectedPiece);
    }

    private void OnCharacterClick(GameObject go)
    {
        var piece = go.GetComponent<Piece>();
        if (SelectedPiece)
        {
            if (SelectedPiece == piece) 
                DeselectPiece();
            else if (SelectedPiece != piece)
                SelectPiece(piece);
        }
        else 
            SelectPiece(piece);
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

    private void GetAllPossiblePlayerBoardInteractions(Player player)
    {
        player.GetAllPossibleInteractions();
    }

    private void InitializePiece(Vector2Int squareCoords, Board board, string title)
    {
        var piece = pieceCreator.CreatePiece(title).GetComponent<Piece>();
        piece.SetData(squareCoords, board);
        
        board.SetPieceOnBoard(squareCoords, piece);
    }
}
