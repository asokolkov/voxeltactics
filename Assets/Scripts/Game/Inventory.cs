using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [NonSerialized] public List<Piece> Pieces;
    public Transform inventoryCollider;
    public Piece addButton;
    
    public int maxPiecesAmount;
    private const double ElementWidth = 1;
    private const double Space = 0.2;

    private void Awake()
    {
        Pieces = new List<Piece>();
        InitializeAddButton();
    }

    private void InitializeAddButton(bool center = true)
    {
        Add(Instantiate(addButton), true, center);
    }

    public void DestroyAddButton()
    {
        Destroy(Pieces[^1].gameObject);
        Pieces.Remove(Pieces[^1]);
    }

    public void Add(Piece piece, bool toEnd = false, bool center = true)
    {
        piece.pieceStatus = PieceStatus.InInventory;
        var piecesAmount = Pieces.Count;
        if (piecesAmount >= maxPiecesAmount) return;
        if (toEnd) Pieces.Add(piece);
        else Pieces.Insert(piecesAmount - 1, piece);
        piece.transform.position = inventoryCollider.position;
        piece.transform.rotation = Quaternion.Euler(85, 0, 0);
        if (center) CenterHorizontally();
    }

    private void CenterHorizontally()
    {
        var piecesAmount = Pieces.Count;
        var totalWidth = (float) (ElementWidth + Space) * piecesAmount;
        var step = totalWidth / piecesAmount;

        for (var i = 0; i < piecesAmount; i++)
        {
            var position = Pieces[i].transform.position;
            Pieces[i].transform.position = new Vector3(
                step * (i + 0.5f) - totalWidth / 2 + inventoryCollider.position.x, 
                position.y, position.z);
        }
    }
    
    private static void AlignVertically(Piece piece)
    {
        var height = piece.GetComponent<Collider>().bounds.size.y;
        piece.transform.position -= new Vector3(0, 0, height / 2);
    }

    public bool Contains(Piece piece)
    {
        return Pieces.Contains(piece);
    }

    public void Remove(Piece piece)
    {
        piece.pieceStatus = PieceStatus.OnBoard;
        Pieces.Remove(piece);
        piece.transform.rotation = Quaternion.identity;
        if (Pieces[^1].title != "AddButton") InitializeAddButton(false);
        CenterHorizontally();
    }
}