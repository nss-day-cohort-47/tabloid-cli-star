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
        private string _connectionString;
        private AuthorRepository _authorRepository;

        private BlogRepository _blogRepository;
        private string _connectionString;


        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);

            _connectionString =  connectionString;
          

            _blogRepository = new BlogRepository(connectionString);

        }


        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine("1) List Posts");
            Console.WriteLine("2) Add Post");
            Console.WriteLine("3) Edit Post");
            Console.WriteLine("4) Remove Post");
            Console.WriteLine("5) Note Management");
            Console.WriteLine("6) Detail Management");
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
                    EditPost();
                    return this;
                case "4":
                    Remove();
                    return this;
                case "5":

                    throw new NotImplementedException();
                    break;
                case "6":
                    Post post = Choose();
                    if (post == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new PostDetailManager(this, _connectionString, post.Id);
                    }

                    return new NoteManager(this, _connectionString);

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
                Blog = _blogRepository.Get(blogId)

            };
            _postRepository.Insert(post);
        }

        public void ListAll()
        {
            foreach (Post p in _postRepository.GetAll())
            {
                Console.WriteLine($"{p.Id}. {p.Title} {p.Url}");
            }
        }

        private void EditPost()
        {
            ListAll();
            Console.Write("Enter Id of Post to Edit: ");

            int postSelect;
            while (!int.TryParse(Console.ReadLine(), out postSelect))
            {
                Console.Write("Enter a Number only of the post to edit: ");
            }
            if (!string.IsNullOrWhiteSpace(postSelect.ToString()))
            {
                Post post = null;
                try
                {
                    post = _postRepository.Get(postSelect);
                    if (!string.IsNullOrEmpty(post.Title))
                    {
                        Console.WriteLine($"Title: {post.Title}");
                        Console.Write("Enter New Title or Press enter: ");
                        string newTitle = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newTitle))
                        {
                            post.Title = newTitle;
                        }
                        Console.WriteLine($"URL: {post.Url}");
                        Console.Write("Enter New URL or Press enter: ");
                        string newUrl = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newUrl))
                        {
                            post.Url = newUrl;
                        }

                        Console.WriteLine($"Blog ID: {post.Blog.Id}");
                        Console.Write("Enter New blog or Press enter: ");
                        int blogId;
                        while (!int.TryParse(Console.ReadLine(), out blogId) && string.IsNullOrWhiteSpace(blogId.ToString()))
                        {
                            Console.Write("Please enter a number or press enter to skip: ");
                        }
                        Console.WriteLine($"Author ID: {post.Author.Id}");
                        Console.Write("Enter New Author or Press enter: ");
                        int authorId;
                        while (!int.TryParse(Console.ReadLine(), out authorId) && string.IsNullOrWhiteSpace(authorId.ToString()))
                        {
                            Console.Write("Please eneter a number or press enter to skip: ");
                        }

                        _postRepository.Update(post);
                    }
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine("Edit post Not successfull, please try again");
                }
            }

        }
        private Post Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Post:";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }


        private void Remove()
        {
            Post postToDelete = Choose("Which post would you like to remove?");
            if (postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
            }
        }

    }
}
