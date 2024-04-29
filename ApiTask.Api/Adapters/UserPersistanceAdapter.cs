using ApiTask.Data.ScaffoldModels;
using ApiTask.Domain.Ports;
using ApiTask.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiTask.Api.Adapters;

public class UserPersistanceAdapter : IUserPersistencePort
{
    public async Task<User> GetUserByIdnAsync(String userId)
    {
        using var _dbContext = new TaskDbContext();
        
        var result = _dbContext.Users.Find(userId);
        return result;
    }

    public Task<User> GetUserByIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        using var context = new TaskDbContext();
        var result = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        return result;
    }

    public Task<User> GetUserByUserNameAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public Task<User> CreateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        using var _dbContext = new TaskDbContext();
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<List<Data.ScaffoldModels.Task>> GetTasksByUserIdAsync(string userId)
    {
        using var _dbContext = new TaskDbContext();
        return await _dbContext.Tasks.Where(t => t.UserId == userId).ToListAsync();
    }
}