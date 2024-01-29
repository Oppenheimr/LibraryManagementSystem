using LibraryManagementSystem.Data;
using Newtonsoft.Json;

namespace LibraryManagementSystem.DataBase;

public class BookDataBase : DataBase<Book>
{

    private string _filePath;

    protected override string FilePath() => string.IsNullOrEmpty(_filePath)
        ? (_filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BookData.json")) : _filePath; 
    
    public override void UpdateObject(Book bookData)
    {
        var books = LoadObjects();

        for (int i = 0; i < books.Length; i++)
        {
            if (books[i].Title == bookData.Title)
            {
                books[i] = bookData;
            }
        }

        SaveObjects(books);
    }
    
    public override bool LoadObject(string title, out Book? book)
    {
        book = null;
        var booksData = LoadObjects();

        foreach (var data in booksData)
        {
            if (data.Title != title) 
                continue;
            book = data;
            return true;
        }

        return false; // Book with the specified title not found
    }

    protected override void CreateFile() => File.WriteAllText(FilePath(), JsonConvert.SerializeObject(new GenericCollection<Book>()
    {
        Collection = new[]
        {
            new Book("War an Peace", "Leo Tolstoy", "500-482-95-1", 12, 1),
            new Book("Frankenstein", "Mary Shelley", "542-876-21-5", 7, 1),
            new Book("1984", "George Orwell", "971-354-86-2", 3,2),
            new Book("Blood, Sweat, and Pixels ", "Jason Schreier", "854-793-24-9", 15,1),
            new Book("Time Machine", "H. G. Wells", "879-423-45-7", 4,1),
            new Book("Gravity", "Tess Gerritsen", "487-315-46-2", 2,1),
            new Book("Cosmos", "Carl Sagan", "847-624-73-1", 9,2),
            new Book("Surely You're Joking Mr. Feynman!", "Richard P. Feynman", "874-542-16-2", 9,1),
            new Book("The Origin of Species", "Charles Darwin", "897-547-24-6", 5,2),
            new Book("Dune", "Frank Herbert", "123-456-78-9",45),
            new Book("To Kill a Mockingbird", "Harper Lee", "234-567-89-0",7),
            new Book("The Great Gatsby", "F. Scott Fitzgerald", "345-678-90-1",8),
            new Book("Moby-Dick", "Herman Melville", "456-789-01-2", 14),
            new Book("The Catcher in the Rye", "J.D. Salinger", "567-890-12-3",14),
            new Book("Brave New World", "Aldous Huxley", "678-901-23-4",62),
            new Book("The Lord of the Rings", "J.R.R. Tolkien", "789-012-34-5",24),
            new Book("The Chronicles of Narnia", "C.S. Lewis", "890-123-45-6",16),
            new Book("One Hundred Years of Solitude", "Gabriel Garcia Marquez", "901-234-56-7",26),
            new Book("The Hitchhiker's Guide to the Galaxy", "Douglas Adams", "012-345-67-8",32),
            new Book("Crime and Punishment", "Fyodor Dostoevsky", "234-567-89-0", 6),
            new Book("The Great Expectations", "Charles Dickens", "345-678-90-1", 11),
            new Book("The Old Man and the Sea", "Ernest Hemingway", "456-789-01-2", 5),
            new Book("Alice's Adventures in Wonderland", "Lewis Carroll", "567-890-12-3", 9),
            new Book("Pride and Prejudice", "Jane Austen", "678-901-23-4", 7),
            new Book("The Shining", "Stephen King", "789-012-34-5", 13),
            new Book("The Hobbit", "J.R.R. Tolkien", "123-456-78-9", 8),
            new Book("The Da Vinci Code", "Dan Brown", "234-567-89-0", 10),
            new Book("The Hunger Games", "Suzanne Collins", "345-678-90-1", 6),
            new Book("The Alchemist", "Paulo Coelho", "456-789-01-2", 15),
            new Book("Gone with the Wind", "Margaret Mitchell", "567-890-12-3", 7),
            new Book("The Godfather", "Mario Puzo", "678-901-23-4", 12),
            new Book("The Kite Runner", "Khaled Hosseini", "789-012-34-5", 9),
            new Book("Jurassic Park", "Michael Crichton", "890-123-45-6", 11),
            new Book("The Girl with the Dragon Tattoo", "Stieg Larsson", "901-234-56-7", 14),
            new Book("The Silence of the Lambs", "Thomas Harris", "012-345-67-8", 5),
            new Book("The Road", "Cormac McCarthy", "345-678-90-1", 8),
            new Book("The Three Musketeers", "Alexandre Dumas", "567-890-12-3", 7),
            new Book("The War of the Worlds", "H.G. Wells", "789-012-34-5", 10),
            new Book("The Joy Luck Club", "Amy Tan", "901-234-56-7", 6),
            new Book("The Picture of Dorian Gray", "Oscar Wilde", "012-345-67-8", 9),
            new Book("The Martian", "Andy Weir", "234-567-89-0", 11),
            new Book("The Secret Garden", "Frances Hodgson Burnett", "345-678-90-1", 8),
            new Book("The Count of Monte Cristo", "Alexandre Dumas", "456-789-01-2", 10),
            new Book("The Catcher in the Rye", "J.D. Salinger", "567-890-12-3", 7),
            new Book("The Grapes of Wrath", "John Steinbeck", "678-901-23-4", 9),
        }
    }));

    private static BookDataBase _instance;
    public static BookDataBase Instance => _instance != null ? _instance : (_instance = new BookDataBase());
}