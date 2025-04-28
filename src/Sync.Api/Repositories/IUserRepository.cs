using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sync.Api.Models;

namespace Sync.Api.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User? user);
        Task<User?> GetByEmailAsync(string? email);
        Task<User?> GetByIdAsync(string? id);
        Task<List<User>> GetInactiveUsersAsync(DateTime threshold);
        Task DeleteAsync(string? id);
    }
}