using System.Text.Json;
using TheBoys.Domain.Abstractions;
using TheBoys.Domain.Entities.Roles;
using TheBoys.Shared.Enums.Roles;
using TheBoys.Shared.Misc;

namespace TheBoys.API.Seeding;

public class SeedingService : ISeedingService
{
    readonly IWebHostEnvironment _webHostEnvironment;
    readonly IServiceScopeFactory _serviceScopeFactory;

    public SeedingService(
        IWebHostEnvironment webHostEnvironment,
        IServiceScopeFactory serviceScopeFactory
    )
    {
        _webHostEnvironment = webHostEnvironment;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public void SeedLanguages()
    {
        if (StaticLanguages.languageModels.Any())
            return;

        var fileName = "Languages.json";
        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Data", fileName);
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File {fileName} not found at path {filePath}");

        var languagesJson = File.ReadAllText(filePath);
        var languages = JsonSerializer.Deserialize<IEnumerable<LanguageModel>>(languagesJson);
        StaticLanguages.languageModels.AddRange(languages);
    }

    public void SeedRoles()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var roleRepository = scope.ServiceProvider.GetRequiredService<IRoleRepository>();

        if (roleRepository.Any())
            return;

        var roles = new List<Role>()
        {
            new Role() { Type = RoleType.SuperAdmin },
            new Role() { Type = RoleType.Admin },
        };
        roleRepository.CreateRange(roles);
        unitOfWork.SaveChanges();
    }
}
