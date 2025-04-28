using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sync.Api.Configurations;
using Sync.Api.Models;

namespace Sync.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IMongoClient? client, IOptions<MongoDbConfig> config)
        {
            if (client == null || config == null || config.Value == null)
            {
                throw new ArgumentNullException("client or config is null");
            }
            var database = client.GetDatabase(config.Value.DatabaseName);
            _users = database.GetCollection<User>(config.Value.UsersCollection);
        }

        public async Task AddAsync(User? user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await _users.InsertOneAsync(user);
        }

        public async Task<User?> GetByEmailAsync(string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }
            return await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User?> GetByIdAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await _users.Find(u => u.Id.ToString() == id).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetInactiveUsersAsync(DateTime threshold)
        {
            return await _users.Find(u => u.LastActive < threshold).ToListAsync();
        }

        public async Task DeleteAsync(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }
            await _users.DeleteOneAsync(u => u.Id.ToString() == id);
        }
    }
}