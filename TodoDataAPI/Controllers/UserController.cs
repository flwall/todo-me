using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoDataAPI.Models;

namespace TodoDataAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly TodoContext _context;
        public UserController(TodoContext context)
        {
            _context = context;

        }

        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {

            return await _context.Users.ToListAsync();

        }




        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            await this._context.Users.AddAsync(user);
            await this._context.SaveChangesAsync();


            return CreatedAtAction(nameof(GetUsers), user);
        }

        [HttpDelete("{userid}")]
        public async Task<ActionResult<User>> DeleteUser(int userid)
        {

            var user = await this._context.Users.Where(u => u.ID == userid).FirstOrDefaultAsync();
            if (user == null) return BadRequest();


            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
            
        }
        

        [HttpPut("{userid}")]
        public async Task<ActionResult> EditUser(int userid, User user)
        {
            var existing = await _context.Users.Where(u => u.ID == userid).FirstOrDefaultAsync();
            if (existing == null) return BadRequest();

            existing = user;
            await _context.SaveChangesAsync();

            return Ok();


        }

    }
}