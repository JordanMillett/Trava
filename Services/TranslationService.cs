using Trava.Repositories;

namespace Trava.Services;

public class TranslationService
{
    private readonly string databasePath;
    public TranslationRepository Repository { get; init; }

    public TranslationService()
    {
        databasePath = "translations.db";
        Repository = new TranslationRepository(databasePath);
    }
}
