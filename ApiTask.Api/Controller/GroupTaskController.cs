using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ApiTask.Data;
using ApiTask.Data.ScaffoldModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ApiTask.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupTaskController : ControllerBase
    {
        private readonly TaskDbContext _dbContext;

        public GroupTaskController(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetGroups()
        {
            List<GroupTask> groups = new List<GroupTask>();
            try
            {
                groups = _dbContext.GroupTasks.ToList();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = groups });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetGroupById(string id)
        {
            try
            {
                var group = _dbContext.GroupTasks.Find(id);
                if (group == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "Group not found" });
                }

                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = group });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateGroup([FromBody] GroupTask group)
        {
            try
            {
                _dbContext.GroupTasks.Add(group);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, new { message = "Group created successfully", response = group });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateGroup(string id, [FromBody] GroupTask updatedGroup)
        {
            try
            {
                var group = _dbContext.GroupTasks.Find(id);
                if (group == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "Group not found" });
                }

                group.Name = updatedGroup.Name;
                group.Description = updatedGroup.Description;

                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "Group updated successfully", response = group });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult DeleteGroup(string id)
        {
            try
            {
                var group = _dbContext.GroupTasks.Find(id);
                if (group == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "Group not found" });
                }

                _dbContext.GroupTasks.Remove(group);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "Group deleted successfully" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }
    }
}
