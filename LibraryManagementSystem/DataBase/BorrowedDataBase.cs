using LibraryManagementSystem.Data;
using LibraryManagementSystem.Extensions;
using Newtonsoft.Json;

namespace LibraryManagementSystem.DataBase;

public class BorrowedDataBase : DataBase<BorrowedBook>
{
    private string _filePath;

    protected override string FilePath() => string.IsNullOrEmpty(_filePath)
        ? (_filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BorrowBookData.json")) : _filePath; 
    
    public override void UpdateObject(BorrowedBook bookData)
    {
        var books = LoadObjects();

        for (var i = 0; i < books.Length; i++)
        {
            if (books[i].ISBN == bookData.ISBN)
            {
                books[i] = bookData;
            }
        }

        SaveObjects(books);
    }
    
    public override bool LoadObject(string isbn, out BorrowedBook? book)
    {
        book = null;
        var booksData = LoadObjects();

        foreach (var data in booksData)
        {
            if (data.ISBN != isbn) 
                continue;
            
            book = data;
            return true;
        }

        return false; // Book with the specified title not found
    }
    
    /// <summary>
    /// Lokal veri tabanından bir objeyi kaldırır
    /// </summary>
    /// <param name="objectToRemove"></param>
    public void RemoveObject(string title, string borrower)
    {
        var objects = LoadObjects();

        if (objects == null || objects.Length == 0) return;

        var list = new List<BorrowedBook>(objects);
        foreach (var borrowed in objects)
        {
            if (borrowed.Borrower != borrower)
                continue;
            
            if (borrowed.GetBook()!.Title == title)
                list.Remove(borrowed);
        }

        objects = list.ToArray();
        SaveObjects(objects);
        _objects = objects;
    }
    
    protected override void CreateFile() => File.WriteAllText(FilePath(), JsonConvert.SerializeObject(new GenericCollection<BorrowedBook>()
    {
        Collection = new[]
        {
            new BorrowedBook("500-482-95-1", "Max Hamilton", DateTimeOffset.UtcNow + TimeSpan.FromDays(2)),
            new BorrowedBook("542-876-21-5", "Isaac Feynman", DateTimeOffset.UtcNow + TimeSpan.FromDays(3)),
            new BorrowedBook("971-354-86-2", "Conan Doyle", DateTimeOffset.UtcNow - TimeSpan.FromDays(2)),
            new BorrowedBook("971-354-86-2", "Neil Pasteur", DateTimeOffset.UtcNow - TimeSpan.FromDays(4)),
            new BorrowedBook("854-793-24-9", "Dimitry Arthur Belyayev", DateTimeOffset.UtcNow - TimeSpan.FromDays(8)),
            new BorrowedBook("879-423-45-7", "Marie Lovelace", DateTimeOffset.UtcNow - TimeSpan.FromDays(12)),
            new BorrowedBook("487-315-46-2", "Nicolaus Goodall", DateTimeOffset.UtcNow - TimeSpan.FromDays(7)),
            new BorrowedBook("847-624-73-1", "Copernicus Mendel", DateTimeOffset.UtcNow + TimeSpan.FromDays(15)),
            new BorrowedBook("847-624-73-1", "Thomas Elion", DateTimeOffset.UtcNow + TimeSpan.FromDays(16)),
            new BorrowedBook("874-542-16-2", "Ernico Thompson", DateTimeOffset.UtcNow - TimeSpan.FromDays(38)),
            new BorrowedBook("897-547-24-6", "George Hubble", DateTimeOffset.UtcNow - TimeSpan.FromDays(22)),
            new BorrowedBook("897-547-24-6", "Sigmund Chadwick", DateTimeOffset.UtcNow - TimeSpan.FromDays(52)),
        }
    }));
    
    private static BorrowedDataBase _instance;
    public static BorrowedDataBase Instance => _instance != null ? _instance : (_instance = new BorrowedDataBase());
}