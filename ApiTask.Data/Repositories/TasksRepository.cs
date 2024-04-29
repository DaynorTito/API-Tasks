using MySql.Data.MySqlClient;
using Task = ApiTask.Data.ScaffoldModels.Task;

namespace ApiTask.Data.Repositories;

public class TasksRepository : ITaskRepository
{
    private readonly MySqlConfiguration _connectionString;
    
    public TasksRepository(MySqlConfiguration connectionString)
    {
        _connectionString = connectionString;
    }

    protected MySqlConnection dbConnection()
    {
        return new MySqlConnection(_connectionString.ToString());
    }
    
    public Task<IEnumerable<Task>> GetAllTasks()
    {
        throw new NotImplementedException();
    }

    public Task<Task> GetTaskById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Task> AddTask(Task task)
    {
        throw new NotImplementedException();
    }

    public Task<Task> UpdateTask(Task task)
    {
        throw new NotImplementedException();
    }

    public Task<Task> DeleteTask(int id)
    {
        throw new NotImplementedException();
    }
}