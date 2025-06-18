namespace Trava.Scripts.Translations;

public enum PartOfSpeechType
{
    Noun,
    Adverb,
    Verb,
    Adjective,
    Pronoun,
    Particle,
    Preposition,
    Conjuction,
    Other
}

public enum NounCaseType
{
    Nominative,
    Genitive,
    Dative,
    Accusative,
    Instrumental,
    Prepositional,
    None
}

public class RussianLemma
{
    public string DisplayText { get; set; } = default!;
    public string NormalForm { get; set; } = default!;

    public PartOfSpeechType PartOfSpeech { get; set; } = default!;
    public NounCaseType NounCase { get; set; } = default!;
    public bool Plural { get; set; } = default!;
    public GenderType Gender { get; set; } = default!;

    public RussianLemma(string displayText = default!)
    {
        DisplayText = displayText;
    }

    public string GetNounDescription()
    {
        string number = Plural ? "Plural" : "Singular";

        return $"{Gender} {NounCase} {number}";
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
            lemma.PartOfSpeech = ParsePartOfSpeech(data.tag.GetAttr("POS").ToString());
            lemma.NounCase = ParseNounCase(data.tag.GetAttr("case").ToString());
            lemma.Plural = data.tag.GetAttr("number").ToString() == "plur";
            lemma.Gender = ParseGender(data.tag.GetAttr("gender").ToString());

            //if(displayText == "тем")
            //Console.WriteLine(data.tag.POS.ToString());
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
        { "ADVB",  PartOfSpeechType.Adverb},
        { "NPRO",  PartOfSpeechType.Pronoun},
        { "PRED",  PartOfSpeechType.Other},
        { "PREP",  PartOfSpeechType.Preposition},
        { "CONJ",  PartOfSpeechType.Conjuction},
        { "PRCL",  PartOfSpeechType.Particle},
        { "INTJ",  PartOfSpeechType.Other},
    };

    public static PartOfSpeechType ParsePartOfSpeech(string? raw)
    {
        if (raw != null && partOfSpeechMap.TryGetValue(raw, out var value))
            return value;
        return PartOfSpeechType.Other;
    }

    private static readonly Dictionary<string, NounCaseType> nounCaseMap = new()
    {
        { "nomn",  NounCaseType.Nominative},
        { "people",  NounCaseType.Genitive},
        { "gent",  NounCaseType.Genitive},
        { "datv",  NounCaseType.Dative},
        { "accs",  NounCaseType.Accusative},
        { "ablt",  NounCaseType.Instrumental},
        { "place",  NounCaseType.Prepositional},
        { "loct",  NounCaseType.Prepositional},
        { "gen2",  NounCaseType.Genitive},
        { "acc2",  NounCaseType.Accusative},
        { "loc2",  NounCaseType.Prepositional},
    };

    public static NounCaseType ParseNounCase(string? raw)
    {
        if (raw != null && nounCaseMap.TryGetValue(raw, out var value))
            return value;
        //Console.WriteLine(raw);
        return NounCaseType.None;
    }

    private static readonly Dictionary<string, GenderType> genderMap = new()
    {
        { "masc", GenderType.Masculine },
        { "femn", GenderType.Feminine },
        { "neut", GenderType.Neuter },
    };

    public static GenderType ParseGender(string? raw)
    {
        if (raw != null && genderMap.TryGetValue(raw, out var value))
            return value;
        //Console.WriteLine(raw);
        return GenderType.None;
    }
}