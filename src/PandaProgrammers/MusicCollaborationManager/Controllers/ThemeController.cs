using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.DAL.Abstract;

namespace MusicCollaborationManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        //private readonly IThemeController _choreRepository;

        public ThemeController()
        {
            //_choreRepository = choreRepository;
        }

        // GET: api/Chore
        [HttpGet("chores")]
        public IActionResult GetChores(int personId)
        {
            // DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            // int days = 7;
            // List<TaskTodo> tasks = _choreRepository.GetAllTasksTodoWithinTimeWindow(today, days);
            // List<TaskTodo> finalTasks = new List<TaskTodo>();
            // foreach (TaskTodo task in tasks)
            // {
            //     if (task.personId == personId)
            //     {                    
            //         finalTasks.Add(task);
            //     }
            // }
            // return Ok(finalTasks);
            return Ok();
        }

        // POST: api/Chore
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("themeAdd")]
        public async Task<ActionResult> themeAdd(string Description, string date, int interval, int itemId)
        {
            // Chore chore = new Chore();
            // chore.Description = Description;
            // chore.Interval = interval;
            // chore.ItemId = itemId;
            // chore.InitialDate = DateTime.Parse(date);

            // return Ok(_choreRepository.AddOrUpdate(chore));
            return Ok();
        }

    }
}