namespace Trava.Scripts.Translations;

public enum PartOfSpeechType
{
    Noun,
    Verb,
    Adjective,
    Other
}

public class RussianLemma
{
    public string DisplayText { get; set; } = default!;
    public string NormalForm { get; set; } = default!;

    public PartOfSpeechType PartOfSpeech { get; set; } = default!;

    public RussianLemma(string displayText = default!)
    {
        DisplayText = displayText;
    }
}

public static class RussianLemmaParser
{
    public static RussianLemma ExtractLemma(string displayText, dynamic data)
    {
        RussianLemma lemma = new RussianLemma();
        lemma.DisplayText = displayText;

        if(data.normal_form != null)
        {
            lemma.NormalForm = data.normal_form.ToString();
            lemma.PartOfSpeech = ParsePartOfSpeech(data.POS);
        }

        return lemma;
    }

    private static readonly Dictionary<string, PartOfSpeechType> partOfSpeechMap = new()
    {
        { "NOUN",  PartOfSpeechType.Noun},
        { "ADJF",  PartOfSpeechType.Adjective},
        { "ADJS",  PartOfSpeechType.Adjective},
        { "COMP",  PartOfSpeechType.Adjective},
        { "VERB",  PartOfSpeechType.Verb},
        { "INFN",  PartOfSpeechType.Verb},
        { "PRTF",  PartOfSpeechType.Verb},
        { "PRTS",  PartOfSpeechType.Verb},
        { "GRND",  PartOfSpeechType.Verb},
        { "NUMR",  PartOfSpeechType.Noun},
        { "ADVB",  PartOfSpeechType.Other},
        { "NPRO",  PartOfSpeechType.Other},
        { "PRED",  PartOfSpeechType.Other},
        { "PREP",  PartOfSpeechType.Other},
        { "CONJ",  PartOfSpeechType.Other},
        { "PRCL",  PartOfSpeechType.Other},
        { "INTJ",  PartOfSpeechType.Other},
    };

    public static PartOfSpeechType ParsePartOfSpeech(string? raw)
    {
        if (raw != null && partOfSpeechMap.TryGetValue(raw, out var value))
            return value;
        return PartOfSpeechType.Other;
    }
}