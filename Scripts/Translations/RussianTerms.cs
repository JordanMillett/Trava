namespace Trava.Scripts.Translations;

public enum RussianGenderType
{
    Masculine,
    Feminine,
    Neuter
}

public class RussianTerm
{
    public string Term { get; set; } = default!;
    public string Stressed { get; set; } = default!;
    public string Translation { get; set; } = default!;
}

public class RussianNoun : RussianTerm
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
    public static RussianTerm? ConvertToTerm(RussianRepository repository, RussianLemma lemma)
    {
        //LOOKUP
        return null;
    }

    private static readonly Dictionary<string, RussianGenderType> genderMap = new()
    {
        { "m", RussianGenderType.Masculine },
        { "f", RussianGenderType.Feminine },
        { "n", RussianGenderType.Neuter }
    };

    public static RussianGenderType ParseGender(string? raw)
    {
        if (raw != null && genderMap.TryGetValue(raw, out var gender))
            return gender;
        return RussianGenderType.Masculine;
    }
}