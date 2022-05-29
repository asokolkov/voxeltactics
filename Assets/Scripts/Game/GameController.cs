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

    public GameObject fieldGO;
    private Field field;
    
    private void Awake()
    {
        SetDependencies();
        CreatePlayers();
    }
    
    private void SetDependencies()
    {
        pieceCreator = GetComponent<PieceCreator>();
        field = fieldGO.GetComponent<Field>();
    }

    private void CreatePlayers()
    {
        Ally = new Player(field.AllyBoard);
        Enemy = new Player(field.EnemyBoard);
    }

    private void Start()
    {
        InitializePiece(new Vector2Int(0, 0), field.EnemyBoard, "Penguin");
        ActivePlayer = Ally;
        GetAllPossiblePlayerBoardInteractions(ActivePlayer);
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
