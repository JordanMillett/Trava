using AutoMapper;
using Trava.Scripts.Repositories;

namespace Trava.Scripts.Models;

public class LexemeMaps : Profile
{
    public LexemeMaps()
    {
        CreateMap<NounRecord, NounLexeme>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => LexemeParser.ParseGender(src.Gender)))
            .ForMember(dest => dest.Animate, opt => opt.MapFrom(src => src.Animate == "1"))
            .ForMember(dest => dest.Indeclinable, opt => opt.MapFrom(src => src.Indeclinable == "1"))
            .ForMember(dest => dest.Uncountable, opt => opt.MapFrom(src => src.Uncountable == "1"))
            .ForMember(dest => dest.PluralOnly, opt => opt.MapFrom(src => src.PluralOnly == "1"));

        CreateMap<VerbRecord, VerbLexeme>();
        CreateMap<AdjectiveRecord, AdjectiveLexeme>();
        CreateMap<OtherRecord, Lexeme>();
    }
}

public enum GenderType
{
    Masculine,
    Feminine,
    Neuter,
    None
}

public class Lexeme
{
    public required string Term { get; set; } = default!;
    public required string Stressed { get; set; } = default!;
    public required string Translation { get; set; } = default!;
}

public class NounLexeme : Lexeme
{
    public required GenderType Gender { get; set; } = default!;
    public required bool Animate { get; set; } = default!;
    public required bool Indeclinable { get; set; } = default!;
    public required bool Uncountable { get; set; } = default!;
    public required bool PluralOnly { get; set; } = default!;
    public required string NominativeSingular { get; set; } = default!;
    public required string GenitiveSingular { get; set; } = default!;
    public required string DativeSingular { get; set; } = default!;
    public required string AccusativeSingular { get; set; } = default!;
    public required string InstrumentalSingular { get; set; } = default!;
    public required string PrepositionalSingular { get; set; } = default!;
    public required string NominativePlural { get; set; } = default!;
    public required string GenitivePlural { get; set; } = default!;
    public required string DativePlural { get; set; } = default!;
    public required string AccusativePlural { get; set; } = default!;
    public required string InstrumentalPlural { get; set; } = default!;
    public required string PrepositionalPlural { get; set; } = default!;
}

public class AdjectiveLexeme : Lexeme
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

public class VerbLexeme : Lexeme
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

public static class LexemeParser
{
    private static readonly IMapper Mapper;

    static LexemeParser()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<LexemeMaps>();
        });
        Mapper = config.CreateMapper();
    }

    public static Lexeme? ConvertToLexeme(TranslationRepository repository, Lemma lemma)
    {
        OtherRecord? retrieved = lemma.PartOfSpeech switch
        {
            PartOfSpeechType.Noun => repository.GetNoun(lemma.NormalForm),
            PartOfSpeechType.Verb => repository.GetVerb(lemma.NormalForm),
            PartOfSpeechType.Adjective => repository.GetAdjective(lemma.NormalForm),
            PartOfSpeechType.Other => repository.GetOther(lemma.NormalForm),
            _ => null
        };

        retrieved ??= repository.TryGetFromAny(lemma.NormalForm);

        if(retrieved == null)
            return null;

        Lexeme term = null!;

        if(retrieved is NounRecord noun)
        {
            term = Mapper.Map<NounLexeme>(noun);
        }else if (retrieved is VerbRecord verb)
        {
            term = Mapper.Map<VerbLexeme>(verb);
        }else if (retrieved is AdjectiveRecord adjective)
        {
            term = Mapper.Map<AdjectiveLexeme>(adjective);            
        }else if (retrieved is OtherRecord other)
        {
            term = Mapper.Map<Lexeme>(other);
        }

        term.Term = lemma.NormalForm;

        return term;
    }

    private static readonly Dictionary<string, GenderType> genderMap = new()
    {
        { "m", GenderType.Masculine },
        { "f", GenderType.Feminine },
        { "n", GenderType.Neuter }
    };

    public static GenderType ParseGender(string? raw)
    {
        if (raw != null && genderMap.TryGetValue(raw, out var value))
            return value;
        return GenderType.None;
    }
}