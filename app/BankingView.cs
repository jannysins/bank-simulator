using System;
using ATMApp.Services; // IMPORTANT: This line connects View to Services

namespace ATMApp.View
{
    public static class BankingView
    {
        public static void Run()
        {
            Console.WriteLine("=== Simple ATM System ===");

            double balance = 1000.00;
            double lastTransaction = 0.00;

            Console.WriteLine($"Initial balance: ₱{balance:N2}");
            Console.WriteLine("--------------------------------------");

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n=== ATM MACHINE ===");
                Console.WriteLine("1: Check Balance");
                Console.WriteLine("2: Deposit Money");
                Console.WriteLine("3: Withdraw Money");
                Console.WriteLine("4: Print Mini Statement");
                Console.WriteLine("5: Exit");
                Console.Write("Select option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": // Check Balance
                        double currentBal = BankingServices.GetBalance(balance);
                        Console.WriteLine($"Current Balance: ₱{currentBal:N2}");
                        break;

                    case "2": // Deposit
                        Console.Write("Enter Amount to Deposit: ");
                        if (double.TryParse(Console.ReadLine(), out double depAmount))
                        {
                            if (depAmount <= 0)
                            {
                                Console.WriteLine("Invalid deposit amount. Please enter a positive value.");
                                continue;
                            }

                            // Pass-by-ref
                            bool depSuccess = BankingServices.Deposit(ref balance, depAmount);

                            if (depSuccess)
                            {
                                lastTransaction = depAmount;
                                Console.WriteLine("Deposit successful.");
                                Console.WriteLine($"Updated Balance: ₱{balance:N2}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a number.");
                        }
                        break;

                    case "3": // Withdraw
                        Console.Write("Enter amount to withdraw: ");
                        if (double.TryParse(Console.ReadLine(), out double withAmount))
                        {
                            if (withAmount <= 0)
                            {
                                Console.WriteLine("Invalid withdrawal amount. Please enter a positive value.");
                                continue;
                            }

                            // Pass-by-ref AND out
                            bool isSuccess;
                            BankingServices.Withdraw(ref balance, withAmount, out isSuccess);

                            if (isSuccess)
                            {
                                lastTransaction = -withAmount;
                                Console.WriteLine("Withdrawal successful.");
                                Console.WriteLine($"Updated Balance: ₱{balance:N2}");
                            }
                            else
                            {
                                Console.WriteLine("Withdrawal failed. Insufficient balance.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please enter a number.");
                        }
                        break;

                    case "4": // Mini Statement
                        Console.WriteLine("--- Mini Statement ---");
                        Console.WriteLine($"Current Balance: ₱{balance:N2}");
                        Console.WriteLine($"Last Transaction Amount: ₱{lastTransaction:N2}");
                        break;

                    case "5": // Exit
                        Console.WriteLine("Thank you for using the ATM. Goodbye!");
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option selected. Please try again.");
                        break;
                }
            }
        }
    }
}
