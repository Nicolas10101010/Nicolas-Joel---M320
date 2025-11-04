namespace Schach.Models;

/// <summary>
/// Player color enum
/// </summary>
public enum PlayerColor
{
    White,
    Black
}

/// <summary>
/// Represents a player
/// </summary>
public class Player(PlayerColor color, string name)
{
    public PlayerColor Color { get; set; } = color;
    public string Name { get; set; } = name;

    public override string ToString()
    {
        return $"{Name} ({(Color == PlayerColor.White ? "White" : "Black")})";
    }
}
