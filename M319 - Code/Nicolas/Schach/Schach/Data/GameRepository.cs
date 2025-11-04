using System.Text.Json;
using Schach.Models.Pieces;

namespace Schach.Data;

/// <summary>
/// Interface for data persistence
/// </summary>
public interface IGameRepository
{
    void SaveGame(GameState gameState);
    GameState? LoadGame();
    bool GameExists();
}

/// <summary>
/// Represents game state for serialization
/// </summary>
public class GameState
{
    public List<PieceData> Pieces { get; init; } = [];
    public string CurrentPlayerColor { get; init; } = string.Empty;
    public List<MoveData> MoveHistory { get; init; } = [];
    public DateTime SavedAt { get; set; }
}

/// <summary>
/// Piece data for serialization
/// </summary>
public abstract class PieceData
{
    public string Type { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public int Row { get; set; }
    public int Column { get; set; }
    public bool HasMoved { get; set; }
}

/// <summary>
/// Move data for serialization
/// </summary>
public abstract class MoveData
{
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}

/// <summary>
/// JSON-based repository for game save/load
/// </summary>
public class JsonGameRepository(string filePath = "savegame.json") : IGameRepository
{
    /// <summary>
    /// Save game state to JSON file
    /// </summary>
    public void SaveGame(GameState gameState)
    {
        try
        {
            gameState.SavedAt = DateTime.Now;
            
            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true
            };
            
            string jsonString = JsonSerializer.Serialize(gameState, options);
            File.WriteAllText(filePath, jsonString);
            
            Console.WriteLine($"Game saved to: {filePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving: {ex.Message}");
            throw new IOException("Could not save game", ex);
        }
    }

    /// <summary>
    /// Load game state from JSON file
    /// </summary>
    public GameState? LoadGame()
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("No saved game file found.");
                return null;
            }

            var jsonString = File.ReadAllText(filePath);
            var gameState = JsonSerializer.Deserialize<GameState>(jsonString);
            
            Console.WriteLine($"Game loaded from: {filePath}");
            return gameState;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Check if save file exists
    /// </summary>
    public bool GameExists()
    {
        return File.Exists(filePath);
    }
}
