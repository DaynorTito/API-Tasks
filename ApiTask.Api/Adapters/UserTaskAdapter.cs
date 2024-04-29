using ApiTask.Domain.Ports;

namespace ApiTask.Api.Adapters;
using ApiTask.Api.Adapters;
using ApiTask.Data.ScaffoldModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
public class UserTaskAdapter
{
    private UserPersistanceAdapter _userPersistenceAdapter;

    public UserTaskAdapter()
    {
        
    }
    public UserTaskAdapter(UserPersistanceAdapter userPersistenceAdapter)
    {
        _userPersistenceAdapter = userPersistenceAdapter;
    }

    public async Task<List<UserWithTasks>> GetUserWithTasksAsync()
    {
        _userPersistenceAdapter = new UserPersistanceAdapter();
        var users = await _userPersistenceAdapter.GetAllUsersAsync();
        var usersWithTasks = new List<UserWithTasks>();
        foreach (var user in users)
        {
            var tasks = await _userPersistenceAdapter.GetTasksByUserIdAsync(user.Id);
            usersWithTasks.Add(new UserWithTasks
            {
                User = user,
                Tasks = tasks
            });
        }
        return usersWithTasks;
    }
}

public class UserWithTasks
{
    public User User { get; set; }
    public List<Task> Tasks { get; set; }
}