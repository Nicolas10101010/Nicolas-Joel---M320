using System;

public class Account
{
    public string Id { get; }
    public string Owner { get; }
    public decimal Balance { get; private set; }

    public Account(string id, string owner, decimal initial = 0m)
    {
        Id = id;
        Owner = owner;
        Balance = initial;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        Balance -= amount;
    }
}