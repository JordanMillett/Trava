using AutoMapper;

namespace Trava.Scripts.Translations;

public class RussianMaps : Profile
{
    public RussianMaps()
    {
        CreateMap<NounRecord, RussianNoun>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => RussianTermParser.ParseGender(src.Gender)))
            .ForMember(dest => dest.Animate, opt => opt.MapFrom(src => Convert.ToBoolean(src.Animate)))
            .ForMember(dest => dest.Indeclinable, opt => opt.MapFrom(src => Convert.ToBoolean(src.Indeclinable)))
            .ForMember(dest => dest.Uncountable, opt => opt.MapFrom(src => Convert.ToBoolean(src.Uncountable)))
            .ForMember(dest => dest.PluralOnly, opt => opt.MapFrom(src => Convert.ToBoolean(src.PluralOnly)));
    }
}

public enum RussianGenderType
{
    Masculine,
    Feminine,
    Neuter
}

public class RussianTerm
{
    public required string Term { get; set; } = default!;
    public required string Stressed { get; set; } = default!;
    public required string Translation { get; set; } = default!;
}

public class RussianNoun : RussianTerm
{
    public string GetDescriptor(string value)
    {
        Dictionary<string, string> map = new()
        {
            { NominativeSingular, "Nominative Singular" },
            { GenitiveSingular, "Genitive Singular" },
            { DativeSingular, "Dative Singular" },
            { AccusativeSingular, "Accusative Singular" },
            { InstrumentalSingular, "Instrumental Singular" },
            { PrepositionalSingular, "Prepositional Singular" },
            { NominativePlural, "Nominative Plural" },
            { GenitivePlural, "Genitive Plural" },
            { DativePlural, "Dative Plural" },
            { AccusativePlural, "Accusative Plural" },
            { InstrumentalPlural, "Instrumental Plural" },
            { PrepositionalPlural, "Prepositional Plural" }
        };

        string termCase = map.TryGetValue(value, out var label) ? label : "Unknown Case";
        string termGender = Gender.ToString();

        return $"{termGender} {termCase}";
    }

    public required RussianGenderType Gender { get; set; } = default!;
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
        var retrieved = lemma.PartOfSpeech switch
        {
            PartOfSpeechType.Noun => repository.GetNoun(lemma.NormalForm),
            _ => null
        };

        RussianTerm? term;

        if(retrieved is NounRecord noun)
        {
            term = Mapper.Map<RussianNoun>(noun);
            term.Term = lemma.NormalForm;
        }

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
        if (raw != null && genderMap.TryGetValue(raw, out var value))
            return value;
        return RussianGenderType.Masculine;
    }
}