using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class PieceCreator : MonoBehaviour
{ 
    public GameObject[] piecesPrefabs;
    private readonly Dictionary<string, GameObject> _piecesDict = new();
    private static readonly Random Random = new();

    private void Awake()
    {
        foreach (var piece in piecesPrefabs)
        {
            _piecesDict.Add(piece.GetComponent<Piece>().title, piece);
        }
    }

    public GameObject CreatePiece(string title)
    {
        var prefab = _piecesDict[title];
        return prefab ? Instantiate(prefab) : null;
    }

    private string GetRandomPieceTitle()
    {
        var values = _piecesDict.Keys.ToList();
        return values[Random.Next(_piecesDict.Count)];
    }

    public GameObject CreateRandomPiece()
    {
        return CreatePiece(GetRandomPieceTitle());
    }
}