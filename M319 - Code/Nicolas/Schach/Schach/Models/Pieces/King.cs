using Schach.Models;

namespace Schach.Models.Pieces;

/// <summary>
/// King - moves one square in any direction
/// </summary>
public class King(PlayerColor color, Position position) : Piece(color, position, PieceType.King)
{
    public override bool CanMoveTo(Position target, Piece?[,] board)
    {
        // Validation: target must be on board
        if (!target.IsValid())
            return false;

        var rowDiff = Math.Abs(target.Row - Position.Row);
        var colDiff = Math.Abs(target.Column - Position.Column);

        // King can only move one square
        if (rowDiff > 1 || colDiff > 1)
            return false;

        // Cannot move to square with own piece
        var targetPiece = board[target.Row, target.Column];
        return targetPiece == null || targetPiece.Color != Color;
    }
}
