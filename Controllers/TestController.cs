using Microsoft.AspNetCore.Mvc;

namespace Project1.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            message = "App Service is working 🚀",
            time = DateTime.UtcNow
        });
    }

    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok("pong");
    }
}