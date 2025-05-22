namespace TheBoys.Shared.Misc;

public class StaticLanguages
{
    public static List<LanguageModel> languageModels = new();
}

public class LanguageModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Flag { get; set; }
}
