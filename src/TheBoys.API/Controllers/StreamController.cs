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
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file provided.");

        // Create uploads folder if it doesn't exist
        var uploadsRootFolder = Path.Combine(_env.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsRootFolder))
            Directory.CreateDirectory(uploadsRootFolder);

        // Generate unique file name
        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(uploadsRootFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Return full URL path
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var fileUrl = $"{baseUrl}/uploads/{fileName}";

        return Ok(new { path = fileUrl, fileName = fileName });
    }

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

        return Ok(new { message = "File deleted successfully." });
    }
}
