using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    public class NoteRepository : DatabaseConnector, IRepository<Note>
    {
        public NoteRepository(string connectionString) : base(connectionString) { }

        public Note List(int id)
        {
            throw new NotImplementedException();
        }

        public Note Insert(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Note> GetAll()
        {
            throw new NotImplementedException();
        }

        public Note Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Note entry)
        {
            throw new NotImplementedException();
        }

        public void Update(Note entry)
        {
            throw new NotImplementedException();
        }
    }
}
