using Trava.Scripts.Translations;

namespace Trava.Services;

public class TranslationService
{
    public RussianRepository Russian { get; init; }

    public TranslationService()
    {
        Russian = new RussianRepository("russian.db");
    }
}
