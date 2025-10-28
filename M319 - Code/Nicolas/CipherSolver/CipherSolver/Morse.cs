using System.Text;

namespace CipherSolver;

public static class Morse
{
    private static readonly Dictionary<char, string> MorseTable = new()
    {
        { 'A', ".-" }, { 'B', "-..." }, { 'C', "-.-." }, { 'D', "-.." },
        { 'E', "." }, { 'F', "..-." }, { 'G', "--." }, { 'H', "...." },
        { 'I', ".." }, { 'J', ".---" }, { 'K', "-.-" }, { 'L', ".-.." },
        { 'M', "--" }, { 'N', "-." }, { 'O', "---" }, { 'P', ".--." },
        { 'Q', "--.-" }, { 'R', ".-." }, { 'S', "..." }, { 'T', "-" },
        { 'U', "..-" }, { 'V', "...-" }, { 'W', ".--" }, { 'X', "-..-" },
        { 'Y', "-.--" }, { 'Z', "--.." },
        { 'Ä', ".-.-" }, { 'Ö', "---." }, { 'Ü', "..--" },
        { '0', "-----" }, { '1', ".----" }, { '2', "..---" }, { '3', "...--" },
        { '4', "....-" }, { '5', "....." }, { '6', "-...." }, { '7', "--..." },
        { '8', "---.." }, { '9', "----." },
        { '.', ".-.-.-" }, { ',', "--..--" }, { '?', "..--.." }, { '!', "-.-.--" },
        { '-', "-....-" }, { '/', "-..-." }, { '@', ".--.-." }, { '(', "-.--." },
        { ')', "-.--.-" }, { ':', "---..." }, { ';', "-.-.-." }, { '=', "-...-" },
        { '+', ".-.-." }, { '_', "..--.-" }, { '"', ".-..-." }, { '$', "...-..-" },
        { '&', ".-..." }, { ' ', "/" }
    };

    public static string Encrypt(string input)
    {
        var result = new StringBuilder();
        foreach (var ch in input.ToUpper())
        {
            result.Append(MorseTable.GetValueOrDefault(ch, "?")).Append(' ');
        }
        return result.ToString();
    }

    public static string Decrypt(string input)
    {
        var result = new StringBuilder();
        var codes = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (var code in codes)
        {
            var match = MorseTable.FirstOrDefault(kv => kv.Value == code);
            result.Append(match.Key != '\0' ? match.Key : '?');
        }

        return result.ToString().ToLower();
    }
}