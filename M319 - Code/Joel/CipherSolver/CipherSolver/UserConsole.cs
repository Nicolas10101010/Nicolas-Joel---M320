using CipherSolver.Enums;

namespace CipherSolver;

public abstract class UserConsole
{
    public static void Execute()
    {
        while (true)
        {
            Console.Write("Cipher (Caesar/Rot13/Morse/RandomGenerator): ");
            if (!Enum.TryParse(Console.ReadLine(), true, out Enums.Cipher cipher))
            {
                Console.WriteLine("Invalid cipher.\n");
                continue;
            }

            if (cipher == Enums.Cipher.RandomGenerator)
            {
                HandleRandomGenerator();
            }
            else
            {
                Console.Write("Mode (Encode/Decode): ");
                if (!Enum.TryParse(Console.ReadLine(), true, out Enums.CipherMode mode))
                {
                    Console.WriteLine("Invalid mode.\n");
                    continue;
                }

                HandleCipher(cipher, mode);
            }

            Console.Write("\nRun again? (y/n): ");
            if (!(Console.ReadLine()?.Trim().ToLower() is "y" or "yes"))
                break;

            Console.Clear();
        }

        Console.WriteLine("Goodbye");
    }

    private static void HandleCipher(Enums.Cipher cipher, Enums.CipherMode mode)
    {
        var action = mode == Enums.CipherMode.Encode ? "Encode" : "Decode";
        var input = GetUserInput($"What do you want to {action}?");

        var result = cipher switch
        {
            Enums.Cipher.Caesar => HandleCaesar(input, mode),
            Enums.Cipher.Rot13 => mode == Enums.CipherMode.Encode ? Rot13.Encrypt(input) : Rot13.Decrypt(input),
            Enums.Cipher.Morse => mode == Enums.CipherMode.Encode ? Morse.Encrypt(input) : Morse.Decrypt(input),
            _ => throw new ArgumentOutOfRangeException(nameof(cipher))
        };

        Console.WriteLine($"{action}d result: {result}");
    }

    private static void HandleRandomGenerator()
    {
        Console.WriteLine("\n=== Random Crypto Generator ===");
        Console.WriteLine("1 - Generate random Caesar shift (1-25)");
        Console.WriteLine("2 - Generate random text to encrypt");
        Console.WriteLine("3 - Generate cryptographic key");
        Console.WriteLine("4 - Auto-encrypt with random shift");
        Console.Write("Choose option: ");

        var option = Console.ReadLine();

        try
        {
            switch (option)
            {
                case "1":
                    Console.WriteLine("\nGenerating random shift...");
                    var shift = RandomOrgService.GenerateRandomShift().Result;
                    Console.WriteLine($"Random Shift: {shift}");
                    Console.WriteLine($"Use this shift for your Caesar cipher!");
                    break;

                case "2":
                    Console.Write("How many characters? (1-100): ");
                    if (!int.TryParse(Console.ReadLine(), out var length))
                    {
                        Console.WriteLine("Invalid length!");
                        return;
                    }

                    Console.WriteLine("\nGenerating random text...");
                    var randomText = RandomOrgService.GenerateRandomString(length).Result;
                    Console.WriteLine($"Random Text: {randomText}");
                    break;

                case "3":
                    Console.Write("Key length? (4-32): ");
                    if (!int.TryParse(Console.ReadLine(), out var keyLength))
                    {
                        Console.WriteLine("Invalid length!");
                        return;
                    }

                    Console.WriteLine("\nGenerating crypto key...");
                    var key = RandomOrgService.GenerateCryptoKey(keyLength).Result;
                    Console.WriteLine($"Cryptographic Key: {key}");
                    Console.WriteLine("(This is a secure random hexadecimal key)");
                    break;

                case "4":
                    Console.Write("Enter text to encrypt: ");
                    var text = Console.ReadLine() ?? string.Empty;

                    if (string.IsNullOrWhiteSpace(text))
                    {
                        Console.WriteLine("No text provided!");
                        return;
                    }

                    Console.WriteLine("\nGenerating random shift and encrypting...");
                    var randomShift = RandomOrgService.GenerateRandomShift().Result;
                    var encrypted = Caesar.Encrypt(text, Enums.Direction.Right, randomShift);

                    Console.WriteLine($"\nOriginal: {text}");
                    Console.WriteLine($"Shift used: {randomShift}");
                    Console.WriteLine($"Encrypted: {encrypted}");
                    Console.WriteLine($"\n(To decrypt, use Caesar cipher with shift {randomShift} to the LEFT)");
                    break;

                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Validation Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static string HandleCaesar(string input, Enums.CipherMode mode)
    {
        var direction = GetDirection(mode.ToString());
        var shiftNumber = GetShiftNumber(direction);

        return mode == Enums.CipherMode.Encode
            ? Caesar.Encrypt(input, direction, shiftNumber)
            : Caesar.Decrypt(input, direction, shiftNumber);
    }

    private static string GetUserInput(string prompt)
    {
        Console.WriteLine(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    private static Enums.Direction GetDirection(string action)
    {
        while (true)
        {
            Console.WriteLine($"In which direction do you want to {action}? Left/Right");
            var input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input) && input.Equals("left", StringComparison.CurrentCultureIgnoreCase))
                return Enums.Direction.Left;

            if (!string.IsNullOrWhiteSpace(input) && input.Equals("right", StringComparison.CurrentCultureIgnoreCase))
                return Enums.Direction.Right;

            Console.WriteLine("Wrong Input");
        }
    }

    private static int GetShiftNumber(Enums.Direction shift)
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