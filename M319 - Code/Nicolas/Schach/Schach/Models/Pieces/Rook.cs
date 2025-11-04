using Schach.Models;

namespace Schach.Models.Pieces;

/// <summary>
/// Rook - moves any distance horizontally or vertically
/// </summary>
public class Rook(PlayerColor color, Position position) : Piece(color, position, PieceType.Rook)
{
    public override bool CanMoveTo(Position target, Piece?[,] board)
    {
        if (!target.IsValid())
            return false;

        var rowDiff = Math.Abs(target.Row - Position.Row);
        var colDiff = Math.Abs(target.Column - Position.Column);

        // Rook can only move horizontally or vertically
        var isHorizontal = rowDiff == 0 && colDiff > 0;
        var isVertical = colDiff == 0 && rowDiff > 0;

        if (!isHorizontal && !isVertical)
            return false;

        if (!IsPathClear(Position, target, board))
            return false;

        var targetPiece = board[target.Row, target.Column];
        return targetPiece == null || targetPiece.Color != Color;
    }
}
