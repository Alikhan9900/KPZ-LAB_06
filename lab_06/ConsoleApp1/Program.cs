using ConsoleApp1;
using System;

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

                int choice;

                do
                {
                    // Display menu for user to choose operation
                    Console.WriteLine("\nChoose an operation:");
                    Console.WriteLine("1. Deposit");
                    Console.WriteLine("2. Withdraw");
                    Console.WriteLine("3. Check Balance");
                    Console.WriteLine("4. Transfer");
                    Console.WriteLine("5. Account info");
                    Console.WriteLine("6. Exit");

                    // Read user's choice
                    Console.Write("\nOption: ");
                    choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Deposit Card №1:");
                            Account depositAccount = atm.GetAccount(card1);
                            if (depositAccount == null)
                            {
                                Console.WriteLine("Account not found.");
                                break;
                            }
                            Console.Write("Enter amount to deposit: ");
                            decimal depositAmount = decimal.Parse(Console.ReadLine());
                            ICommand deposit = new DepositCommand(atm, depositAccount.Card, depositAmount);
                            deposit.Execute();
                            Console.WriteLine("Deposit executed.");
                            break;
                        case 2:
                            Console.WriteLine("Withdraw from Card №2:");
                            Account withdrawAccount = atm.GetAccount(card2);
                            if (withdrawAccount == null)
                            {
                                Console.WriteLine("Account not found.");
                                break;
                            }
                            Console.Write("Enter amount to withdraw\n" +
                                "\t(The amount must be a multiple of 200): ");
                            decimal withdrawAmount = decimal.Parse(Console.ReadLine());

                            // The amount must be a multiple of 200
                            if (withdrawAmount % 200 != 0)
                            {
                                Console.WriteLine("The amount must be a multiple of 200!");
                                break;
                            }
                            ICommand withdraw = new WithdrawCommand(atm, withdrawAccount.Card, withdrawAmount);
                            withdraw.Execute();
                            Console.WriteLine("Withdraw executed.");

                            break;
                        case 3:
                            Console.WriteLine("Check Card №1 Balance:");
                            Account balanceCheckAccount = atm.GetAccount(card1);
                            if (balanceCheckAccount != null)
                            {
                                ICommand balanceCheck = new BalanceCheckCommand(atm, balanceCheckAccount.Card);
                                balanceCheck.Execute();
                                Console.WriteLine("Balance check executed.");
                            }
                            Console.WriteLine("Check Card №2 Balance:");
                            Account balanceCheckAccount2 = atm.GetAccount(card2);
                            if (balanceCheckAccount2 == null)
                            {
                                Console.WriteLine("Account not found."); break;
                            }
                            ICommand balanceCheck2 = new BalanceCheckCommand(atm, balanceCheckAccount2.Card);
                            balanceCheck2.Execute();
                            Console.WriteLine("Balance check executed.");
                            break;
                        case 4:
                            Console.WriteLine("Transfer from Card 1 to Card 2:");
                            Account transferFromAccount = atm.GetAccount(card1);
                            if (transferFromAccount == null)
                            {
                                Console.WriteLine("Source account not found.");
                                break;
                            }

                            Account transferToAccount = atm.GetAccount(card2);
                            if (transferToAccount == null)
                            {
                                Console.WriteLine("Destination account not found.");
                                break;
                            }
                            Console.Write("Enter amount to transfer: ");
                            decimal transferAmount = decimal.Parse(Console.ReadLine());
                            ICommand transfer = new TransferCommand(atm, transferFromAccount.Card, transferToAccount.Card, transferAmount);
                            transfer.Execute();
                            Console.WriteLine("Transfer executed.");
                            break;
                        case 5:
                            Console.WriteLine("Account Information:");
                            Console.WriteLine("1. Account 1");
                            Console.WriteLine("2. Account 2");
                            Console.Write("\nSelect account: ");
                            int accountChoice = int.Parse(Console.ReadLine());

                            switch (accountChoice)
                            {
                                case 1:
                                    Console.WriteLine("Account 1 Information:");
                                    account1.PrintAccountInfo();
                                    break;
                                case 2:
                                    Console.WriteLine("Account 2 Information:");
                                    account2.PrintAccountInfo();
                                    break;
                                default:
                                    Console.WriteLine("Invalid account choice.");
                                    break;
                            }
                            break;
                        case 6:
                            Console.WriteLine("Exiting...");
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                } while (choice != 6);

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
