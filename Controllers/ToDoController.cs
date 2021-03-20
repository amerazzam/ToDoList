using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;
using Microsoft.AspNetCore.Authorization;

namespace ToDoList.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDo_dbContext _context;
        public class userAction { 
         public int id { get; set; }
         public int userId { get; set; }
            public string action { get; set; }
            public DateTime startDate { get; set; }
        }

        public ToDoController(ToDo_dbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDo>>> GetActions()
        {
            var actions = _context.ToDo.AsQueryable();
            return await actions.ToListAsync();
        }

        // GET: api/ToDo/1
        [HttpGet("{id}")]
        public async Task<userAction> GetActions (int id)
        {
            var action = await _context.ToDo.FindAsync(id);

            if (action == null)
            {
                return null;
            }
            userAction u = new userAction();
            u.id = action.Id;
            u.userId = action.UserId;
            u.action = action.Action;
            u.startDate = action.StartDate;
            return u;
        }

        // PUT: api/ToDo/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActions(int id,ToDo actions)
        {
            if (id != actions.Id)
            {
                return BadRequest();
            }

            _context.Entry(actions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActionsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetActions", id, actions);
        }

        // POST: api/ToDo
        [HttpPost]
        public async Task<ActionResult<ToDo>> PostActions(ToDo actions)
        {
            _context.ToDo.Add(actions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActions", new { id = actions.Id }, actions);
        }

        // DELETE: api/ToDo/1
        [HttpDelete("{id}")]
        public async Task<string> DeleteActions(int id)
        {
            var actions = await _context.ToDo.FindAsync(id);
            if (actions == null)
            {
                return "Not Found";
            }

            _context.ToDo.Remove(actions);
            await _context.SaveChangesAsync();

            return "DELETED";
        }

        private bool ActionsExists(int id)
        {
            return _context.ToDo.Any(e => e.Id == id);
        }
    }
}
