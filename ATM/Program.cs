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

        }

        static void TransferMoney(string username, string[] accountTypes, int currentUserIndex, double[] balances)
        {

        }

        static void WithdrawMoney(string username, string[] accountTypes, int currentUserIndex, double[] balances, string[] pins)
        {

        }

        static void LogOut()
        {

        }






        }
    }
}  
