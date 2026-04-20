using Microsoft.AspNetCore.Mvc;

namespace Project1.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UploadController : ControllerBase
{
    private readonly BlobService _blobService;
    
    public UploadController(BlobService blobService)
    {
       _blobService = blobService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var url = await _blobService.UploadAsync(file);
        return Ok(new { url });
    }
}