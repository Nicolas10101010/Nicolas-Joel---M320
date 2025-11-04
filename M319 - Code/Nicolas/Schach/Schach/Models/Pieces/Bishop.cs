using Schach.Models;

namespace Schach.Models.Pieces;

/// <summary>
/// Bishop - moves any distance diagonally
/// </summary>
public class Bishop(PlayerColor color, Position position) : Piece(color, position, PieceType.Bishop)
{
    public override bool CanMoveTo(Position target, Piece?[,] board)
    {
        if (!target.IsValid())
            return false;

        var rowDiff = Math.Abs(target.Row - Position.Row);
        var colDiff = Math.Abs(target.Column - Position.Column);

        // Bishop can only move diagonally
        if (rowDiff != colDiff || rowDiff == 0)
            return false;

        if (!IsPathClear(Position, target, board))
            return false;

        var targetPiece = board[target.Row, target.Column];
        return targetPiece == null || targetPiece.Color != Color;
    }
}
