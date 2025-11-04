using System.Text;
using CipherSolver.Enums;

namespace CipherSolver;

public static class Caesar
{
    private static readonly Dictionary<Direction, int> DirectionMultiplier = new()
    {
        { Direction.Right, 1 },
        { Direction.Left, -1 }
    };

    public static string Encrypt(string input, Direction direction, int shiftNumber)
    {
        var result = new StringBuilder();
        var actualShift = shiftNumber * DirectionMultiplier[direction];

        foreach (var character in input)
        {
            if (!char.IsLetter(character))
            {
                result.Append(character);
                continue;
            }

            var alphabetStart = char.IsUpper(character) ? 'A' : 'a';
            result.Append((char)((character + actualShift - alphabetStart + 26) % 26 + alphabetStart));
        }
        return result.ToString();
    }

    public static string Decrypt(string input, Direction direction, int shiftNumber)
    {
        var oppositeDirection = direction is Direction.Left ? Direction.Right : Direction.Left;
        return Encrypt(input, oppositeDirection, shiftNumber);
    }
}