using Trava.Scripts.Translations;

namespace Trava.Services;

public class TranslationService
{
    private RussianRepository Repository { get; init; }

    public TranslationService()
    {
        Repository = new RussianRepository("russian.db");
    }

    public RussianTerm? ConvertToTerm(RussianLemma lemma)
    {
        return RussianTermParser.ConvertToTerm(Repository, lemma);
    }
}
