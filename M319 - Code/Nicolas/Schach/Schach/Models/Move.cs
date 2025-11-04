using System;

namespace Schach.Models;

/// <summary>
/// Represents a move in chess
/// </summary>
public class Move(Position from, Position to)
{
    private Position From { get; set; } = from;
    private Position To { get; set; } = to;
    private DateTime Timestamp { get; set; } = DateTime.Now;

    public override string ToString()
    {
        return $"{Timestamp:yyyy-MM-dd HH:mm:ss} - {From.ToNotation()} -> {To.ToNotation()}";
    }
}
