using ApiTask.Data.ScaffoldModels;

namespace ApiTask.Domain.Ports;

public interface IUserPersistencePort
{
    Task<User> GetUserByIdAsync(Guid userId);
    Task<User> GetUserByEmailAsync(string email);
    Task<User> GetUserByUserNameAsync(string userName);
    Task<User> CreateUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task DeleteUserAsync(Guid userId);
    Task<List<User>> GetAllUsersAsync();
    Task<List<Data.ScaffoldModels.Task>> GetTasksByUserIdAsync(string userId);
}