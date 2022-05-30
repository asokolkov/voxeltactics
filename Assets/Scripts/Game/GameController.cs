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
    }

    public void OnClick(Vector3 inputPosition, GameObject go)
    {
        if (go.CompareTag("Character")) OnCharacterClick(go);
        else if (go.CompareTag("Tile")) OnTileClick(go);
        else if (go.CompareTag("Inventory")) OnInventoryClick();
    }
    
    private void OnInventoryClick()
    {
        // if (!activePlayer.selectedPiece) return;
        // activePlayer.MoveToInventory(selectedPiece);
    }
    
    private void OnTileClick(GameObject go)
    {
        if (!activePlayer.SelectedPiece) return;
        var tile = go.GetComponent<Tile>();
        activePlayer.Move(activePlayer.SelectedPiece, tile.Coords);
        activePlayer.DeselectPiece(false);
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
                activePlayer.SelectPiece(piece);
            }
        }
        else 
            activePlayer.SelectPiece(piece);
    }
}
