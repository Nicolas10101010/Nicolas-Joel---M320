using Schach.Models;

namespace Schach.Models.Pieces;

/// <summary>
/// Pawn - moves forward, captures diagonally
/// </summary>
public class Pawn(PlayerColor color, Position position) : Piece(color, position, PieceType.Pawn)
{
    public override bool CanMoveTo(Position target, Piece?[,] board)
    {
        if (!target.IsValid())
            return false;

        // Direction depends on color (White moves up, Black moves down)
        var direction = Color == PlayerColor.White ? -1 : 1;
        var startRow = Color == PlayerColor.White ? 6 : 1;

        var rowDiff = target.Row - Position.Row;
        var colDiff = Math.Abs(target.Column - Position.Column);

        var targetPiece = board[target.Row, target.Column];

        switch (colDiff)
        {
            // Move forward (1 square)
            case 0 when rowDiff == direction && targetPiece == null:
                return true;
            // Move forward (2 squares from start)
            case 0 when rowDiff == 2 * direction && 
                        Position.Row == startRow && targetPiece == null:
            {
                // Check if both squares are free
                var middleRow = Position.Row + direction;
                if (board[middleRow, Position.Column] == null)
                    return true;
                break;
            }
            // Capture diagonally
            case 1 when rowDiff == direction && 
                        targetPiece != null && targetPiece.Color != Color:
                return true;
        }

        return false;
    }
}
