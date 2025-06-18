using SQLite;

namespace Trava.Scripts.Translations;

public class RussianRepository
{
    private readonly SQLiteConnection db;

    public RussianRepository(string databasePath)
    {
        db = new SQLiteConnection(databasePath);
    }

    /*
    public string? GetTranslation(string term)
    {
        TranslationRecord record = db.Table<TranslationRecord>().Where(t => t.Term == term).OrderByDescending(t => t.EntryID).FirstOrDefault();
        
        if(record != null)
        {   
            string[] parts = record.Translation.Split(';', StringSplitOptions.RemoveEmptyEntries);

            var primaryTranslations = parts
                .Select(p => p.Split(',')[0].Trim())
                .Where(p => !string.IsNullOrWhiteSpace(p));

            return string.Join(", ", primaryTranslations);
        }

        return null;
    }

    public string[]? GetTranslationAsLines(string term)
    {
        TranslationRecord record = db.Table<TranslationRecord>().Where(t => t.Term == term).OrderByDescending(t => t.EntryID).FirstOrDefault();
        
        if(record != null)
        {   
            return record.Translation.Split(';', StringSplitOptions.RemoveEmptyEntries);
        }

        return null;
    }
    */

    /*
    public void AddMessage(string userId, string content)
    {
        var message = new TranslationRecord
        {
            UserID = userId,
            Content = content,
            CreatedAt = DateTime.UtcNow
        };

        db.Insert(message);
    }

    public List<TranslationRecord> GetRecentMessages(int limit = 20)
    {
        return db.Table<TranslationRecord>()
                 .OrderByDescending(m => m.CreatedAt)
                 .Take(limit)
                 .ToList();
    }

    public List<TranslationRecord> GetMessagesRange(int offset, int limit)
    {
        return db.Table<TranslationRecord>()
                .OrderByDescending(m => m.CreatedAt)
                .Skip(offset)
                .Take(limit)
                .ToList();
    }

    public List<TranslationRecord> GetMessagesByUser(string userId, int limit = 20)
    {
        return db.Table<TranslationRecord>()
                 .Where(m => m.UserID == userId)
                 .OrderByDescending(m => m.CreatedAt)
                 .Take(limit)
                 .ToList();
    }
    */
}