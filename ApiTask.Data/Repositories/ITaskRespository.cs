using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiTask.Data.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Task>> GetAllTasks();
        Task<Task> GetTaskById(int id);
        Task<Task> AddTask(Task task);
        Task<Task> UpdateTask(Task task);
        Task<Task> DeleteTask(int id);
    }
}
