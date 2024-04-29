using ApiTask.Data;
using ApiTask.Data.ScaffoldModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ApiTask.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusHistoryController : ControllerBase
    {
        private readonly TaskDbContext _dbContext;

        public StatusHistoryController(TaskDbContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public IActionResult GetStatusHistory()
        {
            List<StatusHistory> statusHistories = new List<StatusHistory>();
            try
            {
                statusHistories = _dbContext.StatusHistories.ToList();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = statusHistories });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status200OK, new { message = e.Message, response = statusHistories });
            }
        }
        
        [HttpGet("{id}")]
        public IActionResult GetStatusHistoryById(string id)
        {
            try
            {
                var statusHistory = _dbContext.StatusHistories.Find(id);
                if (statusHistory == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "Status history not found" });
                }
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = statusHistory });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateStatusHistory([FromBody] StatusHistory statusHistory)
        {
            try
            {
                _dbContext.StatusHistories.Add(statusHistory);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, new { message = "Status history created successfully", response = statusHistory });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult UpdateStatusHistory(string id, [FromBody] StatusHistory updatedStatusHistory)
        {
            try
            {
                var statusHistory = _dbContext.StatusHistories.Find(id);
                if (statusHistory == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "Status history not found" });
                }
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "Status history updated successfully", response = statusHistory });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteStatusHistory(string id)
        {
            try
            {
                var statusHistory = _dbContext.StatusHistories.Find(id);
                if (statusHistory == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, new { message = "Status history not found" });
                }
                _dbContext.StatusHistories.Remove(statusHistory);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { message = "Status history deleted successfully" });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }
        
        [HttpGet("user/{userId}")]
        [Authorize]
        public IActionResult GetStatusHistoryByUserId(string userId)
        {
            try
            {
                var statusHistories = _dbContext.StatusHistories
                    .Where(h => h.Task.UserId == userId)
                    .ToList();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = statusHistories });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = e.Message });
            }
        }

    }
}
