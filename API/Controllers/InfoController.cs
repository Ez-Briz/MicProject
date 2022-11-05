using System;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using API.Data;
using API.Entity;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class InfoController : ControllerBase
{

    private readonly IUserService _userService;
    private readonly IInfoService _infoService;

    public InfoController(IUserService userService, IInfoService infoService)
    {
        _userService = userService;
        _infoService = infoService;
    }

    [HttpGet]
    public async Task<IActionResult> GetInfo(string unp)
    {
        Info info = await _infoService.GetInfoFromPortal(unp);
        if (info == null)
            return BadRequest("Unp not found!");

        AppUser user = await _userService.GetUserByUnpAsync(unp);
        if (user == null)
            return BadRequest("User not found!");
        
        if (user.LastStatus != info.Row.Ckodsost) // change with infoService
        {
            user.LastStatus = info.Row.Ckodsost;
            return Ok(user.LastStatus); // plug
        }
        return Ok(info);
    }

}
