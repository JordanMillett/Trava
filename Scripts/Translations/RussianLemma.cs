namespace Trava.Scripts.Translations;

public enum PartOfSpeechType
{
    Noun
}

public class RussianLemma
{
    public string NormalForm { get; set; } = default!;

    public PartOfSpeechType PartOfSpeech { get; set; } = default!;
}

public static class RussianLemmaParser
{
    public static RussianLemma? ExtractLemma(dynamic data)
    {
        RussianLemma lemma = new RussianLemma();
        lemma.NormalForm = data.normal_form.ToString();
        
        return null;
    }
}