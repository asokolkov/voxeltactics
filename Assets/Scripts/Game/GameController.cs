using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Player ally;
    public Player enemy;
    private Player activePlayer;

    private void Start()
    {
        activePlayer = ally;
        activePlayer.InitializePiece(new Vector2Int(0, 0), "Penguin");
        activePlayer.InitializePiece(new Vector2Int(1, 0), "Penguin");
        enemy.InitializePiece(new Vector2Int(1, 0), "Perry");
    }

    public void OnClick(Vector3 inputPosition, GameObject go)
    {
        if (go.CompareTag("Character")) OnCharacterClick(go);
        else if (go.CompareTag("Inventory")) OnInventoryClick();
        else if (go.CompareTag("AddButton")) OnAddButtonClick();
        else if (go.CompareTag("InteractionSpot")) OnInteractionSpotClick();
    }

    private void OnInteractionSpotClick()
    {
        if (!activePlayer.SelectedPiece) return;
        // var tile = go.GetComponent<Tile>();
        // if (tile.occupied) return;
        // var inInventory = activePlayer.inventory.Contains(activePlayer.SelectedPiece);
        // if (inInventory) activePlayer.RemoveFromInventory(activePlayer.SelectedPiece);
        // activePlayer.Move(activePlayer.SelectedPiece, tile, inInventory);
        // activePlayer.DeselectPiece(false);
    }

    private void OnAddButtonClick()
    {
        activePlayer.AddRandomPieceToInventory();
    }

    private void OnInventoryClick()
    {
        if (!activePlayer.SelectedPiece) return;
        if (activePlayer.inventory.Contains(activePlayer.SelectedPiece))
            activePlayer.DeselectPiece();
        else
            activePlayer.MoveToInventory(activePlayer.SelectedPiece);
    }

    private void OnCharacterClick(GameObject go)
    {
        var piece = go.GetComponent<Piece>();
        if (activePlayer.SelectedPiece)
        {
            if (activePlayer.SelectedPiece == piece) 
                activePlayer.DeselectPiece();
            else if (activePlayer.SelectedPiece != piece)
            {
                activePlayer.DeselectPiece();
                activePlayer.SelectPiece(piece, 
                    piece.pieceStatus != PieceStatus.InInventory);
            }
        }
        else 
            activePlayer.SelectPiece(piece, 
                piece.pieceStatus != PieceStatus.InInventory);
    }
}
