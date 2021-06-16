﻿using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    public class PostRepository : DatabaseConnector, IRepository<Post>
    {
        public PostRepository(string connectionString) : base(connectionString) { }

        public List<Post> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select a.FirstName as AuthorFirst,
                                    a.LastName as AuthorLast,
                                    a.Bio as AuthorBio,
                                    a.Id As AuthorId, 
                                    p.Title as PostTitle, 
                                    p.PublishDateTime as PublistDateTime, 
                                    p.Id as PostId, 
                                    p.Url as PostUrl, 
                                    b.Id as BlogId, 
                                    b.Title as BlogTitle,
                                    b.URL as BlogUrl
                                    From Post as p
                                    Join Author as a on p.AuthorId = a.id
                                    Join Blog as b on b.id = p.BlogId;";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Post> posts = new List<Post>() { };
                    while (reader.Read())
                    {
                        Post post = new Post()
                        {
                            Title = reader.GetString(reader.GetOrdinal("PostTitle")),
                            Id = reader.GetInt32(reader.GetOrdinal("PostId")),
                            Url = reader.GetString(reader.GetOrdinal("PostUrl")),
                            PublishDateTime = reader.GetDateTime(reader.GetOrdinal("PublistDateTime")),
                            Author = new Author()
                            {
                                FirstName = reader.GetString(reader.GetOrdinal("AuthorFirst")),
                                LastName = reader.GetString(reader.GetOrdinal("AuthorLast")),
                                Bio = reader.GetString(reader.GetOrdinal("AuthorBio")),
                                Id = reader.GetInt32(reader.GetOrdinal("AuthorId")),
                            },
                            Blog = new Blog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("BlogId")),
                                Title = reader.GetString(reader.GetOrdinal("BlogTitle")),
                                Url = reader.GetString(reader.GetOrdinal("BlogUrl")),
                            },

                        };
                        posts.Add(post);
                    }
                    reader.Close();
                    return posts;
                }
            }
        }

        public Post Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetByAuthor(int authorId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT p.id,
                                               p.Title As PostTitle,
                                               p.URL AS PostUrl,
                                               p.PublishDateTime,
                                               p.AuthorId,
                                               p.BlogId,
                                               a.FirstName,
                                               a.LastName,
                                               a.Bio,
                                               b.Title AS BlogTitle,
                                               b.URL AS BlogUrl
                                          FROM Post p 
                                               LEFT JOIN Author a on p.AuthorId = a.Id
                                               LEFT JOIN Blog b on p.BlogId = b.Id 
                                         WHERE p.AuthorId = @authorId";
                    cmd.Parameters.AddWithValue("@authorId", authorId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Post> posts = new List<Post>();
                    while (reader.Read())
                    {
                        Post post = new Post()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("PostTitle")),
                            Url = reader.GetString(reader.GetOrdinal("PostUrl")),
                            PublishDateTime = reader.GetDateTime(reader.GetOrdinal("PublishDateTime")),
                            Author = new Author()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("AuthorId")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Bio = reader.GetString(reader.GetOrdinal("Bio")),
                            },
                            Blog = new Blog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("BlogId")),
                                Title = reader.GetString(reader.GetOrdinal("BlogTitle")),
                                Url = reader.GetString(reader.GetOrdinal("BlogUrl")),
                            }
                        };
                        posts.Add(post);
                    }

                    reader.Close();

                    return posts;
                }
            }
        }

        public void Insert(Post post)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    try
                    {
                        cmd.CommandText = @"INSERT INTO Post (Title, URL, PublishDateTime, AuthorId, BlogId) OUTPUT INSERTED.Id Values (@title, @url, @publishDateTime, @authorId, @blogId);";
                        cmd.Parameters.AddWithValue("@title", post.Title);
                        cmd.Parameters.AddWithValue("@url", post.Url);
                        cmd.Parameters.AddWithValue("@publishDateTime", post.PublishDateTime);
                        cmd.Parameters.AddWithValue("@authorId", post.Author.Id);
                        cmd.Parameters.AddWithValue("@blogId", 1);

                        post.Id = (int)cmd.ExecuteScalar();
                    }
                    catch (NullReferenceException ex)
                    {
                        Console.WriteLine("A blog value or Author Value was out of range");
                    }


                }
            }
        }

        public void Update(Post post)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
