using Trava.Scripts.Repositories;
using Trava.Scripts.Models;

namespace Trava.Blazor.Services.Server;

public class ILexemeService
{
    private TranslationRepository Repository { get; init; }

    public ILexemeService()
    {
        Repository = new TranslationRepository("language.db");
    }

    public Lexeme? ConvertToLexeme(Lemma lemma)
    {
        return LexemeParser.ConvertToLexeme(Repository, lemma);
    }
}
