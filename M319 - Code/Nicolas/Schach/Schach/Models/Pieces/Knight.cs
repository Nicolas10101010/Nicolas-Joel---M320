using Schach.Models;

namespace Schach.Models.Pieces;

/// <summary>
/// Knight - moves in L-shape
/// </summary>
public class Knight(PlayerColor color, Position position) : Piece(color, position, PieceType.Knight)
{
    public override bool CanMoveTo(Position target, Piece?[,] board)
    {
        if (!target.IsValid())
            return false;

        var rowDiff = Math.Abs(target.Row - Position.Row);
        var colDiff = Math.Abs(target.Column - Position.Column);

        // Knight moves in L-shape: 2 in one direction, 1 in the other
        var isValidKnightMove = (rowDiff == 2 && colDiff == 1) || 
                                (rowDiff == 1 && colDiff == 2);

        if (!isValidKnightMove)
            return false;

        // Knight can jump over other pieces
        var targetPiece = board[target.Row, target.Column];
        return targetPiece == null || targetPiece.Color != Color;
    }
}
