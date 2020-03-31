using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using TodoDataAPI.Models;

namespace TodoDataAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TodoController:ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;

        }

        [HttpGet("users/{userid:int}/todos")]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos([FromRoute] int userid)
        {

            User user = await this._context.Users.Include(u => u.Todos).FirstOrDefaultAsync();

            if (user == null) return NotFound(404);
            
            return user.Todos.ToList();

        }

        [HttpPost("users/{userid:int}/todos")]

        public async Task<ActionResult> PostTodo([FromRoute] int userid, Todo todo)
        {
            if (todo == null) return BadRequest();

            var user = await this._context.FindAsync<User>(userid);
            if (user == null) return NotFound();
            if (todo.CreatedAt is null) todo.CreatedAt = DateTime.Now;

            user.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodos), new { id = todo.TodoID }, todo);



        }

        [HttpDelete("users/{userid:int}/todos/{todoid}")]
        public async Task<ActionResult<Todo>> DeleteTodo([FromQuery]int todoid)
        {

            var todo = await this._context.Todos.Where(u => u.TodoID == todoid).FirstOrDefaultAsync();
            if (todo == null) return BadRequest();


            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return todo;

        }

        [HttpPut("users/{userid}/todos/{todoid}")]
        public async Task<ActionResult> EditTodo([FromQuery] int todoid, Todo todo)
        {
            var existing = await _context.Todos.Where(u => u.TodoID == todoid).FirstOrDefaultAsync();
            if (existing == null) return BadRequest();

            existing = todo;
            await _context.SaveChangesAsync();

            return Ok();


        }
    }
}
