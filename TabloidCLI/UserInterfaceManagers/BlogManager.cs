using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    class BlogManager : IUserInterfaceManager
    {

        private readonly IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public BlogManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }



        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Blog Menu");
            Console.WriteLine("1) List Blogs");
            Console.WriteLine("2) Add Blog");
            Console.WriteLine("3) Edit Blog");
            Console.WriteLine("4) Remove Blog");
            Console.WriteLine("5) Note Management");
            Console.WriteLine("0) Return to Main Menu");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    DisplayAllBlogs();
                    return this;
                case "2":
                    AddBlogPost();
                    return this;
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

            return this;
        }

        public void DisplayAllBlogs()
        {
            foreach (Blog b in _blogRepository.GetAll())
            {
                Console.WriteLine($"{b.Title} {b.Url}");
            }
        }

        public void AddBlogPost()
        {
            Console.Write("Name this Blog post! ");
            string Title = Console.ReadLine();

            Console.Write("Insert URL here: ");
            string Url = Console.ReadLine();

            Blog blog = new Blog() {Title = Title, Url = Url};
            _blogRepository.Insert(blog);
        }
    }
}

