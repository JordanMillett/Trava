using SQLite;

namespace Trava.Scripts.Translations;

public class RussianLemma
{
    public string NormalForm { get; set; } = default!;
    public string Stressed { get; set; } = default!;
    public string Translation { get; set; } = default!;
}

public enum RussianGenderType
{
    Masculine,
    Feminine,
    Neuter
}

public class RussianNoun : RussianLemma
{
    public RussianGenderType Gender { get; set; } = default!;
    public string Animate { get; set; } = default!;
    public string Indeclinable { get; set; } = default!;
    public string Uncountable { get; set; } = default!;
    public string PluralOnly { get; set; } = default!;
    public string NominativeSingular { get; set; } = default!;
    public string GenitiveSingular { get; set; } = default!;
    public string DativeSingular { get; set; } = default!;
    public string AccusativeSingular { get; set; } = default!;
    public string InstrumentalSingular { get; set; } = default!;
    public string PrepositionalSingular { get; set; } = default!;
    public string NominativePlural { get; set; } = default!;
    public string GenitivePlural { get; set; } = default!;
    public string DativePlural { get; set; } = default!;
    public string AccusativePlural { get; set; } = default!;
    public string InstrumentalPlural { get; set; } = default!;
    public string PrepositionalPlural { get; set; } = default!;
}

public static class RussianParser
{
    public static RussianLemma? CreateLemma(dynamic data)
    {
        return null;
    }
}