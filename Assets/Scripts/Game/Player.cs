using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PieceCreator))]
public class Player : MonoBehaviour
{
    public SideType sideType;
    public Board board;
    public Inventory inventory;
    public InteractionSpot interactionSpot;
    
    [NonSerialized] public Piece SelectedPiece;
    [NonSerialized] public List<InteractionSpot> ShownInteractionSpots;
    private PieceCreator pieceCreator;
    
    private const float Raise = 0.2f;

    private void Awake()
    {
        ShownInteractionSpots = new List<InteractionSpot>();
        pieceCreator = GetComponent<PieceCreator>();
    }
    
    public void InitializePiece((int x, int y) coords, string title)
    {
        var piece = pieceCreator.CreatePiece(title).GetComponent<Piece>();
        piece.sideType = sideType;
        board[coords.x, coords.y].Piece = piece;
        board[coords.x, coords.y].Status = CellStatus.Occupied;
        piece.transform.position = board[coords.x, coords.y].Position;
    }

    public void SelectPiece(Piece piece, Dictionary<SideType, Player> players)
    {
        SelectedPiece = piece;
        piece.transform.position += new Vector3(0, Raise, 0);
        if (piece.pieceStatus == PieceStatus.InInventory) ShowPlaceSpots();
        else ShowInteractionSpots(players);
    }
    
    public void DeselectPiece(bool drop = true)
    {
        if (drop) SelectedPiece.transform.position -= new Vector3(0, Raise, 0);
        SelectedPiece = null;
        HideInteractions();
    }
    
    public void Interact((int x, int y) coords, Board b)
    {
        SelectedPiece.Interact(coords, b);
        DeselectPiece();
    }
    
    public void SetPieceOn((int x, int y) coords)
    {
        SelectedPiece.x = coords.x;
        SelectedPiece.y = coords.y;
        SelectedPiece.transform.position = board[coords.x, coords.y].Position;
        board[coords.x, coords.y].Status = CellStatus.Occupied;
        RemoveFromInventory(SelectedPiece);
        DeselectPiece(false);
    }
    
    private void ShowPlaceSpots()
    {
        foreach (var spot in board.GetValues()
                     .Where(x => x.Status == CellStatus.Vacant))
            ShownInteractionSpots.Add(InitializeSpot(spot.Position, 
                "SpotToPlace", sideType, (spot.X, spot.Y)));
    }
    
    private void ShowInteractionSpots(Dictionary<SideType, Player> players)
    {
        foreach (var (sType, coords) in SelectedPiece.InteractionSpots)
            ShownInteractionSpots.Add(InitializeSpot(
                players[sType].board[coords.x, coords.y].Position,
                "SpotToInteract", sType, coords));
    }
    
    private InteractionSpot InitializeSpot(Vector3 position, string spotTag, 
        SideType sideType, (int x, int y) coords)
    {
        var spot = Instantiate(interactionSpot, position, Quaternion.identity);
        spot.tag = spotTag;
        spot.sideType = sideType;
        spot.x = coords.x;
        spot.y = coords.y;
        return spot;
    }
    
    private void HideInteractions()
    {
        foreach (var spot in ShownInteractionSpots) Destroy(spot.gameObject);
        ShownInteractionSpots.Clear();
    }
    
    public void MoveToInventory(Piece piece)
    {
        board[piece.x, piece.y].Status = CellStatus.Vacant;
        DeselectPiece();
        inventory.Add(piece);
    }
    
    public void RemoveFromInventory(Piece piece)
    {
        inventory.Remove(piece);
    }
    
    public void AddRandomPieceToInventory()
    {
        var maxReached = inventory.Pieces.Count == inventory.maxPiecesAmount &&
                         inventory.Pieces[^1].title == "AddButton";
        if (maxReached) inventory.DestroyAddButton();
        var piece = pieceCreator.CreateRandomPiece().GetComponent<Piece>();
        piece.pieceStatus = PieceStatus.InInventory;
        piece.sideType = sideType;
        inventory.Add(piece, maxReached);
    }
}