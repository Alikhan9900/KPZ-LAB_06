// src/Account.cs
using System;

namespace ATMProject
{
    public class Account
    {
        public string AccountNumber { get; private set; }
        public decimal Balance { get; private set; }
        public Card Card { get; private set; }
        private const int HASH = 17;
        private const int MultiplicativeCoefficient = 23;

        public Account(string accountNumber, decimal initialBalance, Card card)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
            Card = card;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
            Console.WriteLine($"\tDeposited {amount}. New balance is {Balance}");
        }

        public void Withdraw(decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                Console.WriteLine($"\tWithdrew {amount}. New balance is {Balance}");
            }
            else
            {
                Console.WriteLine($"\tInsufficient funds to withdraw {amount}. Current balance is {Balance}");
            }
        }

        public void Transfer(Account toAccount, decimal amount)
        {
            if (Balance >= amount)
            {
                Balance -= amount;
                toAccount.Deposit(amount);
                Console.WriteLine($"\tTransferred {amount} to {toAccount.AccountNumber}. New balance is {Balance}");
            }
            else
            {
                Console.WriteLine($"\tInsufficient funds to transfer {amount}. Current balance is {Balance}");
            }
        }

        public Memento SaveState()
        {
            return new Memento(AccountNumber, Balance);
        }

        public void PrintAccountInfo()
        {
            Console.WriteLine($"Account Name: {AccountNumber}");
            Console.WriteLine($"Card Number: {Card.CardNumber}");
            Console.WriteLine($"Balance: {Balance:C}");
        }

        public void RestoreState(Memento memento)
        {
            Balance = memento.Balance;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = HASH;
                hash = hash * MultiplicativeCoefficient + (AccountNumber?.GetHashCode() ?? 0);
                hash = hash * MultiplicativeCoefficient + Balance.GetHashCode();
                hash = hash * MultiplicativeCoefficient + (Card?.GetHashCode() ?? 0);
                return hash;
            }
        }
    }
}
