using Schach.Models;
using Schach.Models.Pieces;

namespace Schach.Logic;

/// <summary>
/// Validates moves
/// </summary>
public class MovementValidator
{
    /// <summary>
    /// Check if move is valid
    /// </summary>
    public static bool IsValidMove(Position from, Position to, PlayerColor currentPlayer, Piece?[,] board)
    {
        try
        {
            // Validation: positions must be on board
            if (!from.IsValid() || !to.IsValid())
                return false;

            // Validation: from and to must be different
            if (from.Equals(to))
                return false;

            var piece = board[from.Row, from.Column];

            // Validation: piece must exist at start position
            if (piece == null)
                return false;

            // Validation: piece must belong to current player
            if (piece.Color != currentPlayer)
                return false;

            // Validation: piece must be able to move there
            if (!piece.CanMoveTo(to, board))
                return false;

            // Validation: move must not put own king in check
            return !WouldKingBeInCheck(from, to, currentPlayer, board);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Validation error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Simulate move and check if own king would be in check
    /// </summary>
    private static bool WouldKingBeInCheck(Position from, Position to, PlayerColor color, Piece?[,] board)
    {
        // Simulate move
        var piece = board[from.Row, from.Column];
        var targetPiece = board[to.Row, to.Column];
        var originalPosition = piece?.Position;

        board[to.Row, to.Column] = piece;
        board[from.Row, from.Column] = null;
        if (piece != null)
            piece.Position = to;

        // Check if king is in check
        var kingInCheck = IsKingInCheck(color, board);

        // Undo move
        board[from.Row, from.Column] = piece;
        board[to.Row, to.Column] = targetPiece;
        if (piece != null && originalPosition != null)
            piece.Position = originalPosition;

        return kingInCheck;
    }

    /// <summary>
    /// Check if king is in check
    /// </summary>
    private static bool IsKingInCheck(PlayerColor color, Piece?[,] board)
    {
        // Find king
        Position? kingPosition = null;
        for (var row = 0; row < 8; row++)
        {
            for (var col = 0; col < 8; col++)
            {
                var piece = board[row, col];
                if (piece?.Type != PieceType.King || piece.Color != color) continue;
                kingPosition = piece.Position;
                break;
            }
            if (kingPosition != null) break;
        }

        if (kingPosition == null)
            return false;

        // Check if any opponent piece can capture the king
        for (var row = 0; row < 8; row++)
        {
            for (var col = 0; col < 8; col++)
            {
                var piece = board[row, col];
                if (piece == null || piece.Color == color) continue;
                if (piece.CanMoveTo(kingPosition, board))
                    return true;
            }
        }

        return false;
    }
}
