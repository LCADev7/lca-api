﻿using Church.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Church.Data
{
    public class DataContext
    {
        private readonly IMongoDatabase _database = null;

        public DataContext(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("DefaultConnection"));
            if (client != null)
                _database = client.GetDatabase("ChurchDB");
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<Role> Roles => _database.GetCollection<Role>("Roles");
        public IMongoCollection<ProfilePhoto> ProfilePhotos => _database.GetCollection<ProfilePhoto>("ProfilePhotos");

    }
}