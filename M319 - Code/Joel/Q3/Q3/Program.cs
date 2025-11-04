namespace Q3;

public class AirportHashMap
{
    private readonly Dictionary<string, string> _airports = new();
    private readonly List<string> _searchHistory = new();

    public void LoadAirportsFromFile(string filePath)
    {
        try
        {
            var lines = File.ReadAllLines(filePath);
            
            for (var i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                if (string.IsNullOrEmpty(line)) continue;
                var parts = line.Split(',');
                if (parts.Length < 2) continue;
                var code = parts[0].Trim();
                var name = parts[1].Trim();
                _airports[code] = name;
            }

            Console.WriteLine($"{_airports.Count} Flughäfen geladen!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler: {ex.Message}");
        }
    }

    public string SearchByCode(string code)
    {
        var result = _airports.GetValueOrDefault(code, "Nicht gefunden");
        _searchHistory.Add($"{code} -> {result}");
        return result;
    }

    public void AddAirport(string code, string name)
    {
        _airports[code] = name;
        Console.WriteLine($"Flughafen {code} hinzugefügt");
    }

    public void ShowAllAirports()
    {
        Console.WriteLine("Code -> Name");
        foreach (var airport in _airports)
        {
            Console.WriteLine($"{airport.Key} -> {airport.Value}");
        }
    }

    public void ShowAllAirportsSorted()
    {
        Console.WriteLine("Code -> Name (alphabetisch nach Code)");
        var sortedAirports = _airports.OrderBy(x => x.Key);
        foreach (var airport in sortedAirports)
        {
            Console.WriteLine($"{airport.Key} -> {airport.Value}");
        }
    }

    public void ShowSearchHistory()
    {
        Console.WriteLine("Suchverlauf:");
        if (_searchHistory.Count == 0)
        {
            Console.WriteLine("Keine Suchen durchgeführt");
            return;
        }
        
        for (var i = 0; i < _searchHistory.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_searchHistory[i]}");
        }
    }

    public int GetCount()
    {
        return _airports.Count;
    }
}

public abstract class Program
{
    public static void Main(string[] args)
    {
        var airportMap = new AirportHashMap();
        
        const string csvPath = @"C:\Users\nicol\OneDrive - TBZ\Github\Nicolas-Joel---M320\M319 - Code\Nicolas\Q3\Q3\airports.csv";
        airportMap.LoadAirportsFromFile(csvPath);
        
        while (true)
        {
            Console.WriteLine("\n1 - Suchen");
            Console.WriteLine("2 - Alle anzeigen");
            Console.WriteLine("3 - Alle alphabetisch anzeigen");
            Console.WriteLine("4 - Hinzufügen");
            Console.WriteLine("5 - Suchverlauf anzeigen");
            Console.WriteLine("6 - Anzahl");
            Console.WriteLine("0 - Beenden");
            Console.Write("Wähle: ");
            
            var choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    Console.Write("Code: ");
                    var code = Console.ReadLine()?.ToUpper();
                    if (!string.IsNullOrEmpty(code))
                    {
                        var result = airportMap.SearchByCode(code);
                        Console.WriteLine($"{code} -> {result}");
                    }
                    break;
                    
                case "2":
                    airportMap.ShowAllAirports();
                    break;
                    
                case "3":
                    airportMap.ShowAllAirportsSorted();
                    break;
                    
                case "4":
                    Console.Write("Code: ");
                    var newCode = Console.ReadLine()?.ToUpper();
                    Console.Write("Name: ");
                    var newName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newCode) && !string.IsNullOrEmpty(newName))
                    {
                        airportMap.AddAirport(newCode, newName);
                    }
                    break;
                    
                case "5":
                    airportMap.ShowSearchHistory();
                    break;
                    
                case "6":
                    Console.WriteLine($"Anzahl: {airportMap.GetCount()}");
                    break;
                    
                case "0":
                    return;
                    
                default:
                    Console.WriteLine("Ungültig!");
                    break;
            }
        }
    }
}