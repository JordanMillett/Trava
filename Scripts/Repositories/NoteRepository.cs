using SQLite;

namespace Trava.Scripts.Repositories;

public class NoteRepository
{
    private readonly SQLiteConnection db;

    public NoteRepository(string databasePath)
    {
        db = new SQLiteConnection(databasePath);
        db.CreateTable<NoteRecord>();
    }

    public void UpdateNote(NoteRecord note)
    {
        if (db.Find<NoteRecord>(note.GUID) == null)
        {
            note.CreationDate = DateTime.UtcNow;
            note.ModifiedDate = DateTime.UtcNow;
            db.Insert(note);
        }
        else
        {
            note.ModifiedDate = DateTime.UtcNow;
            db.Update(note);
        }
    }

    public List<NoteRecord> GetAllNotes()
    {
        return db.Table<NoteRecord>().OrderBy(n => n.CreationDate).ToList();
    }

    public List<NoteRecord> GetNotesByTitle(string title)
    {
        return db.Table<NoteRecord>().Where(n => n.Title.Contains(title)).OrderBy(n => n.Title).ToList();
    }
}

public record NoteRecord
{
    [PrimaryKey, AutoIncrement]
    public int GUID { get; set; }

    [Indexed]
    public string Title { get; set; } = default!;
    public string Contents { get; set; } = default!;

    public bool Pinned { get; set; } = default!;
    public bool Archived { get; set; } = default!;
    public DateTime CreationDate { get; set; } = default!;
    public DateTime ModifiedDate { get; set; } = default!;
}