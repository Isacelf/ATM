// Isac Elfstrand SUT23
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

        }
    }
}