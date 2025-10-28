using CipherSolver.Enums;

namespace CipherSolver;

public abstract class UserConsole
{
    public static void Execute()
    {
        while (true)
        {
            Console.Write("Cipher (Caesar/Rot13/Morse): ");
            if (!Enum.TryParse(Console.ReadLine(), true, out Cipher cipher))
            {
                Console.WriteLine("Invalid cipher.\n");
                continue;
            }

            Console.Write("Mode (Encode/Decode): ");
            if (!Enum.TryParse(Console.ReadLine(), true, out CipherMode mode))
            {
                Console.WriteLine("Invalid mode.\n");
                continue;
            }

            HandleCipher(cipher, mode);

            Console.Write("\nRun again? (y/n): ");
            if (!(Console.ReadLine()?.Trim().ToLower() is "y" or "yes"))
                break;

            Console.Clear();
        }

        Console.WriteLine("Goodbye!");
    }

    private static void HandleCipher(Cipher cipher, CipherMode mode)
    {
        var action = mode == CipherMode.Encode ? "Encode" : "Decode";
        var input = GetUserInput($"What do you want to {action}?");

        var result = cipher switch
        {
            Cipher.Caesar => HandleCaesar(input, mode),
            Cipher.Rot13 => mode == CipherMode.Encode ? Rot13.Encrypt(input) : Rot13.Decrypt(input),
            Cipher.Morse => mode == CipherMode.Encode ? Morse.Encrypt(input) : Morse.Decrypt(input),
            _ => throw new ArgumentOutOfRangeException(nameof(cipher))
        };

        Console.WriteLine($"{action}d result: {result}");
    }

    private static string HandleCaesar(string input, CipherMode mode)
    {
        var direction = GetDirection(mode.ToString());
        var shiftNumber =  GetShiftNumber(direction);
        
        return mode == CipherMode.Encode
            ? Caesar.Encrypt(input, direction, shiftNumber)
            : Caesar.Decrypt(input, direction, shiftNumber);
    }
    private static string GetUserInput(string prompt)
    {
        Console.WriteLine(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    private static Direction GetDirection(string action)
    {
        while (true)
        {
            Console.WriteLine($"In which direction do you want to {action}? Left/Right");
            var input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input) && input.Equals("left", StringComparison.CurrentCultureIgnoreCase))
                return Direction.Left;
            
            if (!string.IsNullOrWhiteSpace(input) && input.Equals("right", StringComparison.CurrentCultureIgnoreCase))
                return Direction.Right;

            Console.WriteLine("Wrong Input");
        }
    }

    private static int GetShiftNumber(Direction shift)
    {
        while (true)
        {
            Console.WriteLine($"How many Characters do you want to Shift to the {shift}");
            var input = Console.ReadLine();

            if (int.TryParse(input, out var shiftNumber))
                return shiftNumber;

            Console.WriteLine("Invalid input. Please enter a number.");
        }
    }
}