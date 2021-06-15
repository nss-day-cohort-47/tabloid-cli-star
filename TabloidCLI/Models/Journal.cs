using System;
using System.Collections.Generic;

namespace TabloidCLI.Models
{
    public class Journal
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDateTime { get; }

        public override string ToString()
        {
            return $"{Title} {CreateDateTime}";
        }

        public Journal(string Title, string Content, DateTime CreateDateTime)
        {
            this.Title = Title;
            this.Content = Content;
            this.CreateDateTime = CreateDateTime;
        }

        public Journal(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }
}
