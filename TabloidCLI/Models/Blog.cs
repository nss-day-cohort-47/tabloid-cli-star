using System.Collections.Generic;

namespace TabloidCLI.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();

        public override string ToString()
        {
            return $"{Title} ({Url})";
        }

        //public Blog(string Title, string Url)
        //{
        //    this.Title = Title;
        //    this.Url = Url;
        //}

        //public Blog(string title, string url, int Id)
        //{
        //    Title = title;
        //    Url = url;
        //}
    }
}