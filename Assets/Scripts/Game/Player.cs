using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(PieceCreator))]
public class Player : MonoBehaviour
{
    public SideType sideType;
    public Board board;
    public Inventory inventory;
    public TMP_Text textManager;
    public int actionsAmount;
    
    public InteractionSpot singleSpot;
    public InteractionSpot fullSpot;
    public InteractionSpot rowSpot;
    
    [NonSerialized] public Piece SelectedPiece;
    [NonSerialized] public List<InteractionSpot> ShownInteractionSpots;
    private PieceCreator _pieceCreator;

    private const float Raise = 0.2f;
    private const int BoardSize = 3;

    private void Awake()
    {
        ShownInteractionSpots = new List<InteractionSpot>();
        _pieceCreator = GetComponent<PieceCreator>();
    }
    
    public void InitializePiece((int x, int y) coords, string title)
    {
        var piece = _pieceCreator.CreatePiece(title).GetComponent<Piece>();
        piece.sideType = sideType;
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
    
    public void Interact(List<Vector2Int> coords, Board b)
    {
        SelectedPiece.Interact(coords, b);
        textManager.text = actionsAmount + " actions";
        DeselectPiece();
    }
    
    public void SetPieceOn(Vector2Int coords)
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
        foreach (var cell in board.GetValues().Where(x => x.Status == CellStatus.Vacant))
            ShownInteractionSpots.Add(InitializeSpot(singleSpot, cell.Position, 
                "SpotToPlace", sideType, new List<Vector2Int> { cell.Coords}));
    }
    
    private void ShowInteractionSpots(Dictionary<SideType, Player> players)
    {
        var interactionConverter = new Dictionary<InteractionType, List<Vector2Int>>
        {
            [InteractionType.Back] =   new() { new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(0, 2) },
            [InteractionType.Center] = new() { new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(1, 2) },
            [InteractionType.Front] =  new() { new Vector2Int(2, 0), new Vector2Int(2, 1), new Vector2Int(2, 2) },
            [InteractionType.Left] =   new() { new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(2, 0) },
            [InteractionType.Middle] = new() { new Vector2Int(0, 1), new Vector2Int(1, 1), new Vector2Int(2, 1) },
            [InteractionType.Right] =  new() { new Vector2Int(0, 2), new Vector2Int(1, 2), new Vector2Int(2, 2) },
        };
        foreach (var (side, interaction) in SelectedPiece.InteractionSpots)
        {
            if (interaction == InteractionType.AnySingle)
                foreach (var cell in players[side].board.GetValues())
                    ShownInteractionSpots.Add(InitializeSpot(singleSpot, cell.Position,
                        "SpotToInteract", side, new List<Vector2Int> { cell.Coords }));
            
            else if (interaction == InteractionType.Full)
                ShownInteractionSpots.Add(InitializeSpot(fullSpot, players[side].board[1, 1].Position,
                    "SpotToInteract", side, new List<Vector2Int>
                    {
                        new(0, 2), new(1, 2), new(2, 2),
                        new(0, 1), new(1, 1), new(2, 1),
                        new(0, 0), new(1, 0), new(2, 0)
                    }));
            
            else if (interaction == InteractionType.AllRows)
            {
                for (var x = 0; x < BoardSize; x++)
                    ShownInteractionSpots.Add(InitializeRowSpot(players[side].board[x, 1].Position, 
                        "SpotToInteract", side, true,
                        new List<Vector2Int> { new(x, 0), new(x, 1), new(x, 2) }));
            }
            
            else if (interaction == InteractionType.AllColumns)
            {
                for (var y = 0; y < BoardSize; y++)
                    ShownInteractionSpots.Add(InitializeRowSpot(players[side].board[1, y].Position, 
                        "SpotToInteract", side, false, 
                        new List<Vector2Int> { new(0, y), new(1, y), new(2, y) }));
            }
            
            else
            {
                var rotate = interaction is InteractionType.Back or InteractionType.Center or InteractionType.Front;
                var t = interactionConverter[interaction];
                ShownInteractionSpots.Add(InitializeRowSpot(players[side].board[t[1].x, t[1].y].Position, 
                    "SpotToInteract", side, rotate, t));
            }
            
        }
    }
    
    private InteractionSpot InitializeRowSpot(Vector3 position, string spotTag,
        SideType side, bool rotate, List<Vector2Int> coords)
    {
        var spot = Instantiate(rowSpot, position, Quaternion.identity);
        if (rotate) spot.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        spot.tag = spotTag;
        spot.sideType = side;
        spot.coords = coords;
        return spot;
    }
    
    private InteractionSpot InitializeSpot(InteractionSpot it, Vector3 position, string spotTag, 
        SideType side, List<Vector2Int> coords)
    {
        var spot = Instantiate(it, position, Quaternion.identity);
        spot.tag = spotTag;
        spot.sideType = side;
        spot.coords = coords;
        return spot;
    }

    private void ChangeSpotColor(InteractionSpot spot, (int x, int y) coords)
    {
        var spotRenderer = spot.gameObject.GetComponent<Renderer>();
        if (coords.x == 0) spotRenderer.material.color = Color.cyan;
        else if (coords.x == 1) spotRenderer.material.color = Color.green;
        else if (coords.x == 2) spotRenderer.material.color = Color.red;
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
        var piece = _pieceCreator.CreateRandomPiece().GetComponent<Piece>();
        piece.pieceStatus = PieceStatus.InInventory;
        piece.sideType = sideType;
        inventory.Add(piece, maxReached);
    }
}