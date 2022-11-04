using System;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using API.Data;
using API.Entity;
using API.Interfaces;
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

    public InfoController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetInfo(string unp)
    {
        Info info = await GetInfoFromPortal(unp);
        if (info == null)
            return BadRequest("Unp not found!");

        AppUser user = await _userService.GetUserByUnpAsync(unp);
        if (user == null)
            return BadRequest("User not found!");
        
        if (user.LastStatus != info.Row.Ckodsost)
        {
            user.LastStatus = info.Row.Ckodsost;
            return Ok(user.LastStatus); // plug
        }
        return Ok(info);
    }

    static private async Task<Info> GetInfoFromPortal(string unp)
    {
        HttpClient client = new()
        {
            BaseAddress = new Uri("http://www.portal.nalog.gov.by/grp/")
        };
        string request = $"getData?unp={unp}&charset=UTF-8&type=json";
        var response = await client.GetAsync(request);
        if (!response.IsSuccessStatusCode)
            return null;

        var responseMessage = await response.Content.ReadAsStringAsync();
        var info = JsonConvert.DeserializeObject<Info>(responseMessage);
        return info;
    }
}
