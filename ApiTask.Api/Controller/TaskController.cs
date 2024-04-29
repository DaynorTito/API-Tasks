using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ApiTask.Data;
using Microsoft.AspNetCore.Authorization;
using Task = ApiTask.Data.ScaffoldModels.Task;


namespace ApiTask.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskDbContext _dbcontext;

        public TaskController(TaskDbContext context)
        {
            _dbcontext = context;
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            List<Data.ScaffoldModels.Task> tasks = new List<Data.ScaffoldModels.Task>();
            try
            {
                tasks = _dbcontext.Tasks.ToList();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = tasks });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status200OK, new { message = e.Message, response = tasks });
            }
        }
        
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetTaskById(String id)
        {
            try
            {
                var task = _dbcontext.Tasks.Find(id);
                if (task == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "Task not found" });
                }
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = task });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }
        
        [HttpPost]
        [Authorize]
        public IActionResult CreateTask([FromBody] Data.ScaffoldModels.Task task)
        {
            try
            {
                _dbcontext.Tasks.Add(task);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, new { message = "Task created successfully", response = task });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }
        
        [HttpPut]
        [Authorize]
        [Route("{id}")]
        public IActionResult UpdateTask([FromBody] Data.ScaffoldModels.Task updatedTask)
        {
            try
            {
                var task = _dbcontext.Tasks.Find(updatedTask.Id);
                if (task == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "Task not found" });
                }

                task.Title = updatedTask.Title;
                task.Description = updatedTask.Description;
                task.DueDate = updatedTask.DueDate;
                task.Status = updatedTask.Status;

                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "Task updated successfully", response = task });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }
        
        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public IActionResult DeleteTask(string id)
        {
            try
            {
                var task = _dbcontext.Tasks.Find(id);
                if (task == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "Task not found" });
                }

                _dbcontext.Tasks.Remove(task);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "Task deleted successfully" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }
        
        [HttpGet("{userId}/{priority}")]
        public IActionResult GetUserTasksByPriority(string userId, string priority)
        {
            try
            {
                IQueryable<Data.ScaffoldModels.Task> tasksQuery = _dbcontext.Tasks
                    .Where(t => t.UserId == userId); 
                switch (priority.ToLower())
                {
                    case "high":
                        tasksQuery = tasksQuery.Where(t => t.Priority == "high");
                        break;
                    case "medium":
                        tasksQuery = tasksQuery.Where(t => t.Priority == "medium");
                        break;
                    case "low":
                        tasksQuery = tasksQuery.Where(t => t.Priority == "low");
                        break;
                    case "without priority":
                        tasksQuery = tasksQuery.Where(t => t.Priority == null);
                        break;
                    default:
                        return StatusCode(StatusCodes.Status400BadRequest, new { message = "Invalid priority" });
                }
                tasksQuery = tasksQuery.OrderBy(t => t.Priority);
                var tasks = tasksQuery.ToList();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = tasks });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }
    }
}
