using System;
using System.Collections.Generic;
using UnityEngine;

public class PieceCreator : MonoBehaviour
{ 
    public GameObject[] piecesPrefabs;
    private readonly Dictionary<string, GameObject> titleToPieceDictionary = new();

    private void Awake()
    {
        foreach (var piece in piecesPrefabs)
        {
            titleToPieceDictionary.Add(piece.GetComponent<Piece>().title, piece);
        }
    }

    public GameObject CreatePiece(string title)
    {
        var prefab = titleToPieceDictionary[title];
        return prefab ? Instantiate(prefab) : null;
    }
}