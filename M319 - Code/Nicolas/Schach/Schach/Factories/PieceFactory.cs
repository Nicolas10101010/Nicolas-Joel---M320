using Schach.Models;
using Schach.Models.Pieces;

namespace Schach.Factories;

/// <summary>
/// Factory pattern for chess piece creation
/// </summary>
public abstract class PieceFactory
{
    /// <summary>
    /// Create piece by type and color
    /// </summary>
    private static Piece CreatePiece(PieceType type, PlayerColor color, Position position)
    {
        // Factory decides which concrete class to instantiate
        return type switch
        {
            PieceType.King => new King(color, position),
            PieceType.Queen => new Queen(color, position),
            PieceType.Rook => new Rook(color, position),
            PieceType.Bishop => new Bishop(color, position),
            PieceType.Knight => new Knight(color, position),
            PieceType.Pawn => new Pawn(color, position),
            _ => throw new ArgumentException($"Unknown piece type: {type}")
        };
    }

    /// <summary>
    /// Create all pieces for starting position
    /// </summary>
    public static List<Piece> CreateStartingPieces()
    {
        var pieces = new List<Piece>
        {
            // White pieces
            CreatePiece(PieceType.Rook, PlayerColor.White, new Position(7, 0)),
            CreatePiece(PieceType.Knight, PlayerColor.White, new Position(7, 1)),
            CreatePiece(PieceType.Bishop, PlayerColor.White, new Position(7, 2)),
            CreatePiece(PieceType.Queen, PlayerColor.White, new Position(7, 3)),
            CreatePiece(PieceType.King, PlayerColor.White, new Position(7, 4)),
            CreatePiece(PieceType.Bishop, PlayerColor.White, new Position(7, 5)),
            CreatePiece(PieceType.Knight, PlayerColor.White, new Position(7, 6)),
            CreatePiece(PieceType.Rook, PlayerColor.White, new Position(7, 7))
        };

        // White pawns
        for (var col = 0; col < 8; col++)
        {
            pieces.Add(CreatePiece(PieceType.Pawn, PlayerColor.White, new Position(6, col)));
        }

        // Black pieces
        pieces.Add(CreatePiece(PieceType.Rook, PlayerColor.Black, new Position(0, 0)));
        pieces.Add(CreatePiece(PieceType.Knight, PlayerColor.Black, new Position(0, 1)));
        pieces.Add(CreatePiece(PieceType.Bishop, PlayerColor.Black, new Position(0, 2)));
        pieces.Add(CreatePiece(PieceType.Queen, PlayerColor.Black, new Position(0, 3)));
        pieces.Add(CreatePiece(PieceType.King, PlayerColor.Black, new Position(0, 4)));
        pieces.Add(CreatePiece(PieceType.Bishop, PlayerColor.Black, new Position(0, 5)));
        pieces.Add(CreatePiece(PieceType.Knight, PlayerColor.Black, new Position(0, 6)));
        pieces.Add(CreatePiece(PieceType.Rook, PlayerColor.Black, new Position(0, 7)));

        for (var col = 0; col < 8; col++)
        {
            pieces.Add(CreatePiece(PieceType.Pawn, PlayerColor.Black, new Position(1, col)));
        }

        return pieces;
    }
}
