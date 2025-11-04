using Schach.Logic;
using Schach.Models;
using Schach.Models.Pieces;

namespace Schach.UI;

/// <summary>
/// UI for console interaction
/// </summary>
public class ConsoleUi
{
    private readonly GameManager _gameManager = GameManager.Instance;

    /// <summary>
    /// Main menu loop
    /// </summary>
    public void ShowMainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════╗");
            Console.WriteLine("║        CHESS - MAIN MENU       ║");
            Console.WriteLine("╚════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("1. Start new game");
            Console.WriteLine("2. Exit");
            Console.WriteLine();
            Console.Write("Choose an option: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    StartNewGame();
                    break;
                case "2":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid input!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    /// <summary>
    /// Start a new game
    /// </summary>
    private void StartNewGame()
    {
        _gameManager.NewGame();
        Console.WriteLine("\nNew game started!");
        Console.ReadKey();
        PlayGame();
    }

    /// <summary>
    /// Game loop
    /// </summary>
    private void PlayGame()
    {
        while (!_gameManager.IsGameOver)
        {
            Console.Clear();
            DisplayBoard();
            Console.WriteLine();
            Console.WriteLine($"Current turn: {_gameManager.CurrentPlayer}");
            Console.WriteLine();

            if (_gameManager.Board.IsKingInCheck(_gameManager.CurrentPlayer.Color))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("*** YOUR KING IS IN CHECK! ***");
                Console.ResetColor();
                Console.WriteLine();
            }

            Console.WriteLine("Commands: 'move' to make a move, 'help' for help, 'menu' for main menu");
            Console.Write("> ");

            var command = Console.ReadLine()?.ToLower();

            switch (command)
            {
                case "move":
                    MakeMove();
                    break;
                case "help":
                    ShowHelp();
                    break;
                case "menu":
                    if (ConfirmAction("Return to menu? Unsaved progress will be lost!"))
                        return;
                    break;
                case "stats":
                    _gameManager.PrintGameStats();
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Unknown command!");
                    Console.ReadKey();
                    break;
            }
        }

        Console.WriteLine("\nGame over!");
        Console.ReadKey();
    }

    /// <summary>
    /// Execute a move (input validation)
    /// </summary>
    private void MakeMove()
    {
        try
        {
            Console.Write("From (e.g. e2): ");
            var from = Console.ReadLine();
            
            Console.Write("To (e.g. e4): ");
            var to = Console.ReadLine();

            // Input validation
            if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to))
            {
                Console.WriteLine("Invalid input!");
                Console.ReadKey();
                return;
            }

            if (_gameManager.MakeMove(from, to))
            {
                Console.WriteLine("Move executed!");
            }
            else
            {
                Console.WriteLine("Invalid move!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        Console.ReadKey();
    }

    /// <summary>
    /// Display the board
    /// </summary>
    private void DisplayBoard()
    {
        var board = _gameManager.Board.GetBoard();

        Console.WriteLine("    a  b  c  d  e  f  g  h");
        Console.WriteLine("  ┌────────────────────────┐");

        for (int row = 0; row < 8; row++)
        {
            Console.Write($"{8 - row} │");

            for (var col = 0; col < 8; col++)
            {
                var piece = board[row, col];
                
                // Board pattern (light/dark)
                Console.BackgroundColor = (row + col) % 2 == 0 ? ConsoleColor.Black : ConsoleColor.DarkGray;

                if (piece != null)
                {
                    // Piece color
                    Console.ForegroundColor = piece.Color == PlayerColor.White 
                        ? ConsoleColor.White 
                        : ConsoleColor.Yellow;
                    
                    Console.Write($" {piece.GetSymbol()} ");
                }
                else
                {
                    Console.Write("   ");
                }

                Console.ResetColor();
            }

            Console.WriteLine($"│ {8 - row}");
        }

        Console.WriteLine("  └────────────────────────┘");
        Console.WriteLine("    a  b  c  d  e  f  g  h");
    }

    /// <summary>
    /// Show help
    /// </summary>
    private static void ShowHelp()
    {
        Console.Clear();
        Console.WriteLine("═══════════════ HELP ═══════════════");
        Console.WriteLine();
        Console.WriteLine("PIECES:");
        Console.WriteLine("  K/k = King");
        Console.WriteLine("  Q/q = Queen");
        Console.WriteLine("  R/r = Rook");
        Console.WriteLine("  B/b = Bishop");
        Console.WriteLine("  N/n = Knight");
        Console.WriteLine("  P/p = Pawn");
        Console.WriteLine();
        Console.WriteLine("UPPERCASE = White pieces");
        Console.WriteLine("lowercase = Black pieces");
        Console.WriteLine();
        Console.WriteLine("MOVES:");
        Console.WriteLine("  Enter start position (e.g. e2)");
        Console.WriteLine("  Then enter target position (e.g. e4)");
        Console.WriteLine();
        Console.WriteLine("COMMANDS:");
        Console.WriteLine("  move  - Make a move");
        Console.WriteLine("  help  - Show this help");
        Console.WriteLine("  stats - Show game statistics");
        Console.WriteLine("  menu  - Back to main menu");
        Console.WriteLine();
        Console.WriteLine("════════════════════════════════════");
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    /// <summary>
    /// Confirmation prompt
    /// </summary>
    private static bool ConfirmAction(string message)
    {
        Console.Write($"\n{message} (y/n): ");
        var response = Console.ReadLine()?.ToLower();
        return response == "y" || response == "yes";
    }
}
