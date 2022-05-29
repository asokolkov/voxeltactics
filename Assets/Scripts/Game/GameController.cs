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

    public Board allyBoard;

    [NonSerialized] public Piece SelectedPiece;

    private const float Raise = 0.2f;

    private void Awake()
    {
        SetDependencies();
        Ally = new Player();
        Enemy = new Player();
    }
    
    private void Start()
    {
        ActivePlayer = Ally;
        InitializePiece(new Vector2Int(0, 0), allyBoard, "Penguin");
        InitializePiece(new Vector2Int(0, 1), allyBoard, "Penguin");
    }
    
    private void SetDependencies()
    {
        pieceCreator = GetComponent<PieceCreator>();
    }
    
    public void OnClick(Vector3 inputPosition, GameObject go)
    {
        if (go.CompareTag("Character")) OnCharacterClick(go);
        else if (go.CompareTag("Tile")) OnTileClick(go);
    }

    private void OnVoidClick()
    {
        DeselectPiece();
    }

    private void OnTileClick(GameObject go)
    {
        if (!SelectedPiece) return;
        var tile = go.GetComponent<Tile>();
        if (tile.Piece == null) MovePieceOnTile(tile);
    }
    
    private void OnCharacterClick(GameObject go)
    {
        var piece = go.GetComponent<Piece>();
        if (SelectedPiece)
        {
            if (SelectedPiece == piece) 
                DeselectPiece();
            else if (SelectedPiece != piece)
            {
                DeselectPiece();
                SelectPiece(piece);
            }
        }
        else 
            SelectPiece(piece);
    }
    
    private void MovePieceOnTile(Tile tile)
    {
        var tileCoords = SelectedPiece.Board.GetTileCoords(tile);
        SelectedPiece.MovePieceTo(tileCoords);
        DeselectPiece(false);
    }
    
    private void SelectPiece(Piece piece)
    {
        SelectedPiece = piece;
        piece.transform.position += new Vector3(0, Raise, 0);
    }
    
    private void DeselectPiece(bool drop = true)
    {
        if (drop) SelectedPiece.transform.position -= new Vector3(0, Raise, 0);
        SelectedPiece = null;
    }
    
    private void InitializePiece(Vector2Int squareCoords, Board board, string title)
    {
        var piece = pieceCreator.CreatePiece(title).GetComponent<Piece>();
        piece.FillData(squareCoords, board);
    }
}
