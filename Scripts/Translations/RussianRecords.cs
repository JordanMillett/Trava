using SQLite;

namespace Trava.Scripts.Translations;

public record OtherRecord
{
    [PrimaryKey, AutoIncrement]
    public int EntryID { get; set; }

    public string Term { get; set; } = default!;
    public string Stressed { get; set; } = default!;
    public string Translation { get; set; } = default!;
}

public record AdjectiveRecord : OtherRecord
{
    public string Comparative { get; set; } = default!;
    public string Superlative { get; set; } = default!;
    public string ShortMasculine { get; set; } = default!;
    public string ShortFeminine { get; set; } = default!;
    public string ShortNeuter { get; set; } = default!;
    public string ShortPlural { get; set; } = default!;
    public string NominativeMasculine { get; set; } = default!;
    public string GenitiveMasculine { get; set; } = default!;
    public string DativeMasculine { get; set; } = default!;
    public string AccusativeMasculine { get; set; } = default!;
    public string InstrumentalMasculine { get; set; } = default!;
    public string PrepositionalMasculine { get; set; } = default!;
    public string NominativeFeminine { get; set; } = default!;
    public string GenitiveFeminine { get; set; } = default!;
    public string DativeFeminine { get; set; } = default!;
    public string AccusativeFeminine { get; set; } = default!;
    public string InstrumentalFeminine { get; set; } = default!;
    public string PrepositionalFeminine { get; set; } = default!;
    public string NominativeNeuter { get; set; } = default!;
    public string GenitiveNeuter { get; set; } = default!;
    public string DativeNeuter { get; set; } = default!;
    public string AccusativeNeuter { get; set; } = default!;
    public string InstrumentalNeuter { get; set; } = default!;
    public string PrepositionalNeuter { get; set; } = default!;
    public string NominativePlural { get; set; } = default!;
    public string GenitivePlural { get; set; } = default!;
    public string DativePlural { get; set; } = default!;
    public string AccusativePlural { get; set; } = default!;
    public string InstrumentalPlural { get; set; } = default!;
    public string PrepositionalPlural { get; set; } = default!;
}

public record NounRecord : OtherRecord
{
    public string Gender { get; set; } = default!;
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

public record VerbRecord : OtherRecord
{
    public string Aspect { get; set; } = default!;
    public string AspectualPartner { get; set; } = default!;
    public string SingularImperative { get; set; } = default!;
    public string PluralImperative { get; set; } = default!;
    public string PastMasculine { get; set; } = default!;
    public string PastFeminine { get; set; } = default!;
    public string PastNeuter { get; set; } = default!;
    public string PastPlural { get; set; } = default!;
    public string FirstPersonSingular { get; set; } = default!;
    public string SecondPersonSingular { get; set; } = default!;
    public string ThirdPersonSingular { get; set; } = default!;
    public string FirstPersonPlural { get; set; } = default!;
    public string SecondPersonPlural { get; set; } = default!;
    public string ThirdPersonPlural { get; set; } = default!;
}