using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using TheBoys.Shared.Abstractions;

namespace TheBoys.API.Localization;

public class JsonStringLocalizer : IStringLocalizer
{
    private readonly Dictionary<string, Dictionary<string, string>> _localizationData;

    private readonly IUserContext _userContext;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public JsonStringLocalizer(
        IHttpContextAccessor httpContextAccessor,
        IServiceScopeFactory serviceScopeFactory
    )
    {
        _localizationData = LoadLocalizationData();
        _serviceScopeFactory = serviceScopeFactory;
        _userContext = _serviceScopeFactory
            .CreateScope()
            .ServiceProvider.GetService<IUserContext>();
    }

    private Dictionary<string, Dictionary<string, string>> LoadLocalizationData()
    {
        var localizationData = new Dictionary<string, Dictionary<string, string>>();
        var localizationFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources");

        foreach (var file in Directory.GetFiles(localizationFolderPath, "*.json"))
        {
            var cultureName = Path.GetFileNameWithoutExtension(file);
            var jsonData = File.ReadAllText(file);
            var translations = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);
            localizationData[cultureName] = translations;
        }
        return localizationData;
    }

    public LocalizedString this[string name]
    {
        get
        {
            var value = GetLocalizedString(name);
            return new LocalizedString(name, value ?? name, resourceNotFound: value == null);
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var value = string.Format(GetLocalizedString(name) ?? name, arguments);
            return new LocalizedString(name, value, resourceNotFound: value == null);
        }
    }

    private string GetLocalizedString(string name)
    {
        if (
            _localizationData.TryGetValue(_userContext.Language.Value, out var translations)
            && translations.TryGetValue(name, out var localizedValue)
        )
        {
            return localizedValue;
        }

        return null;
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) =>
        _localizationData[_userContext.Language.Value]
            .Select(kvp => new LocalizedString(kvp.Key, kvp.Value, resourceNotFound: false));
}
