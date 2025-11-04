using System;

namespace Schach.Models;

/// <summary>
/// Represents a position on the chess board
/// </summary>
public class Position
{
    public int Row { get; }
    public int Column { get; }

    public Position(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public Position(string notation)
    {
        if (string.IsNullOrWhiteSpace(notation) || notation.Length != 2)
            throw new ArgumentException("Invalid notation");

        notation = notation.ToLowerInvariant();
        char file = notation[0];
        char rank = notation[1];

        if (file < 'a' || file > 'h' || rank < '1' || rank > '8')
            throw new ArgumentException("Position outside board");

        Column = file - 'a';
        Row = 8 - (rank - '0');
    }

    public bool IsValid()
    {
        return Row >= 0 && Row < 8 && Column >= 0 && Column < 8;
    }

    public string ToNotation()
    {
        return $"{(char)('a' + Column)}{8 - Row}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Position other)
            return Row == other.Row && Column == other.Column;
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Row, Column);
    }

    public override string ToString()
    {
        return ToNotation();
    }
}
