using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ApiTask.Data;


namespace ApiTask.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskDbContext _dbcontext;

        public TaskController(TaskDbContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("task")]
        public IActionResult GetTasks()
        {
            List<Task> tasks = new List<Task>();
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
        [Route("task/{id}")]
        public IActionResult GetTaskById(int id)
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
        [Route("create")]
        public IActionResult CreateTask([FromBody] Task task)
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
        [Route("change/{id}")]
        public IActionResult UpdateTask([FromBody] Task updatedTask)
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
        [Route("delete/{id}")]
        public IActionResult DeleteTask(int id)
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

    }
}