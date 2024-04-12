using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiTask.Domain.Models;


namespace ApiTask.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly StoreManagementDbContext _context;

        public TaskRepository(StoreManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Task>> GetAllTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<Task> GetTaskById(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<Task> AddTask(Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Task> UpdateTask(Task task)
        {
            _context.Entry(task).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Task> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return null;
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return task;
        }
    }
}
