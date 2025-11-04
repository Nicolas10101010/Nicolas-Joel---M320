namespace CipherSolver;

public class RandomOrgService
{
    private static readonly HttpClient client = new HttpClient();


    /// Generiert einen echten Zufalls-Shift für Caesar Cipher
    public static async Task<int> GenerateRandomShift()
    {
        try
        {
            // Random.org API gibt echte Zufallszahlen zurück (1-25 für Caesar)
            var response = await client.GetStringAsync(
                "https://www.random.org/integers/?num=1&min=1&max=25&col=1&base=10&format=plain&rnd=new");

            if (int.TryParse(response.Trim(), out var shift))
            {
                return shift;
            }
            throw new Exception("Konnte Zufallszahl nicht parsen");
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"API-Aufruf fehlgeschlagen: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Unerwarteter Fehler: {ex.Message}");
        }
    }

    /// Generiert einen zufälligen String zum Verschlüsseln
    public static async Task<string> GenerateRandomString(int length)
    {
        if (length < 1 || length > 100)
            throw new ArgumentException("Länge muss zwischen 1 und 100 sein");

        try
        {
            // Generiert zufällige Buchstaben (65-90 = A-Z)
            var response = await client.GetStringAsync(
                $"https://www.random.org/integers/?num={length}&min=65&max=90&col=1&base=10&format=plain&rnd=new");

            var numbers = response.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var result = string.Empty;

            foreach (var num in numbers)
            {
                if (int.TryParse(num.Trim(), out var asciiValue))
                {
                    result += (char)asciiValue;
                }
            }

            return result;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"API-Aufruf fehlgeschlagen: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Unerwarteter Fehler: {ex.Message}");
        }
    }

    /// Generiert einen kryptographisch sicheren Schlüssel
    public static async Task<string> GenerateCryptoKey(int keyLength)
    {
        if (keyLength < 4 || keyLength > 32)
            throw new ArgumentException("Schlüssellänge muss zwischen 4 und 32 sein");

        try
        {
            // Generiert zufällige Bytes für einen Hex-Schlüssel
            var response = await client.GetStringAsync(
                $"https://www.random.org/integers/?num={keyLength}&min=0&max=15&col={keyLength}&base=16&format=plain&rnd=new");

            return response.Replace("\n", "").Replace("\t", "").Replace(" ", "");
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"API-Aufruf fehlgeschlagen: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Unerwarteter Fehler: {ex.Message}");
        }
    }
}