using System;
using System.Collections.Generic;

public class Bank
{
    private readonly List<Account> accounts = new();

    public Account CreateAccount(string id, string owner, decimal initial = 0m)
    {
        var acc = new Account(id, owner, initial);
        accounts.Add(acc);
        return acc;
    }

    public Account? GetAccount(string id)
    {
        return accounts.Find(a => a.Id == id);
    }

    public IEnumerable<Account> ListAccounts() => accounts;

    public void Transfer(string fromId, string toId, decimal amount)
    {
        var from = GetAccount(fromId);
        var to = GetAccount(toId);
        if (from == null || to == null) return;

        from.Withdraw(amount);
        to.Deposit(amount);
    }
}