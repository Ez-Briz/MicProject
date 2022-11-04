using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class InfoController : ControllerBase
{

    public InfoController()
    {
    }
    [HttpGet]
    public async void GetInfo(string unp)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://www.portal.nalog.gov.by/grp/");
        string request = $"getData?unp={unp}&type=json";
        var response = await client.GetAsync(request);
        
        Console.WriteLine(await response.Content.ReadAsStringAsync());
    }
}
