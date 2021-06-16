using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    class PostManager : IUserInterfaceManager
    {

        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        ///TODO: hood up blog
        /// private BlogRepository _blogRepository;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            // _blogRepository = new BlogRepository(connectionString);
        }


        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine("1) List Posts");
            Console.WriteLine("2) Add Post");
            Console.WriteLine("3) Edit Post");
            Console.WriteLine("4) Remove Post");
            Console.WriteLine("5) Note Management");
            Console.WriteLine("0) Return to Main Menu");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ListAll();
                    return this;
                case "2":
                    AddPost();
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

            throw new NotImplementedException();
        }



        public void AddPost()
        {
            Console.Write("Post Title: ");
            string title = Console.ReadLine();
            Console.Write("Url to post: ");
            string url = Console.ReadLine();
            // Add logic to catch ID's out of range
            Console.Write("Author ID: ");
            int authorID = int.Parse(Console.ReadLine());
            Console.Write("Blog ID: ");
            int blogId = int.Parse(Console.ReadLine());
            Post post = new Post()
            {
                Title = title,
                Url = url,
                Author = _authorRepository.Get(authorID),
                PublishDateTime = DateTime.Now,
                //Blog = _blogRepository.get(blogId)

            };
            _postRepository.Insert(post);
        }

        public void ListAll()
        {
            foreach (Post p in _postRepository.GetAll())
            {
                Console.WriteLine($"{p.Title} {p.Url}");
            }
        }
    }
}
