﻿using Church.Models;
using MongoDB.Driver;
using Church.RepositoryInterfaces;

namespace Church.Repositories
{
    // Visitor Repository
    public class VisitorRepository : IVisitorRepository
    {
        private readonly IMongoCollection<Visitor> _visitors;

        public VisitorRepository(IMongoDatabase database)
        {
            _visitors = database.GetCollection<Visitor>("Visitors");
        }

        public async Task<Visitor> AddVisitor(Visitor visitor)
        {
            await _visitors.InsertOneAsync(visitor);
            return visitor; // Return the visitor after adding it
        }

        public async Task<Visitor> UpdateVisitor(Visitor visitor)
        {
            await _visitors.ReplaceOneAsync(v => v.Id == visitor.Id, visitor);
            return visitor; // Return the visitor after updating it
        }

        public async Task<Visitor> GetVisitor(string id)
        {
            return await _visitors.Find(visitor => visitor.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Visitor>> GetAllVisitors()
        {
            return await _visitors.Find(visitor => true).ToListAsync();
        }

        public async Task DeleteVisitor(string id)
        {
            await _visitors.DeleteOneAsync(visitor => visitor.Id == id);
        }

        public IEnumerable<Visitor> GetVisitorsByDate(DateTime date)
        {
            var filter = Builders<Visitor>.Filter.Eq(v => v.DateEntered.Date, date.Date);
            return _visitors.Find(filter).ToList();
        }

    }
}