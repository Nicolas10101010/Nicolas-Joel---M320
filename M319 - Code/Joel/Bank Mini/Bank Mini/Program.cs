using System;

class Program
{
    static void Main()
    {
        var bank = new Bank();
        Console.WriteLine("Mini-Bank (new/list/dep/wd/tx/exit)");

        while (true)
        {
            Console.Write("> ");
            var input = Console.ReadLine()?.Split(' ');
            if (input == null || input.Length == 0) continue;

            switch (input[0].ToLower())
            {
                case "new":
                    bank.CreateAccount(input[1], input[2], input.Length > 3 ? decimal.Parse(input[3]) : 0);
                    Console.WriteLine("Konto erstellt.");
                    break;

                case "list":
                    foreach (var a in bank.ListAccounts())
                        Console.WriteLine($"{a.Id} {a.Owner} {a.Balance} CHF");
                    break;

                case "dep":
                    bank.GetAccount(input[1])?.Deposit(decimal.Parse(input[2]));
                    Console.WriteLine("Einzahlung OK.");
                    break;

                case "wd":
                    bank.GetAccount(input[1])?.Withdraw(decimal.Parse(input[2]));
                    Console.WriteLine("Abhebung OK.");
                    break;

                case "tx":
                    bank.Transfer(input[1], input[2], decimal.Parse(input[3]));
                    Console.WriteLine("Überweisung OK.");
                    break;

                case "exit":
                    return;
            }
        }
    }
}