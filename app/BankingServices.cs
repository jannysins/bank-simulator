using System;

namespace ATMApp.Services
{
    public static class BankingServices
    {
        // Option 1: Pass-by-value (Check Balance)
        public static double GetBalance(double balance)
        {
            return balance;
        }

        // Option 2: ref (Deposit)
        public static bool Deposit(ref double balance, double amount)
        {
            if (amount > 0)
            {
                balance += amount;
                return true;
            }
            return false;
        }

        // Option 3: ref + out (Withdraw)
        public static void Withdraw(ref double balance, double amount, out bool isSuccessful)
        {
            if (amount > 0 && balance >= amount)
            {
                balance -= amount;
                isSuccessful = true;
            }
            else
            {
                isSuccessful = false;
            }
        }
    }
}
