using System;
using System.Collections.Generic;
using System.Text;

namespace TabloidCLI.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime PublishDateTime { get; set; }
        public Author Author = null;
        public Blog Blog = null;
        public bool IsActive { get; set; }
    }
}
