using Schach.Models;

namespace Schach.Models.Pieces;

/// <summary>
/// Queen - moves any distance in all directions
/// </summary>
public class Queen(PlayerColor color, Position position) : Piece(color, position, PieceType.Queen)
{
    public override bool CanMoveTo(Position target, Piece?[,] board)
    {
        if (!target.IsValid())
            return false;

        var rowDiff = Math.Abs(target.Row - Position.Row);
        var colDiff = Math.Abs(target.Column - Position.Column);

        // Queen can move horizontal, vertical or diagonal
        var isHorizontal = rowDiff == 0 && colDiff > 0;
        var isVertical = colDiff == 0 && rowDiff > 0;
        var isDiagonal = rowDiff == colDiff && rowDiff > 0;

        if (!isHorizontal && !isVertical && !isDiagonal)
            return false;

        // Check if path is clear
        if (!IsPathClear(Position, target, board))
            return false;

        // Cannot move to own piece
        var targetPiece = board[target.Row, target.Column];
        return targetPiece == null || targetPiece.Color != Color;
    }
}
