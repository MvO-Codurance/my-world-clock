using System.Globalization;
using TimeZoneNames;

namespace MyWorldClock;

public class LanguageService : ILanguageService
{
    private static readonly IEnumerable<Language> SupportedLanguages;

    static LanguageService()
    {
        var supportedLanguageCodes = TZNames.GetLanguageCodes(true);
        var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);

        SupportedLanguages = supportedLanguageCodes
            .Join(cultures,
                language => language.Replace("_", "-"),
                culture => culture.Name,
                (language, culture) => 
                    new Language(
                        Code: culture.Name, 
                        DisplayName: $"{culture.DisplayName} / {culture.NativeName}", 
                        Name: culture.DisplayName, 
                        NativeName: culture.NativeName
                    )
            )
            .OrderBy(language => language.DisplayName);
    }
    
    public IEnumerable<Language> GetLanguages()
    {
        return SupportedLanguages;
    }
}