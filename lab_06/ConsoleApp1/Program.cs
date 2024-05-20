﻿using System;

namespace ATMProject
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting ATM operations...");

                // Create ATM with initial cash
                ATM atm = new ATM(10000);

                // Create accounts and cards
                Card card1 = new Card("1234-5678-9012-3456");
                Account account1 = new Account("ACC1", 5000, card1);
                atm.AddAccount(account1);

                Card card2 = new Card("6543-2109-8765-4321");
                Account account2 = new Account("ACC2", 3000, card2);
                atm.AddAccount(account2);

                // Perform operations
                ICommand deposit = new DepositCommand(atm, card1, 1000);
                deposit.Execute();
                Console.WriteLine("Deposit executed.");

                ICommand withdraw = new WithdrawCommand(atm, card1, 500);
                withdraw.Execute();
                Console.WriteLine("Withdraw executed.");

                ICommand balanceCheck = new BalanceCheckCommand(atm, card1);
                balanceCheck.Execute();
                Console.WriteLine("Balance check executed.");

                ICommand transfer = new TransferCommand(atm, card1, card2, 1500);
                transfer.Execute();
                Console.WriteLine("Transfer executed.");

                // Check balance after transfer
                balanceCheck.Execute();
                Console.WriteLine("Balance check after transfer executed.");

                // Simulate out of service
                atm.SetState(new OutOfServiceState(atm));
                deposit.Execute();
                Console.WriteLine("Deposit executed while ATM is out of service.");

                Console.WriteLine("ATM operations completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.ReadLine(); 
        }
    }
}
