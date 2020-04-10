using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoDataAPI.Models;

namespace TodoDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) =>
            (this._userManager, this._signInManager) = (userManager, signInManager);

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthUser authUser)
        {
            var user = await this._userManager.FindByNameAsync(authUser.Username);
            if (user == null)
            {
                return BadRequest("Invalid Username or Password");
            }

            var res = await _signInManager.PasswordSignInAsync(user, authUser.Password, false, false);
            if (res.Succeeded)
            {
                return Ok($"Logged in user {authUser.Username} successfully");
            }

            return BadRequest("Invalid Username or Password");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthUser user)
        {
            var u = new AppUser
            {
                UserName = user.Username,
                Email = user.Email
            };
            var result = await _userManager.CreateAsync(u, user.Password);
            if (result.Succeeded)
            {
                return Ok($"Registered {user.Username} successfully");
            }

            return BadRequest("Registering failed");

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok("Logged out successfully");
        }



    }
}