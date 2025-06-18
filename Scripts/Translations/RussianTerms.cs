using AutoMapper;

namespace Trava.Scripts.Translations;

public class RussianMaps : Profile
{
    public RussianMaps()
    {
        CreateMap<NounRecord, RussianNoun>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => RussianTermParser.ParseGender(src.Gender)))
            .ForMember(dest => dest.Animate, opt => opt.MapFrom(src => src.Animate == "1"))
            .ForMember(dest => dest.Indeclinable, opt => opt.MapFrom(src => src.Indeclinable == "1"))
            .ForMember(dest => dest.Uncountable, opt => opt.MapFrom(src => src.Uncountable == "1"))
            .ForMember(dest => dest.PluralOnly, opt => opt.MapFrom(src => src.PluralOnly == "1"));

        CreateMap<VerbRecord, RussianVerb>();
        CreateMap<AdjectiveRecord, RussianAdjective>();
        CreateMap<OtherRecord, RussianTerm>();
    }
}

public enum GenderType
{
    Masculine,
    Feminine,
    Neuter,
    None
}

/*
    public string GetDescriptor(string value)
    {
        Console.WriteLine(PrepositionalSingular);

        string termCase = value switch
        {
            var v when v == NominativeSingular.Replace("\'","") => "Nominative Singular",
            var v when v == GenitiveSingular.Replace("\'","") => "Genitive Singular",
            var v when v == DativeSingular.Replace("\'","") => "Dative Singular",
            var v when v == AccusativeSingular.Replace("\'","") => "Accusative Singular",
            var v when v == InstrumentalSingular.Replace("\'","") => "Instrumental Singular",
            var v when v == PrepositionalSingular.Replace("\'","") => "Prepositional Singular",
            var v when v == NominativePlural.Replace("\'","") => "Nominative Plural",
            var v when v == GenitivePlural.Replace("\'","") => "Genitive Plural",
            var v when v == DativePlural.Replace("\'","") => "Dative Plural",
            var v when v == AccusativePlural.Replace("\'","") => "Accusative Plural",
            var v when v == InstrumentalPlural.Replace("\'","") => "Instrumental Plural",
            var v when v == PrepositionalPlural.Replace("\'","") => "Prepositional Plural",
            _ => "Unknown Case"
        };

        string termGender = Gender.ToString();

        return $"{termGender} {termCase}";
    }*/

public class RussianTerm
{
    public required string Term { get; set; } = default!;
    public required string Stressed { get; set; } = default!;
    public required string Translation { get; set; } = default!;
}

public class RussianNoun : RussianTerm
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

public class RussianAdjective : RussianTerm
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

public class RussianVerb : RussianTerm
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

public static class RussianTermParser
{
    private static readonly IMapper Mapper;

    static RussianTermParser()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<RussianMaps>();
        });
        Mapper = config.CreateMapper();
    }

    public static RussianTerm? ConvertToTerm(RussianRepository repository, RussianLemma lemma)
    {
        /*
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
        { "CONJ",  PartOfSpeechType.Adjective},
        { "PRCL",  PartOfSpeechType.Other},
        { "INTJ",  PartOfSpeechType.Other},
        */

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

        RussianTerm term = null!;

        if(retrieved is NounRecord noun)
        {
            term = Mapper.Map<RussianNoun>(noun);
        }else if (retrieved is VerbRecord verb)
        {
            term = Mapper.Map<RussianVerb>(verb);
        }else if (retrieved is AdjectiveRecord adjective)
        {
            term = Mapper.Map<RussianAdjective>(adjective);            
        }else if (retrieved is OtherRecord other)
        {
            term = Mapper.Map<RussianTerm>(other);
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