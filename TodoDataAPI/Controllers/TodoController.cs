using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TodoDataAPI.Models;

namespace TodoDataAPI.Controllers
{
    [Route("api/")]
    [Authorize]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly UserManager<AppUser> _userManager;

        public TodoController(TodoContext context, UserManager<AppUser> userManager) => (this._context, this._userManager) = (context, userManager);


        [HttpGet("todos")]

        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {


            return Ok(await GetAuthenticatedUserTodos());
        }


        [HttpPost("todos")]

        public async Task<ActionResult> PostTodo([FromBody] Todo todo)
        {
            if (todo == null) return BadRequest();

            //var user=_userManager.GetUserAsync(User);
            string idclaim = User.Claims.Where(c =>
            {
                Trace.WriteLine(c);
                return c.Type == ClaimTypes.NameIdentifier;
            }).FirstOrDefault().Value;


            var user = await this._context.FindAsync<AppUser>(idclaim);
            if (user == null) return NotFound();
            if (todo.CreatedAt is null) todo.CreatedAt = DateTime.Now;

            user.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodos), new { id = todo.TodoID }, todo);



        }

        private async Task<IEnumerable<Todo>> GetAuthenticatedUserTodos()
        {
            string idclaim = User.Claims.Where(c =>
            {
                return c.Type == ClaimTypes.NameIdentifier;
            }).FirstOrDefault().Value;

            //var user = await this._context.FindAsync<AppUser>(idclaim);
            var user = await _context.AppUsers.Include(a => a.Todos).Where(u => u.Id == idclaim).FirstAsync();


            return user.Todos;


        }
        //untested
        [HttpDelete("todos/{todoid}")]
        public async Task<ActionResult<Todo>> DeleteTodo(int todoid)
        {
            var todos = await GetAuthenticatedUserTodos();

            var todo = todos.Where(t => t.TodoID == todoid).FirstOrDefault();
            if (todo == null) return BadRequest();


            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return todo;
        }

        //untested
        [HttpPut("todos/{todoid}/done")]
        public async Task<IActionResult> SwitchDone(int todoid)
        {
            var todo = (await GetAuthenticatedUserTodos()).Where(t => t.TodoID == todoid).FirstOrDefault();
            if (todo == null) return BadRequest();
            todo.Done = !todo.Done;
            await _context.SaveChangesAsync();
            return Ok(todo);
        }


        ///Untested
        [HttpPut("todos/{todoid}")]
        public async Task<ActionResult> EditTodo(int todoid, Todo todo)
        {
            if (todoid != todo.TodoID)
                return BadRequest("The Todo item has to fit to the Todo in the URL Parameters");
            var existing = (await GetAuthenticatedUserTodos()).Where(t => t.TodoID == todoid).FirstOrDefault();
            if (existing == null) return BadRequest();


            var e = _context.Entry(existing);
            e.CurrentValues.SetValues(todo);


            await _context.SaveChangesAsync();

            return Ok(todo);


        }
    }
}
