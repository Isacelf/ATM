// Isac Elfstrand SUT23
using System;

namespace ATM
{
    internal class Program
    {
        static bool loggedIn = false;
        static double[] balances = { 2000, 320.45, 160.8, 790, 577.8, 7000, 456.3, 6000, 731.78, 2500 };

        static void Main(string[] args)
        {
            string[] usernames = { "admin", "user1", "user2", "user3", "user4" };
            string[] pins = { "2000", "7777", "1555", "1234", "9898" };
            string[] accountTypes = { "Save Account", "Pay Account" };
            Console.WriteLine("Hello and welcome to the bank!");

            int attempts = 3;
            var username = "";
            var pin = "";
            int currentUserIndex = -1;

            while (true)
            {
                while (attempts > 0 && !loggedIn)
                {
                    Console.WriteLine("Please enter your username: ");
                    username = Console.ReadLine().ToLower();
                    Console.WriteLine("Please enter your PIN Code: ");
                    pin = Console.ReadLine();

                    currentUserIndex = LogIn(username, pin, usernames, pins);
                    if (currentUserIndex >= 0)
                    {
                        Console.WriteLine("Login Succesful");
                        loggedIn = true;
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        attempts--;
                        Console.WriteLine($"Login failed, you have [{attempts}] attempts left. ");
                    }
                    if (attempts == 0)
                    {
                        Console.WriteLine("Too many failed login attempts");
                        break;
                    }
                }
                while (loggedIn)
                {
                    Console.WriteLine("Choose from the menu:");
                    Console.WriteLine("[1] Accounts & Balance");
                    Console.WriteLine("[2] Transfer money");
                    Console.WriteLine("[3] Withdraw money");
                    Console.WriteLine("[4] Log out");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Accounts(username, accountTypes, currentUserIndex, balances);
                            break;
                        case 2:
                            TransferMoney(username, accountTypes, currentUserIndex, balances);
                            break;
                        case 3:
                            WithdrawMoney(username, accountTypes, currentUserIndex, balances, pins);
                            break;
                        case 4:
                            LogOut();
                            break;
                        default:
                            Console.WriteLine("Error, wrong input.");
                            continue;
                    }
                    Console.WriteLine("Press Enter to return to the main menu.");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }


        static int LogIn(string username, string pin, string[] usernames, string[] pins)
        {
            for (int i = 0; i < usernames.Length; i++)
            {
                if (username == usernames[i] && pin == pins[i])
                {
                    return i;
                }
            }
            return -1;
        }

        static void Accounts(string username, string[] accountTypes, int currentUserIndex, double[] balances)
        {
            Console.WriteLine($"Here are your accounts, {username}:");
            for (int i = 0; i < accountTypes.Length; i++)
            {
                Console.WriteLine($"{accountTypes[i]}: {balances[currentUserIndex * accountTypes.Length + i]}$");
            }
        }

        static void TransferMoney(string username, string[] accountTypes, int currentUserIndex, double[] balances)
        {
            if (currentUserIndex >= 0)
            {
                int userBalanceIndex = currentUserIndex * accountTypes.Length;

                Console.WriteLine("Your accounts:");
                for (int i = 0; i < accountTypes.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {accountTypes[i]}: {balances[userBalanceIndex + i]}$");
                }

                Console.Write("Select the account to transfer from (Enter the number): ");
                int sourceAccountIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                if (sourceAccountIndex >= 0 && sourceAccountIndex < accountTypes.Length)
                {
                    Console.Write("Enter the amount to transfer: ");
                    double amountToTransfer = Convert.ToDouble(Console.ReadLine());

                    if (amountToTransfer <= balances[userBalanceIndex + sourceAccountIndex])
                    {
                        Console.WriteLine("Your account for transfer:");
                        for (int i = 0; i < accountTypes.Length; i++)
                        {
                            if (i != sourceAccountIndex)
                            {
                                Console.WriteLine($"{i + 1}. {accountTypes[i]}");
                            }
                        }
                        Console.Write("Select the account to transfer to (Enter the number): ");
                        int targetAccountIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                        if (targetAccountIndex >= 0 && targetAccountIndex < accountTypes.Length)
                        {
                            balances[userBalanceIndex + sourceAccountIndex] -= amountToTransfer;
                            balances[userBalanceIndex + targetAccountIndex] += amountToTransfer;

                            Console.WriteLine($"Transfer of {amountToTransfer}$ from {accountTypes[sourceAccountIndex]} to {accountTypes[targetAccountIndex]} completed.");

                            Console.WriteLine("Your updated balances:");
                            for (int i = 0; i < accountTypes.Length; i++)
                            {
                                Console.WriteLine($"{accountTypes[i]}: {balances[userBalanceIndex + i]}$");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid target account selection.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"You do not have enough funds in your {accountTypes[sourceAccountIndex]} account.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid source account selection.");
                }
            }
            else
            {
                Console.WriteLine("No accounts found for this user.");
            }
        }

    static void WithdrawMoney(string username, string[] accountTypes, int currentUserIndex, double[] balances, string[] pins)
        {
            if (currentUserIndex >= 0)
            {
                int userBalanceIndex = currentUserIndex * accountTypes.Length;

                Console.WriteLine("Choose the account to withdraw from:");
                for (int i = 0; i < accountTypes.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {accountTypes[i]}");
                }
                int selectedAccountIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                if (selectedAccountIndex >= 0 && selectedAccountIndex < accountTypes.Length)
                {
                    Console.Write($"Enter the amount to withdraw from {accountTypes[selectedAccountIndex]} account: ");
                    double amountToWithdraw = Convert.ToDouble(Console.ReadLine());

                    Console.WriteLine("Please enter your PIN Code to confirm the withdrawal: ");
                    string enteredPin = Console.ReadLine();

                    if (enteredPin == pins[currentUserIndex])
                    {
                        if (amountToWithdraw <= balances[userBalanceIndex + selectedAccountIndex])
                        {
                            balances[userBalanceIndex + selectedAccountIndex] -= amountToWithdraw;
                            Console.WriteLine($"You have withdrawn {amountToWithdraw}$ from {accountTypes[selectedAccountIndex]} account.");
                            Console.WriteLine($"Your updated balance for {accountTypes[selectedAccountIndex]}: {balances[userBalanceIndex + selectedAccountIndex]}$");

                        }
                        else
                        {
                            Console.WriteLine($"You do not have enough funds in your {accountTypes[selectedAccountIndex]} account.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect PIN. Withdrawal canceled.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid account selection.");
                }
            }
            else
            {
                Console.WriteLine("No accounts found for this user.");
            }
        }

        static void LogOut()
        {
            Console.WriteLine("You are logged out");
            loggedIn = false;
        }  
    }
}  
