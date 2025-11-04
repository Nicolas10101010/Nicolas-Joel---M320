using Schach.Models;
using Schach.Models.Pieces;

namespace Schach.Logic;

/// <summary>
/// Singleton game manager
/// </summary>
public class GameManager
{
    private static GameManager? _instance;
    private static readonly Lock Lock = new Lock();

    private readonly Player _whitePlayer;
    private readonly Player _blackPlayer;

    // Private constructor for singleton
    private GameManager()
    {
        Board = new Board();
        _whitePlayer = new Player(PlayerColor.White, "White");
        _blackPlayer = new Player(PlayerColor.Black, "Black");
        CurrentPlayer = _whitePlayer;
        MoveHistory = [];
        IsGameOver = false;
    }

    /// <summary>
    /// Thread-safe singleton instance
    /// </summary>
    public static GameManager Instance
    {
        get
        {
            lock (Lock)
            {
                _instance ??= new GameManager();
                return _instance;
            }
        }
    }

    public Board Board { get; private set; }

    public Player CurrentPlayer { get; private set; }

    private List<Move> MoveHistory { get; }

    public bool IsGameOver { get; private set; }

    /// <summary>
    /// Start a new game
    /// </summary>
    public void NewGame()
    {
        Board = new Board();
        CurrentPlayer = _whitePlayer;
        MoveHistory.Clear();
        IsGameOver = false;
    }

    /// <summary>
    /// Make a move (by notation)
    /// </summary>
    public bool MakeMove(string fromNotation, string toNotation)
    {
        try
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(fromNotation) || string.IsNullOrWhiteSpace(toNotation))
            {
                throw new ArgumentException("Invalid input");
            }

            var from = new Position(fromNotation.ToLower());
            var to = new Position(toNotation.ToLower());

            return MakeMove(from, to);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Make a move (by positions)
    /// </summary>
    private bool MakeMove(Position from, Position to)
    {
        if (IsGameOver)
        {
            Console.WriteLine("The game is already over!");
            return false;
        }

        if (!Board.MovePiece(from, to, CurrentPlayer.Color)) return false;
        MoveHistory.Add(new Move(from, to));
            
        var opponentColor = CurrentPlayer.Color == PlayerColor.White 
            ? PlayerColor.Black 
            : PlayerColor.White;
            
        if (Board.IsKingInCheck(opponentColor))
        {
            Console.WriteLine("\nCheck!");
                
            if (IsCheckmate(opponentColor))
            {
                Console.WriteLine($"Checkmate! {CurrentPlayer.Name} wins!");
                IsGameOver = true;
            }
        }

        SwitchPlayer();
        return true;

    }

    /// <summary>
    /// Switch current player
    /// </summary>
    private void SwitchPlayer()
    {
        CurrentPlayer = CurrentPlayer.Color == PlayerColor.White 
            ? _blackPlayer 
            : _whitePlayer;
    }

    /// <summary>
    /// Check checkmate
    /// </summary>
    private bool IsCheckmate(PlayerColor color)
    {
        if (!Board.IsKingInCheck(color))
            return false;

        var possibleMoves = Board.GetAllPossibleMoves(color);
        return possibleMoves.Count == 0;
    }

    /// <summary>
    /// Print game statistics
    /// </summary>
    public void PrintGameStats()
    {
        Console.WriteLine($"\n=== Game Statistics ===");
        Console.WriteLine($"Number of moves: {MoveHistory.Count}");
        Console.WriteLine($"Current player: {CurrentPlayer}");
        Console.WriteLine($"Captured pieces: {Board.GetCapturedPieces().Count}");
    }
}
