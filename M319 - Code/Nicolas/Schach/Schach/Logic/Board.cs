using Schach.Models;
using Schach.Models.Pieces;
using Schach.Factories;

namespace Schach.Logic;

/// <summary>
/// Chess board with all pieces
/// </summary>
public class Board
{
    private readonly Piece?[,] _squares;
    private readonly List<Piece> _capturedPieces;
    private readonly MovementValidator _validator;

    public Board()
    {
        _squares = new Piece?[8, 8];
        _capturedPieces = [];
        _validator = new MovementValidator();
        InitializeBoard();
    }

    /// <summary>
    /// Initialize board with starting position
    /// </summary>
    private void InitializeBoard()
    {
        var pieces = PieceFactory.CreateStartingPieces();
        
        foreach (var piece in pieces)
        {
            _squares[piece.Position.Row, piece.Position.Column] = piece;
        }
    }

    /// <summary>
    /// Get piece at position
    /// </summary>
    public Piece? GetPieceAt(Position position)
    {
        return !position.IsValid() ? null : _squares[position.Row, position.Column];
    }

    /// <summary>
    /// Move piece from one position to another
    /// </summary>
    public bool MovePiece(Position from, Position to, PlayerColor currentPlayer)
    {
        try
        {
            // Delegate validation to MovementValidator
            if (!MovementValidator.IsValidMove(from, to, currentPlayer, _squares))
            {
                return false;
            }

            var piece = _squares[from.Row, from.Column];
            var targetPiece = _squares[to.Row, to.Column];

            // Capture piece
            if (targetPiece != null)
            {
                _capturedPieces.Add(targetPiece);
            }

            // Execute move
            _squares[to.Row, to.Column] = piece;
            _squares[from.Row, from.Column] = null;

            if (piece == null) return true;
            piece.Position = to;
            piece.HasMoved = true;

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during move: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Check if king is in check
    /// </summary>
    public bool IsKingInCheck(PlayerColor color)
    {
        // Find king
        Position? kingPosition = null;
        for (var row = 0; row < 8; row++)
        {
            for (var col = 0; col < 8; col++)
            {
                var piece = _squares[row, col];
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
                var piece = _squares[row, col];
                if (piece == null || piece.Color == color) continue;
                if (piece.CanMoveTo(kingPosition, _squares))
                    return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Get board as 2D array
    /// </summary>
    public Piece?[,] GetBoard()
    {
        return _squares;
    }

    /// <summary>
    /// Get all captured pieces
    /// </summary>
    public List<Piece> GetCapturedPieces()
    {
        return _capturedPieces;
    }

    /// <summary>
    /// Find all possible moves for a player
    /// </summary>
    public List<Move> GetAllPossibleMoves(PlayerColor color)
    {
        var moves = new List<Move>();

        for (var row = 0; row < 8; row++)
        {
            for (var col = 0; col < 8; col++)
            {
                var piece = _squares[row, col];
                if (piece?.Color != color) continue;
                for (var targetRow = 0; targetRow < 8; targetRow++)
                {
                    for (var targetCol = 0; targetCol < 8; targetCol++)
                    {
                        var target = new Position(targetRow, targetCol);
                        if (MovementValidator.IsValidMove(piece.Position, target, color, _squares))
                        {
                            moves.Add(new Move(piece.Position, target));
                        }
                    }
                }
            }
        }

        return moves;
    }
}

