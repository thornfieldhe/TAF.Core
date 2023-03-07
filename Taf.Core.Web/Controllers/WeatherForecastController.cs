using Microsoft.AspNetCore.Mvc;

namespace Configuration.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase{
    private static readonly string[] Summaries = new[]{
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration          _configuration;

    public HomeController(ILogger<HomeController> logger,IConfiguration configuration){
        _logger             = logger;
        _configuration = configuration;
    }

    [HttpGet(Name = "Get")]
    public string Get() => _configuration["Test"];
}
