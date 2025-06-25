using Trava.Scripts.Repositories;
using Trava.Scripts.Models;

namespace Trava.Blazor.Services.Server;

public class INoteService
{
    private NoteRepository Repository { get; init; }

    public INoteService()
    {
        Repository = new NoteRepository("notes.db");
    }

    public void CreateNote(string title, string contents)
    {
        NoteRecord created = new NoteRecord()
        {
            Title = title,
            Contents = contents
        };

        UpdateNote(created);
    }

    public void UpdateNote(NoteRecord note)
    {
        Repository.UpdateNote(note);
    }

    public List<NoteRecord> GetAllNotes()
    {
        return Repository.GetAllNotes();
    }

    public List<NoteRecord> GetNotesByTitle(string title)
    {
        return Repository.GetNotesByTitle(title);
    }
}
