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
        private string _connectionString;

        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;
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
                    DisplayAllJournals();
                    return this;
                case "2":
                    AddJournalEntry();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    DeleteJournalEntry();
                    return this;
                case "5":
                    return new NoteManager(this, _connectionString);
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    break;
            }

            throw new NotImplementedException();
        }
        /// <summary>
        /// Adds a journal from a user with title, content and date created.
        /// </summary>
        public void AddJournalEntry()
        {
            Console.Write("What is the title of this entry? ");
            string Title = Console.ReadLine();

            Console.Write("What's on your mind? ");
            string Content = Console.ReadLine();

            Journal journal = new Journal(Title, Content, DateTime.Now);
            _journalRepository.Insert(journal);
        }
        /// <summary>
        /// This list all journals from the database
        /// </summary>
        public void DisplayAllJournals()
        {
            foreach (Journal j in _journalRepository.GetAll())
            {
                Console.WriteLine($"{j.Title} {j.CreateDateTime} {j.Content}");
            }
        }

        private Journal Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a journal:";
            }

            Console.WriteLine(prompt);

            List<Journal> journals = _journalRepository.GetAll();

            for (int i = 0; i < journals.Count; i++)
            {
                Journal journal = journals[i];
                Console.WriteLine($@" {i + 1}) {journal.Title}
{journal.CreateDateTime}
{journal.Content}
");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return journals[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }
        public void DeleteJournalEntry()
        {
            Console.WriteLine(@"
Journal Posts");
            int i = 1;
            //foreach (Journal j in _journalRepository.GetAll())
            //{
            //    Console.WriteLine($"{j.Id}) {j.Title} {j.CreateDateTime} {j.Content}");
            //}
            
            Journal journalToDelete = Choose("Which entry would you like to delete?");
            if (journalToDelete != null)
            {
                _journalRepository.Delete(journalToDelete.Id);
            }

        }

        private void Edit()
        {
            Journal journalToEdit = Choose("Which journal would you like to edit?");
            if (journalToEdit == null)
            {
                return;
            }

            foreach (Journal j in _journalRepository.GetAll())
            {

            }

            Console.WriteLine();
            Console.Write("New title (blank to leave unchanged: ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                journalToEdit.Title = title;
            }
            Console.Write("New Content (blank to leave unchanged: ");
            string content = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(content))
            {
                journalToEdit.Content = content;
            }

            _journalRepository.Update(journalToEdit);
        }
    }
}
