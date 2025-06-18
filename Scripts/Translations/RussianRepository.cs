using SQLite;

namespace Trava.Scripts.Translations;

public class RussianRepository
{
    private readonly SQLiteConnection db;

    public RussianRepository(string databasePath)
    {
        db = new SQLiteConnection(databasePath);
    }

    public NounRecord? GetNoun(string key)
    {
        return db.Table<NounRecord>().Where(t => t.Term == key).OrderByDescending(t => t.EntryID).FirstOrDefault();
    }

    public VerbRecord? GetVerb(string key)
    {
        return db.Table<VerbRecord>().Where(t => t.Term == key).OrderByDescending(t => t.EntryID).FirstOrDefault();
    }

    public AdjectiveRecord? GetAdjective(string key)
    {
        return db.Table<AdjectiveRecord>().Where(t => t.Term == key).OrderByDescending(t => t.EntryID).FirstOrDefault();
    }

    public OtherRecord? GetOther(string key)
    {
        return db.Table<OtherRecord>().Where(t => t.Term == key).OrderByDescending(t => t.EntryID).FirstOrDefault();
    }

    public OtherRecord? TryGetFromAny(string key)
    {
        OtherRecord? found = null;
        found ??= GetNoun(key);
        found ??= GetAdjective(key);
        found ??= GetVerb(key);
        found ??= GetOther(key);

        return found;
    }
}