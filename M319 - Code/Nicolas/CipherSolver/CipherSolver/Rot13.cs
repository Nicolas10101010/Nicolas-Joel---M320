using CipherSolver.Enums;

namespace CipherSolver;

public static class Rot13
{
    public static string Encrypt(string input)
    {
        return Caesar.Encrypt(input, Direction.Right, 13);
    }

    public static string Decrypt(string input)
    {
        return Caesar.Decrypt(input, Direction.Right, 13);
    }
}