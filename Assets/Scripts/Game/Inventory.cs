using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Piece> pieces;
    
    private const int MaxPiecesAmount = 4;
    private const double ElementWidth = 1;
    private const double Space = 0.2;

    private void Awake()
    {
        pieces = new List<Piece>();
    }

    public void Add(Piece piece)
    {
        var piecesAmount = pieces.Count;
        if (piecesAmount >= MaxPiecesAmount) return;
        pieces.Add(piece);
        piece.transform.position = transform.position;
        piece.transform.rotation = Quaternion.Euler(85, 0, 0);
        CenterHorizontally();
    }
    
    private void CenterHorizontally()
    {
        var piecesAmount = pieces.Count;
        var totalWidth = (float) (ElementWidth + Space) * piecesAmount;
        var step = totalWidth / piecesAmount;

        for (var i = 0; i < piecesAmount; i++)
        {
            var position = pieces[i].transform.position;
            pieces[i].transform.position = new Vector3(
                step * (i + 0.5f) - totalWidth / 2 + transform.position.x, 
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
        return pieces.Contains(piece);
    }

    public void Remove(Piece piece)
    {
        pieces.Remove(piece);
        piece.transform.rotation = Quaternion.identity;
        CenterHorizontally();
    }
}