using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Player[] unityPlayers;
    private Player activePlayer;

    private Dictionary<SideType, Player> players;

    private void Start()
    {
        players = new Dictionary<SideType, Player>();
        FormPlayers();
        activePlayer = players[SideType.Ally];
        activePlayer.InitializePiece((0, 0), "Penguin");
        activePlayer.InitializePiece((1, 0), "Penguin");
        players[SideType.Enemy].InitializePiece((0, 0), "Perry");
    }

    private void FormPlayers()
    {
        foreach (var player in unityPlayers) players.Add(player.sideType, player);
    }

    public void OnClick(GameObject go)
    {
        if (go.CompareTag("Character")) OnCharacterClick(go);
        else if (go.CompareTag("Inventory")) OnInventoryClick();
        else if (go.CompareTag("AddButton")) OnAddButtonClick();
        else if (go.CompareTag("SpotToInteract")) OnSpotToInteractClick(go);
        else if (go.CompareTag("SpotToPlace")) SpotToPlaceClick(go);
    }

    private void SpotToPlaceClick(GameObject go)
    {
        var spot = go.GetComponent<InteractionSpot>();
        activePlayer.SetPieceOn((spot.x, spot.y));
    }

    private void OnSpotToInteractClick(GameObject go)
    {
        var spot = go.GetComponent<InteractionSpot>();
        activePlayer.Interact((spot.x, spot.y), players[spot.sideType].board);
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
        if (piece.sideType == activePlayer.sideType)
        {
            if (activePlayer.SelectedPiece)
            {
                if (activePlayer.SelectedPiece == piece)
                    activePlayer.DeselectPiece();
                else if (activePlayer.SelectedPiece != piece)
                {
                    activePlayer.DeselectPiece();
                    activePlayer.SelectPiece(piece, players);
                }
            }
            else
                activePlayer.SelectPiece(piece, players);
        }
        else
        {
            Debug.Log("It's enemy piece");
        }
    }
}
