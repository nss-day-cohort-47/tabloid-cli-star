using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    public class BlogRepository : DatabaseConnector, IRepository<Blog>
    {
        public BlogRepository(string connectionString) : base(connectionString) { }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Blog Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Blog> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    List<Blog> blogs = new List<Blog>() { };
                    cmd.CommandText = @"Select Title, Id, Url From Blog";
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Blog blog = new Blog()
                        {
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                         Url = reader.GetString(reader.GetOrdinal("Url")),
                         Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    };
                    blogs.Add(blog);
                }
                reader.Close();
                return blogs;
            }
        }
    }


    public void Insert(Blog entry)
    {
        using (SqlConnection conn = Connection)
        {
            conn.Open();
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO Blog (Title, Url)
                                            OUTPUT INSERTED.Id
                                            VALUES (@title, @Url)";
                cmd.Parameters.AddWithValue("@title", entry.Title);
                cmd.Parameters.AddWithValue("@Url", entry.Url);
                int id = (int)cmd.ExecuteScalar();

                entry.Id = id;
            }
        }
    }

    public void Update(Blog entry)
    {
        throw new NotImplementedException();
    }
}
}
