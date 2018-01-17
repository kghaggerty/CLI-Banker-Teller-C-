using System;

namespace BankTeller
{
    public class MainMenu
    {
        public static int Show()
        {
            Console.Clear();
            Console.WriteLine ("WELCOME TO NASHVILLE SAFE & SOUND BANK");
            Console.WriteLine ("**************************************");
            Console.WriteLine ("1. Create customer account");
            Console.Write ("> ");
            ConsoleKeyInfo enteredKey = Console.ReadKey();
            Console.WriteLine("");
            return int.Parse(enteredKey.KeyChar.ToString());
        }
    }
}