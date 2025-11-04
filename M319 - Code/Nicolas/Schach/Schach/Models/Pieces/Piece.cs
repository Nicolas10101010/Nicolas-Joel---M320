using Schach.Models;

namespace Schach.Models.Pieces;

/// <summary>
/// Piece type enum
/// </summary>
public enum PieceType
{
    King,
    Queen,
    Rook,
    Bishop,
    Knight,
    Pawn
}

/// <summary>
/// Abstract base class for all chess pieces
/// </summary>
public abstract class Piece(PlayerColor color, Position position, PieceType type)
{
    public PlayerColor Color { get; protected set; } = color;
    public Position Position { get; set; } = position;
    public PieceType Type { get; protected set; } = type;
    public bool HasMoved { get; set; } = false;

    /// <summary>
    /// Abstract method - must be implemented by each piece
    /// </summary>
    public abstract bool CanMoveTo(Position target, Piece?[,] board);

    /// <summary>
    /// Check if path is clear (for Bishop, Rook, Queen)
    /// </summary>
    protected bool IsPathClear(Position from, Position to, Piece?[,] board)
    {
        var rowDirection = Math.Sign(to.Row - from.Row);
        var colDirection = Math.Sign(to.Column - from.Column);

        var currentRow = from.Row + rowDirection;
        var currentCol = from.Column + colDirection;

        while (currentRow != to.Row || currentCol != to.Column)
        {
            if (board[currentRow, currentCol] != null)
                return false;

            currentRow += rowDirection;
            currentCol += colDirection;
        }

        return true;
    }

    public virtual char GetSymbol()
    {
        var symbol = Type switch
        {
            PieceType.King => 'K',
            PieceType.Queen => 'Q',
            PieceType.Rook => 'R',
            PieceType.Bishop => 'B',
            PieceType.Knight => 'N',
            PieceType.Pawn => 'P',
            _ => '?'
        };

        return Color == PlayerColor.White ? symbol : char.ToLower(symbol);
    }

    public override string ToString()
    {
        return $"{Color} {Type} at {Position}";
    }
}
