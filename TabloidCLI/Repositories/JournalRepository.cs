using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    public class JournalRepository : DatabaseConnector, IRepository<Journal>
    {

        public JournalRepository(string connectionString) : base(connectionString) { }

        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @$"DELETE FROM Journal WHERE Id = {id};";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Journal Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Journal> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    /// <summary>
                    /// SQL Command used to get all journal related data from the database.  
                    /// </summary>
                    List<Journal> journals = new List<Journal>() { };
                    cmd.CommandText = @"Select Title, Id, CreateDateTime, Content
                                        From Journal";
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Journal journal = new Journal(reader.GetString(reader.GetOrdinal("Title")),
                            reader.GetString(reader.GetOrdinal("Content")),
                            reader.GetDateTime(reader.GetOrdinal("CreateDateTime")));
                        journal.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        journals.Add(journal);
                    }
                    reader.Close();
                    return journals;
                }
            }
        }
        /// <summary>
        ///  Inserts a Journal Entry into the database
        /// </summary>
        /// <param name="entry">
        ///     User Generated Journal Object
        /// </param>
        public void Insert(Journal entry)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Journal (Title, Content, CreateDateTime)
                                            OUTPUT INSERTED.Id
                                            VALUES (@title, @content, @createdatetime)";
                    cmd.Parameters.AddWithValue("@title", entry.Title);
                    cmd.Parameters.AddWithValue("@content", entry.Content);
                    cmd.Parameters.AddWithValue("@createdatetime", entry.CreateDateTime);
                    int id = (int)cmd.ExecuteScalar();

                    entry.Id = id;
                }
            }
        }

        public void Update(Journal entry)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Journal
                                               SET Title = @title,
                                               Content = @content
                                         WHERE id = @id";
                    
                    cmd.Parameters.AddWithValue("@title", entry.Title);
                    cmd.Parameters.AddWithValue("@content", entry.Content);
                    cmd.Parameters.AddWithValue("@id", entry.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}