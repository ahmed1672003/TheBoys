namespace TheBoys.API.Controllers;

[Route("api/v1/stream")]
[ApiController]
public class StreamController : ControllerBase
{
    private readonly IWebHostEnvironment _env;

    public StreamController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [HttpPost()]
    [Authorize]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No image provided.");

        if (!file.FileName.IsImage())
        {
            return BadRequest("Not supported image extension.");
        }

        // Create uploads folder if it doesn't exist
        var uploadsRootFolder = Path.Combine(_env.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsRootFolder))
            Directory.CreateDirectory(uploadsRootFolder);

        // Generate unique file name
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadsRootFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Return full URL path
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var fileUrl = $"{baseUrl}/uploads/{fileName}";

        return Ok(
            new ResponseOf<dynamic>
            {
                Success = true,
                Result = new { path = fileUrl, fileName = fileName },
            }
        );
    }

    [Authorize]
    [HttpGet("delete/{fileName}")]
    public IActionResult DeleteFile(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            return BadRequest("File name is required.");

        var uploadsRootFolder = Path.Combine(_env.WebRootPath, "uploads");
        var filePath = Path.Combine(uploadsRootFolder, fileName);

        if (!System.IO.File.Exists(filePath))
            return NotFound("File not found.");

        System.IO.File.Delete(filePath);

        return Ok(new Response { Message = "File deleted successfully.", Success = true });
    }
}
