using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(string unp, string email)
        {
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