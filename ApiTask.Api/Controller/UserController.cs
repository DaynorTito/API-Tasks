using System;
using System.Collections.Generic;
using ApiTask.Api.Adapters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ApiTask.Data;
using ApiTask.Data.ScaffoldModels;
using Microsoft.AspNetCore.Authorization;

namespace ApiTask.Api.Controller
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly TaskDbContext _dbContext;
        private UserTaskAdapter _userTaskAdapter;

        public UserController(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("user")]
        public IActionResult GetUsers()
        {
            List<User> users = new List<User>();
            try
            {
                users = _dbContext.Users.ToList();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = users });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(string id)
        {
            try
            {
                var user = _dbContext.Users.Find(id);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "User not found" });
                }

                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = user });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created,
                    new { message = "User created successfully", response = user });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateUser(string id, [FromBody] User updatedUser)
        {
            try
            {
                var user = _dbContext.Users.Find(id);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "User not found" });
                }

                user.Username = updatedUser.Username;
                user.Email = updatedUser.Email;
                user.Passwd = updatedUser.Passwd;

                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK,
                    new { message = "User updated successfully", response = user });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteUser(string id)
        {
            try
            {
                var user = _dbContext.Users.Find(id);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "User not found" });
                }

                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "User deleted successfully" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }
        
        [HttpGet("usersWithTasks")]
        public async Task<IActionResult> GetUsersWithTasks()
        {
            try
            {
                _userTaskAdapter = new UserTaskAdapter(new UserPersistanceAdapter());
                var usersWithTasks = await _userTaskAdapter.GetUserWithTasksAsync();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = usersWithTasks });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }
    }
}
