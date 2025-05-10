using System.Text.Json;
using TheBoys.API.Data;
using TheBoys.API.Entities;

namespace TheBoys.API.Seeding;

public class SeedingService : ISeedingService
{
    readonly ApplicationDbContext _conetxt;
    readonly IWebHostEnvironment _webHostEnvironment;

    public SeedingService(ApplicationDbContext conetxt, IWebHostEnvironment webHostEnvironment)
    {
        _conetxt = conetxt;
        _webHostEnvironment = webHostEnvironment;
    }

    public void SeedLanguages()
    {
        if (_conetxt.Languages.Any())
            return;

        var fileName = "Languages.json";
        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Data", fileName);
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File {fileName} not found at path {filePath}");

        var languagesJson = File.ReadAllText(filePath);
        var languages = JsonSerializer.Deserialize<IEnumerable<Language>>(languagesJson);
        _conetxt.Languages.AddRange(languages);

        _conetxt.SaveChanges();
    }
}
