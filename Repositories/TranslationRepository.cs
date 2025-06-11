using SQLite;

namespace Trava.Repositories;

public record TranslationRecord
{
    [PrimaryKey, AutoIncrement]
    public int EntryID { get; set; }

    [Indexed]
    public string Term { get; set; } = default!;

    [NotNull]
    public string Translation { get; set; } = default!;
}

public class TranslationRepository
{
    private readonly SQLiteConnection db;

    public TranslationRepository(string databasePath)
    {
        db = new SQLiteConnection(databasePath);
        db.CreateTable<TranslationRecord>();
    }

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