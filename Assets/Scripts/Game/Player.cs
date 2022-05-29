using System.Collections.Generic;

public class Player
{
    public Board Board { get; set; }
    public List<Piece> AlivePieces { get; set; }
    public List<Piece> DeadPieces { get; set; }

    public Player(Board board)
    {
        Board = board;
        AlivePieces = new List<Piece>();
        DeadPieces = new List<Piece>();
    }

    public void AddAlivePiece(Piece piece)
    {
        if (!AlivePieces.Contains(piece)) AlivePieces.Add(piece);
    }
    
    public void AddDeadPiece(Piece piece)
    {
        if (!DeadPieces.Contains(piece)) DeadPieces.Add(piece);
    }
    
    public void RemoveAlivePiece(Piece piece)
    {
        if (AlivePieces.Contains(piece)) AlivePieces.Remove(piece);
    }
    
    public void RemoveDeadPiece(Piece piece)
    {
        if (!DeadPieces.Contains(piece)) DeadPieces.Remove(piece);
    }

    public void GetAllPossibleInteractions()
    {
        foreach (var piece in AlivePieces)
        {
            if (Board.HasPiece(piece))
            {
                piece.SelectInteractionPositions();
            }
        }
    }
}