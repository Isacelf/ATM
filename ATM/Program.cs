// Isac Elfstrand SUT23
using System;

namespace ATM
{
    internal class Program
    {
        static bool loggedIn = false; // Variabel för att hålla reda på om användaren är inloggad
        static double[] balances = { 2000, 320.45, 160.8, 790, 577.8, 7000, 456.3, 6000, 731.78, 2500 }; // Array som håller användarnas kontosaldo


        static void Main(string[] args)
        {
            string[] usernames = { "admin", "user1", "user2", "user3", "user4" }; // Array som håller användarnamn
            string[] pins = { "2000", "7777", "1555", "1234", "9898" }; // Array som håller pinkoder
            string[] accountTypes = { "Save Account", "Pay Account" }; // Array för kontotyperna
            Console.WriteLine("Hello and welcome to the bank!"); // Välkomnstmeddelandet för programmet

            int attempts = 3; // Antal inloggsningsförsök
            var username = ""; // Tom variabel som väntar på användarens inmatning senare i koden.
            var pin = ""; // Tom variabel som väntar på användarens inmatning senare i koden.
            int currentUserIndex = -1; // "Index" för den aktuella/inloggade användaren.

            while (true) // While loop som kommer köras till den bryts manuellt.
            {
                while (attempts > 0 && !loggedIn) // Körs så länge det finns "attempts" kvar samt att användaren inte är inloggad.
                {
                    Console.WriteLine("Please enter your username: ");
                    username = Console.ReadLine().ToLower(); // Här sparas användarnamnet ner
                    Console.WriteLine("Please enter your PIN Code: ");
                    pin = Console.ReadLine(); // Här sparas pin-koden ner

                    currentUserIndex = LogIn(username, pin, usernames, pins); // Anropar funktionen LogIn och kollar om de inmatade svaren(username, pin) finns i de två listorna(usernames, pins)
                    if (currentUserIndex >= 0) // Om det retunerar större eller lika med 0 så loggar den in. Användaren hittades i listorna.
                    {
                        Console.WriteLine("Login Succesful");
                        loggedIn = true; // Inloggad
                        Console.Clear();
                    }
                    else // Vid misslyckad inloggning, körs den här
                    {
                        Console.Clear();
                        attempts--; // Tar bort ett "attempt"
                        Console.WriteLine($"Login failed, you have [{attempts}] attempts left. ");
                    }
                    if (attempts == 0) // Om "attempts" är 0 och alla försök är slut med inloggningen så avslutas programmet
                    {
                        Console.WriteLine("Too many failed login attempts");
                        break;
                    }
                }
                while (loggedIn) // Om du är inloggad skrivs huvudmenyn ut
                {
                    Console.WriteLine("Choose from the menu:");
                    Console.WriteLine("[1] Accounts & Balance");
                    Console.WriteLine("[2] Transfer money");
                    Console.WriteLine("[3] Withdraw money");
                    Console.WriteLine("[4] Log out");
                    int choice = Convert.ToInt32(Console.ReadLine()); // Sparar ner valet man gör i variablen "choice".

                    switch (choice) // Beroende på användarens val så anropar den motsvarande funktion.
                    {
                        case 1:
                            Accounts(username, accountTypes, currentUserIndex, balances); // Account funktionen anropas.
                            break;
                        case 2:
                            TransferMoney(username, accountTypes, currentUserIndex, balances); // TransferMoney funktionen anropas.
                            break;
                        case 3:
                            WithdrawMoney(username, accountTypes, currentUserIndex, balances, pins); // WithdrawMoney funktionen anropas.
                            break;
                        case 4:
                            LogOut(); // LogOut funktionen anropas
                            break;
                        default: // Körs om inmatningen inte matchar något av valen i huvudmenyn.
                            Console.WriteLine("Error, wrong input.");
                            continue; // Användaren får en ny chans att göra ett val istället för att det crashar/blir utskickad.
                    }
                    Console.WriteLine("Press Enter to return to the main menu.");
                    Console.ReadLine(); // Väntar på Enter knapptryck för att fortsätta.
                    Console.Clear();
                }
            }
        }


        static int LogIn(string username, string pin, string[] usernames, string[] pins) // Logga in funktion
        {
            for (int i = 0; i < usernames.Length; i++) // For loop som kollar listan av användarnamn.
            {
                if (username == usernames[i] && pin == pins[i]) // If sats som jämför om de inmatade username samt pin finns i usernames och pins. 
                {
                    return i; // Om matchning hittas så retuneras användarens index.
                }
            }
            return -1; // Om det inte hittas så retuneras det -1.
        }

        static void Accounts(string username, string[] accountTypes, int currentUserIndex, double[] balances) // Funktion för att visa användarens konto samt saldo.
        {
            Console.WriteLine($"Here are your accounts, {username}:");
            for (int i = 0; i < accountTypes.Length; i++) // For loop som kör igenom listan "accountTypes" för att hitta kontotyperna som användaren har.
            {
                Console.WriteLine($"{accountTypes[i]}: {balances[currentUserIndex * accountTypes.Length + i]}$"); // Skriver ut information om varje konto samt saldot i kontot.
            }
        }

        static void TransferMoney(string username, string[] accountTypes, int currentUserIndex, double[] balances) // Funktion för att överföra pengar mellan konton.
        {
            if (currentUserIndex >= 0) // Vilkor som kontrollerar om en användare är inloggad eller inte, är det större eller lika med 0 så är en gilgig användare inloggad.
            {
                int userBalanceIndex = currentUserIndex * accountTypes.Length; // Beräknar indexet i användarens saldo i "balances arrayen".

                Console.WriteLine("Your accounts:");
                for (int i = 0; i < accountTypes.Length; i++) // Körs för att kolla vilka olika kontotyper användaren har.
                {
                    Console.WriteLine($"{i + 1}. {accountTypes[i]}: {balances[userBalanceIndex + i]}$"); // Loopen visar vilka konton användaren har och saldot i dem.
                }

                Console.Write("Select the account to transfer from (Enter the number): ");
                int sourceAccountIndex = Convert.ToInt32(Console.ReadLine()) - 1; // Sparar ner inmatningen i "sourceAccountIndex"dvs källan, måste ta bort -1 då kontonumret är 1 men arrayer använder basindex 0.

                if (sourceAccountIndex >= 0 && sourceAccountIndex < accountTypes.Length) // Kontrollerar om det valda kontot är giltigt genom index.
                {
                    Console.Write("Enter the amount to transfer: ");
                    double amountToTransfer = Convert.ToDouble(Console.ReadLine()); // Sparar ner inmatning som ska överföras, konverteras till double för att kunna ta decimaler.

                    if (amountToTransfer <= balances[userBalanceIndex + sourceAccountIndex]) // Kontrollerar om det finns tillräckligt med saldo
                    {
                        Console.WriteLine("Your account for transfer:");
                        for (int i = 0; i < accountTypes.Length; i++) // Visar de konton som finns att överföra till.
                        {
                            if (i != sourceAccountIndex) // Villkor som kollar så inte kontoindex är samma som sourceAccountIndex.
                            {
                                Console.WriteLine($"{i + 1}. {accountTypes[i]}");
                            }
                        }
                        Console.Write("Select the account to transfer to (Enter the number): ");
                        int targetAccountIndex = Convert.ToInt32(Console.ReadLine()) - 1; // Sparar ner inmatningen i "targetAccountIndex" dvs mottagare, måste ta bort -1 då kontonumret är 1 men arrayer använder basindex 0.

                        if (targetAccountIndex >= 0 && targetAccountIndex < accountTypes.Length) // Kontrollerar om det valda kontot är giltigt, för att se om index är större eller lika med 0
                        {
                            balances[userBalanceIndex + sourceAccountIndex] -= amountToTransfer; // Här genomförs överföringen om alla villkor uppfylls.
                            balances[userBalanceIndex + targetAccountIndex] += amountToTransfer; 

                            Console.WriteLine($"Transfer of {amountToTransfer}$ from {accountTypes[sourceAccountIndex]} to {accountTypes[targetAccountIndex]} completed.");

                            Console.WriteLine("Your updated balances:");
                            for (int i = 0; i < accountTypes.Length; i++) 
                            {
                                Console.WriteLine($"{accountTypes[i]}: {balances[userBalanceIndex + i]}$"); // Skriver ut kontot samt saldot som användaren har och sammanfattar efter överföringen
                            }
                        }
                        else // Hela sista delen av funktionen hanterar olika scenarior vid fel med källkonto, mottagarkonto eller överförningsbelopp.
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

    static void WithdrawMoney(string username, string[] accountTypes, int currentUserIndex, double[] balances, string[] pins) // Funktion för att ta ut pengar från konto
        {
            if (currentUserIndex >= 0) // Kontrollerar så att användaren är inloggad.
            {
                int userBalanceIndex = currentUserIndex * accountTypes.Length; // Beräknar indexet i användarens saldo i "balances arrayen".

                Console.WriteLine("Choose the account to withdraw from:");
                for (int i = 0; i < accountTypes.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {accountTypes[i]}"); // Skriver ut lista över användarens konto och ber användaren välja från vilket konto.
                }
                int selectedAccountIndex = Convert.ToInt32(Console.ReadLine()) - 1; // Sparar ner inmatningen i "selectedAccountIndex", måste ta bort -1 då kontonumret är 1 men arrayer använder basindex 0.

                if (selectedAccountIndex >= 0 && selectedAccountIndex < accountTypes.Length) // Kontrollerar om det valda kontot är giltigt.
                {
                    Console.Write($"Enter the amount to withdraw from {accountTypes[selectedAccountIndex]} account: "); 
                    double amountToWithdraw = Convert.ToDouble(Console.ReadLine()); // Summan som ska tas ut, konveteras till double för att fungera med decimaler.

                    Console.WriteLine("Please enter your PIN Code to confirm the withdrawal: ");
                    string enteredPin = Console.ReadLine(); // Ombeds att skriva in pin-koden för att bekräfta uttaget.

                    if (enteredPin == pins[currentUserIndex]) // Kontrollerar om pinkoden stämmer överens med den sparade pinkoden.
                    {
                        if (amountToWithdraw <= balances[userBalanceIndex + selectedAccountIndex]) // Kontrollerar om användaren har tillräckligt med pengar.
                        {
                            balances[userBalanceIndex + selectedAccountIndex] -= amountToWithdraw; // Uttaget utförs här om alla vilkor uppfylls.
                            Console.WriteLine($"You have withdrawn {amountToWithdraw}$ from {accountTypes[selectedAccountIndex]} account."); // Sammanfattning som skriver hur mycket som är uttaget och från vilket konto.
                            Console.WriteLine($"Your updated balance for {accountTypes[selectedAccountIndex]}: {balances[userBalanceIndex + selectedAccountIndex]}$"); // Sammanfattning som visar saldot i kontorna efter uttaget.

                        }
                        else // Den sista delen av koden i funktionen hanterar olika scenarior vid fel med uttag, inte tillräckligt med pengar, fel pin-kod, fel konto med mera.
                        {
                            Console.WriteLine($"You do not have enough funds in your {accountTypes[selectedAccountIndex]}.");
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

        static void LogOut() // Funktion för att logga ut användaren
        {
            Console.WriteLine("You are logged out");
            loggedIn = false; // Sätter variablen loggedIn till false
        }  
    }
}  
