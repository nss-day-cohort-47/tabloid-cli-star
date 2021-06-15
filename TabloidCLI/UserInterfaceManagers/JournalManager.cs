using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    class JournalManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;

        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
        }


        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine("1) List Journals");
            Console.WriteLine("2) Add Journal");
            Console.WriteLine("3) Edit Journal");
            Console.WriteLine("4) Remove Journal");
            Console.WriteLine("5) Note Management");
            Console.WriteLine("0) Return to Main Menu");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    throw new NotImplementedException();
                    break;
                case "2":
                    AddJournalEntry();
                    break;
                case "3":
                    throw new NotImplementedException();
                    break;
                case "4":
                    throw new NotImplementedException();
                    break;
                case "5":
                    throw new NotImplementedException();
                    break;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    break;
            }

            throw new NotImplementedException();
        }

        public void AddJournalEntry()
        {
            Console.Write("What is the title of this entry? ");
            string Title = Console.ReadLine();

            Console.Write("What's on your mind? ");
            string Content = Console.ReadLine();

            Journal journal = new Journal(Title, Content, DateTime.Now);
            _journalRepository.Insert(journal);
        }

    }
}
