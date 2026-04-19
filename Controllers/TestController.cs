using Microsoft.AspNetCore.Mvc;

namespace Project1.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/test")]
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