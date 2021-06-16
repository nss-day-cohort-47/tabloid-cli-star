using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers 
{
    class SettingsManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        //private SettingsRepository _settingsRepository;
        private string _connectionString;

        public SettingsManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            //_authorRepository = new AuthorRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Settings Menu");
            Console.WriteLine(" 1) Change Background Color");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Colorize();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void Colorize()
        {
            Console.WriteLine("Choose a color for the background:");
            Console.WriteLine("");
            Console.WriteLine("(1)-Blueberry Blue");
            Console.WriteLine("(2)-Rellow Red");
            Console.WriteLine("(3)-Gargbage Green");
            Console.WriteLine("(4)-Dull Grey");
            Console.WriteLine("(5)-Back in Black");
            Console.Write("Enter the number choice of your selection: ");
            string selection = Console.ReadLine();
            if (selection == "1")
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Clear();
            }
            else if (selection == "2")
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Clear();
            }
            else if (selection == "3")
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Clear();
            }
           else if (selection == "4")
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();
            }
            else if (selection == "5")
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Selection Invalid");
            }
        }


       
    }
}
