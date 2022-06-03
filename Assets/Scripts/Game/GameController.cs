using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        DisablePlayers();
        SetPlayer();
        activePlayer.InitializePiece((0, 0), "Penguin");
        activePlayer.InitializePiece((1, 0), "Penguin");
        players[SideType.Enemy].InitializePiece((0, 0), "Perry");
    }

    private void DisablePlayers()
    {
        foreach (var player in players.Values)
            TogglePlayer(player, false);
    }

    private void SetPlayer()
    {
        if (activePlayer == null)
        {
            activePlayer = players[SideType.Ally];
        }
        else
        {
            TogglePlayer(activePlayer, false);
            activePlayer = activePlayer.sideType == SideType.Ally
                ? players[SideType.Enemy]
                : players[SideType.Ally];
        }
        TogglePlayer(activePlayer, true);
    }

    private static void TogglePlayer(Player player, bool activate)
    {
        player.inventory.gameObject.SetActive(activate);
        player.textManager.gameObject.SetActive(activate);
        foreach (var piece in player.inventory.Pieces)
            piece.gameObject.SetActive(activate);
        Debug.Log(player.sideType + (activate ? " on" : " off"));
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
        CountAction();
    }

    private void OnSpotToInteractClick(GameObject go)
    {
        var spot = go.GetComponent<InteractionSpot>();
        activePlayer.Interact((spot.x, spot.y), players[spot.sideType].board);
        CountAction();
    }

    private void OnAddButtonClick()
    {
        activePlayer.AddRandomPieceToInventory();
        CountAction();
    }

    private void OnInventoryClick()
    {
        if (!activePlayer.SelectedPiece) return;
        if (activePlayer.inventory.Contains(activePlayer.SelectedPiece))
            activePlayer.DeselectPiece();
        else
            activePlayer.MoveToInventory(activePlayer.SelectedPiece);
        CountAction();
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
    
    private void CountAction()
    {
        activePlayer.actionsAmount--;
        if (activePlayer.actionsAmount <= 0)
        {
            activePlayer.actionsAmount = 2;
            SetPlayer();
        }
        activePlayer.textManager.text = activePlayer.actionsAmount + " actions";
    }
}
