using System;
using Microsoft.Data.Sqlite;


namespace BankTeller
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the database interface
            DatabaseInterface db = new DatabaseInterface();

            // Check/create the Account table
            db.CheckAccountTable();

            int choice;

            //variable to hold account ID 
            int accountId = -1;

            do
            {
                // Show the main menu by invoking the static method
                choice = MainMenu.Show();

                switch (choice)
                {
                    // Menu option 1: Adding account
                    case 1:
                        // Ask user to input customer name
                        Console.WriteLine("Please enter your full name!");
                        Console.Write(">");
                        string CustomerName = Console.ReadLine();

                        // Insert customer account into database
                        db.Insert($@"
                            INSERT INTO Account
                            (Id, Customer, Balance)
                            VALUES
                            (null, '{CustomerName}', 0)
                        ");
                        break;

                    // Menu option 2: Deposit money
                    case 2:
                        // Logic here
                        //if account ID == -1 / it hasn't been populated yet..then ask for id number
                        if(accountId == -1) 
                        {
                            Console.WriteLine("Enter Account Number Please");
                            Console.Write(">");
                            accountId = int.Parse(Console.ReadLine());
                        }

                        //Ask how much they would like to deposit
                        Console.WriteLine("How much would you like to deposit?");
                        Console.Write(">");
                        //Store deposit amount into a variable
                        double depositAmount = double.Parse(Console.ReadLine());

                        //Enter info into database.  
                        db.Update($@"
                        UPDATE Account
                        SET Balance = Balance + {depositAmount}
                        WHERE id = {accountId}
                        ");
                        break;
                    
                    //Withdraw money option
                    case 3:
                    //Again, ask for id # if it has already been populated
                    if(accountId == -1) 
                        {
                            Console.WriteLine("Enter Account Number Please");
                            Console.Write(">");
                            accountId = int.Parse(Console.ReadLine());
                        }

                        //Ask how much they would like to withdraw
                        Console.WriteLine("How much money would you like to withdraw?");
                        Console.Write(">");
                        //Asign withdraw amount to a variable
                        double withdrawAmount = double.Parse(Console.ReadLine());

                        //Enter info into database
                        db.Update($@"
                        UPDATE Account
                        SET Balance = Balance - {withdrawAmount}
                        WHERE id = {accountId}");
                        break;
                    
                    //Display balance
                    case 4:
                        if(accountId == -1) 
                        {
                            Console.WriteLine("Enter Account Number Please");
                            Console.Write(">");
                            accountId = int.Parse(Console.ReadLine());
                        }

                        //define balance and customer name 
                        double balance = 0;
                        string customerName = "";

                        //Database to query the info
                        db.Query($@"
                        SELECT Balance, Customer FROM Account
                        WHERE Id = {accountId}
                        ", (SqliteDataReader reader) => 
                        {
                            while (reader.Read())
                            {
                                balance = reader.GetDouble(0);
                                customerName = reader.GetString(1);
                            }
                        });

                        Console.WriteLine($"Current Account Balance for {customerName}");
                        Console.WriteLine($"$ {balance}");

                        // pause command line before break statement to display balance
                        Console.WriteLine("Press Enter To Continue");
                        Console.Write(">");
                        Console.ReadLine();
                        break;

                }
            } while (choice != 5);



        }
    }
}
