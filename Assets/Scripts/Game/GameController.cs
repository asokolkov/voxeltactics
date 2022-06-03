using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Player[] unityPlayers;
    private Player _activePlayer;

    private Dictionary<SideType, Player> _players;

    private void Start()
    {
        _players = new Dictionary<SideType, Player>();
        FormPlayers();
        DisablePlayers();
        SetPlayer();
        _activePlayer.InitializePiece((0, 0), "Penguin");
        _activePlayer.InitializePiece((1, 0), "Penguin");
        _players[SideType.Enemy].InitializePiece((0, 0), "Perry");
    }

    private void DisablePlayers()
    {
        foreach (var player in _players.Values)
            TogglePlayer(player, false);
    }

    private void SetPlayer()
    {
        if (_activePlayer == null)
        {
            _activePlayer = _players[SideType.Ally];
        }
        else
        {
            TogglePlayer(_activePlayer, false);
            _activePlayer = _activePlayer.sideType == SideType.Ally
                ? _players[SideType.Enemy]
                : _players[SideType.Ally];
        }
        TogglePlayer(_activePlayer, true);
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
        foreach (var player in unityPlayers) _players.Add(player.sideType, player);
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
        if (spot.coords.Count != 1) throw new ArgumentException("Spot not found");
        _activePlayer.SetPieceOn(spot.coords.First());
        CountAction();
    }

    private void OnSpotToInteractClick(GameObject go)
    {
        var spot = go.GetComponent<InteractionSpot>();
        //_activePlayer.Interact(spot.coords, _players[spot.sideType].board);
        CountAction();
    }

    private void OnAddButtonClick()
    {
        _activePlayer.AddRandomPieceToInventory();
        CountAction();
    }

    private void OnInventoryClick()
    {
        if (!_activePlayer.SelectedPiece) return;
        if (_activePlayer.inventory.Contains(_activePlayer.SelectedPiece))
            _activePlayer.DeselectPiece();
        else
            _activePlayer.MoveToInventory(_activePlayer.SelectedPiece);
        CountAction();
    }

    private void OnCharacterClick(GameObject go)
    {
        var piece = go.GetComponent<Piece>();
        if (piece.sideType == _activePlayer.sideType)
        {
            if (_activePlayer.SelectedPiece)
            {
                if (_activePlayer.SelectedPiece == piece)
                    _activePlayer.DeselectPiece();
                else if (_activePlayer.SelectedPiece != piece)
                {
                    _activePlayer.DeselectPiece();
                    _activePlayer.SelectPiece(piece, _players);
                }
            }
            else
                _activePlayer.SelectPiece(piece, _players);
        }
        else
        {
            Debug.Log("It's enemy piece");
        }
    }
    
    private void CountAction()
    {
        _activePlayer.actionsAmount--;
        if (_activePlayer.actionsAmount <= 0)
        {
            _activePlayer.actionsAmount = 2;
            SetPlayer();
        }
        _activePlayer.textManager.text = _activePlayer.actionsAmount + " actions";
    }
}
