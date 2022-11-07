using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using API.Entity;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        readonly Regex emailRegex = new(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        readonly Regex unpRegex = new("^[0-9]+$");

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(string unp, string email)
        {
            if (unp == null || 
                email == null || 
                unp.Length <= 6 || 
                !unpRegex.Match(unp).Success ||
                !emailRegex.Match(email).Success)
                    return BadRequest("Email/Unp is invalid!");
            var result = await _userService.AddUserAsync(new AppUser(unp, email));
            return result ? Ok(result) : BadRequest();
        }

        [HttpGet("{unp}")]
        public async Task<IActionResult> GetUser(string unp)
        {
            var user = await _userService.GetUserByUnpAsync(unp);
            return user != null ? Ok(user) : BadRequest(); 
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }
    }
}